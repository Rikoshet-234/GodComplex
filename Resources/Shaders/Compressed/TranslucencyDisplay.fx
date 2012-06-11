#include"Inc/Global.fx"
Texture2D _TexIrradiance:register(t0);cbuffer cbObject:register( b1){float4x4 _Local2World;float4 _EmissiveColor;};struct VS_IN{float3 Position:POSITION;float3 Normal:NORMAL;float3 Tangent:TANGENT;float2 UV:TEXCOORD0;};struct PS_IN{float4 __Position:SV_POSITION;float3 Normal:NORMAL;float2 UV:TEXCOORD0;};PS_IN VS(VS_IN V){float4 P=mul(float4(V.Position,1.),_Local2World);PS_IN f;f.__Position=mul(P,_World2Proj);f.Normal=V.Normal;f.UV=.5*(1.+V.Position.xy);f.UV.y=1.-f.UV.y;return f;}float4 PS(PS_IN V):SV_TARGET0{return float4(lerp(TEX2D(_TexIrradiance,LinearClamp,V.UV).xyz,_EmissiveColor.xyz,_EmissiveColor.w),1.);}