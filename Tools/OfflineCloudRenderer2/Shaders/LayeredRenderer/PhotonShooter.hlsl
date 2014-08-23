////////////////////////////////////////////////////////////////////////////////
// Photon Shooter
////////////////////////////////////////////////////////////////////////////////
//
// The goal of this compute shader is to shoot an important amount of photons from one side of a flat layer
// Cloud landscape is composed of a large amount of layers that will represent the entire landscape up to several kilometers in altitude
// 
// Photons will initially start from the top layer (assuming the Sun is above the clouds) and will be fired through each successive layer
//	from top to bottom.
// A "top" bucket of remaining photons is initially filled with indices of photons still in the race for shooting top to bottom, while another
//	"bottom" bucket (initially empty) will progressively fill up to indicate photons that came back up to the original top side of the layer.
// 
// When we reach the bottom layer, we start the process again using the bottom bucket and starting from the bottom layer up to the top layer.
// This time, it's the "top" bucket that fills up with photons bouncing back to the bottom of each layer.
// 
// The process is repeated until all photons have either escaped through the top or the bottom layer. Practice will tell us how many bounces
//	are actually needed to obtain a correct lighting for the entire cloudscape.
// 
//	---------------------------
//
// Every time a batch of photons is shot through between 2 layers, the photons that reached the bottom layer are splat to a 2D texture array
//	(as many array slices as layers)
//
// This texture 2D array is then used, with interpolation, to determine the lighting at a particular 3D position within the cloudscape.
//
#include "../Global.hlsl"
#include "PhotonStructure.hlsl"

static const uint	MAX_THREADS = 1024;

static const float	INITIAL_START_EPSILON = 1e-4;

cbuffer	CBInput : register( b8 )
{
	uint	_LayerIndex;			// Index of the layer we start shooting photons from
	uint	_LayersCount;			// Total amount of layers
	float	_LayerThickness;		// Thickness of each individual layer (in meters)
	float	_SigmaScattering;		// Scattering coefficient (in m^-1)

	uint	_MaxScattering;			// Maximum scattering events before discarding the photon
	float3	_CloudScapeSize;		// Size of the cloud scape covered by the 3D texture of densities

	uint	_BatchIndex;			// Photon batch index
}

Texture3D<float>	_TexDensity;	// 3D noise density texture covering the entire cloudscape

// Samples the cloud density given a world space position within the cloud scape
float	SampleDensity( float3 _Position )
{
	float3	TopCorner = float3( 0, 0, 0 ) + float3( -0.5 * _CloudScapeSize.x, _CloudScapeSize.y, -0.5 * _CloudScapeSize.z );
	float3	BottomCorner = float3( 0, 0, 0 ) + float3( 0.5 * _CloudScapeSize.x, 0.0, 0.5 * _CloudScapeSize.z );
	float3	UVW = (_Position - TopCorner) / (BottomCorner - TopCorner);
	return _TexDensity.SampleLevel( LinearClamp, UVW, 0.0 ).x;
}

void	ShootPhoton( uint _PhotonIndex )
{
	uint	PhotonStartOffset = _SourceBucketOffsets[_LayerIndex];
	uint	PhotonEndOffset = _SourceBucketOffsets[_LayerIndex+1];
	uint	PhotonsCount = PhotonEndOffset - PhotonStartOffset;
	if ( _PhotonIndex >= PhotonsCount )
		return;	// We've already shot all photons!

	uint	PhotonIndex = PhotonStartOffset + _PhotonIndex;
	Photon	Pp = _Photons[PhotonIndex];

	PhotonUnpacked	P;
	UnPackPhoton( Pp, P );

	// Prepare marching through the layer
	float	MarchedLength = 0.0;
	float	OpticalDepth = 0.0;
	uint	ScatteringEventsCount = 0;

	float	StepSize = _LayerThickness / 64.0;	// Arbitrary! Parameter!
	uint	StepsCount = 0;

	float	LayerTopAltitude = (_LayersCount-_LayerIndex) * _LayerThickness;
	float	LayerBottomAltitude = LayerTopAltitude - _LayerThickness;

	bool	Exited = false;

	while ( ScatteringEventsCount < _MaxScattering )
	{
		// March one step
		P.Position += StepSize * P.Direction;

		if ( P.Position.y > LayerTopAltitude )
		{	// Exited through top of layer, compute exact intersection
			float	t = (LayerTopAltitude - P.Position.y) / (StepSize * P.Direction.y);
			P.Position += t * StepSize * P.Direction;
			Exited = true;
			break;
		}
		else if ( P.Position.y < LayerBottomAltitude )
		{	// Exited through bottom of layer, compute exact intersection
			float	t = (LayerBottomAltitude - P.Position.y) / (StepSize * P.Direction.y);
			P.Position += t * StepSize * P.Direction;
			P.LayerIndex++;	// Passed through the layer
			Exited = true;
			break;
		}

		// Sample density
		float	Density = SampleDensity( P.Position );
		OpticalDepth += Density * _SigmaScattering;

		// Draw random number and check if we should be scattered
#if USE_RANDOM_TABLE
		float	Random = _Random[uint(-0.17138198 * (_MaxScattering * _PhotonIndex + StepsCount)) & (USE_RANDOM_TABLE-1)].x;
#else
		float	Random = Hash( -0.01718198 * (_MaxScattering * (0.27891+_PhotonIndex) + StepsCount) );
#endif

		if ( Random > exp( -OpticalDepth * StepSize ) )
		{	// We must scatter!
			P.Direction = Scatter( _PhotonIndex, ScatteringEventsCount++, _MaxScattering, P.Direction );
//			OpticalDepth = 0.0;				// Reset optical depth for next scattering event
			OpticalDepth += log( Random );	// Reset optical depth for next scattering event
		}

		StepsCount++;
	}

	if ( !Exited )
		return;

	// Write resulting photon
	PackPhoton( P, Pp );
	_Photons[PhotonIndex] = Pp;

	// Update buckets
	if ( P.LayerIndex == _LayerIndex )
	{	// Exited through top, update Bottom => Top bucket with that photon index and increase photons offset for next layer
		uint	NextLayerPhotonsOffset;
		InterlockedAdd( _BottomUpPhotonBucketOffsets[_LayerIndex+1], 1, NextLayerPhotonsOffset );
		_BottomUpPhotonBucket[NextLayerPhotonsOffset] = PhotonIndex;
	}
	else
	{	// Exited through bottom, update Top => Down bucket with that photon index and increase counter of photons still in the race to bottom layer
		uint	ThroughPhotonsCounter;
		InterlockedAdd( _TopDownPhotonBucketCounter[0], 1, ThroughPhotonsCounter );
		_TopDownPhotonBucket[ThroughPhotonsCounter] = PhotonIndex;

// 		uint	ThroughPhotonsCounter = _TopDownPhotonBucketCounter[0];
// 		_TopDownPhotonBucket[ThroughPhotonsCounter] = PhotonIndex;
// 		_TopDownPhotonBucketCounter[0] = ThroughPhotonsCounter+1;
	}
}

[numthreads( MAX_THREADS, 1, 1 )]
void	CS( uint3 _GroupID : SV_GROUPID, uint3 _GroupThreadID : SV_GROUPTHREADID )
{
//	uint	PhotonIndex = MAX_THREADS * _GroupID.x + _GroupThreadID.x;
	uint	PhotonIndex = MAX_THREADS * _BatchIndex + _GroupThreadID.x;
	ShootPhoton( PhotonIndex );
}