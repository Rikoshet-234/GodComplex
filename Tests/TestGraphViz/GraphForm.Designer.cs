﻿namespace TestGraphViz
{
	partial class GraphForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.checkBoxRun = new System.Windows.Forms.CheckBox();
			this.buttonReset = new System.Windows.Forms.Button();
			this.buttonReload = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.buttonResetObstacles = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.buttonLoad = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.panelOutput = new TestGraphViz.PanelOutput(this.components);
			this.floatTrackbarControlDeltaTime = new Nuaj.Cirrus.Utility.FloatTrackbarControl();
			this.buttonResetAll = new System.Windows.Forms.Button();
			this.integerTrackbarControlIterationsCount = new Nuaj.Cirrus.Utility.IntegerTrackbarControl();
			this.label1 = new System.Windows.Forms.Label();
			this.floatTrackbarControlSpringConstant = new Nuaj.Cirrus.Utility.FloatTrackbarControl();
			this.label3 = new System.Windows.Forms.Label();
			this.floatTrackbarControlDampingConstant = new Nuaj.Cirrus.Utility.FloatTrackbarControl();
			this.floatTrackbarControlK0 = new Nuaj.Cirrus.Utility.FloatTrackbarControl();
			this.floatTrackbarControlK1 = new Nuaj.Cirrus.Utility.FloatTrackbarControl();
			this.floatTrackbarControlK2 = new Nuaj.Cirrus.Utility.FloatTrackbarControl();
			this.floatTrackbarControlK3 = new Nuaj.Cirrus.Utility.FloatTrackbarControl();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.checkBoxAutoCenter = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// checkBoxRun
			// 
			this.checkBoxRun.AutoSize = true;
			this.checkBoxRun.Location = new System.Drawing.Point(530, 309);
			this.checkBoxRun.Name = "checkBoxRun";
			this.checkBoxRun.Size = new System.Drawing.Size(46, 17);
			this.checkBoxRun.TabIndex = 2;
			this.checkBoxRun.Text = "Run";
			this.checkBoxRun.UseVisualStyleBackColor = true;
			this.checkBoxRun.CheckedChanged += new System.EventHandler(this.checkBoxRun_CheckedChanged);
			// 
			// buttonReset
			// 
			this.buttonReset.Location = new System.Drawing.Point(582, 305);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.Size = new System.Drawing.Size(46, 23);
			this.buttonReset.TabIndex = 3;
			this.buttonReset.Text = "Reset";
			this.buttonReset.UseVisualStyleBackColor = true;
			this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
			// 
			// buttonReload
			// 
			this.buttonReload.Location = new System.Drawing.Point(770, 508);
			this.buttonReload.Name = "buttonReload";
			this.buttonReload.Size = new System.Drawing.Size(75, 23);
			this.buttonReload.TabIndex = 4;
			this.buttonReload.Text = "Reload";
			this.buttonReload.UseVisualStyleBackColor = true;
			this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(530, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Delta-Time";
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 10;
			// 
			// buttonResetObstacles
			// 
			this.buttonResetObstacles.Location = new System.Drawing.Point(741, 305);
			this.buttonResetObstacles.Name = "buttonResetObstacles";
			this.buttonResetObstacles.Size = new System.Drawing.Size(98, 23);
			this.buttonResetObstacles.TabIndex = 3;
			this.buttonResetObstacles.Text = "Reset Obstacles";
			this.buttonResetObstacles.UseVisualStyleBackColor = true;
			this.buttonResetObstacles.Click += new System.EventHandler(this.buttonResetObstacles_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "*.sim";
			this.saveFileDialog1.Filter = "Simulation Files (*.sim)|*.sim";
			this.saveFileDialog1.RestoreDirectory = true;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "*.sim";
			this.openFileDialog1.FileName = "TestObstacles.obs";
			this.openFileDialog1.Filter = "Simulation Files (*.sim)|*.sim";
			this.openFileDialog1.RestoreDirectory = true;
			// 
			// buttonLoad
			// 
			this.buttonLoad.Location = new System.Drawing.Point(555, 508);
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.Size = new System.Drawing.Size(75, 23);
			this.buttonLoad.TabIndex = 15;
			this.buttonLoad.Text = "Load";
			this.buttonLoad.UseVisualStyleBackColor = true;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(639, 508);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 15;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			// 
			// panelOutput
			// 
			this.panelOutput.Location = new System.Drawing.Point(12, 12);
			this.panelOutput.Name = "panelOutput";
			this.panelOutput.Size = new System.Drawing.Size(512, 512);
			this.panelOutput.TabIndex = 0;
			this.panelOutput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelOutput_MouseDown);
			// 
			// floatTrackbarControlDeltaTime
			// 
			this.floatTrackbarControlDeltaTime.Location = new System.Drawing.Point(639, 13);
			this.floatTrackbarControlDeltaTime.MaximumSize = new System.Drawing.Size(10000, 20);
			this.floatTrackbarControlDeltaTime.MinimumSize = new System.Drawing.Size(70, 20);
			this.floatTrackbarControlDeltaTime.Name = "floatTrackbarControlDeltaTime";
			this.floatTrackbarControlDeltaTime.RangeMax = 1000F;
			this.floatTrackbarControlDeltaTime.RangeMin = 0F;
			this.floatTrackbarControlDeltaTime.Size = new System.Drawing.Size(200, 20);
			this.floatTrackbarControlDeltaTime.TabIndex = 17;
			this.floatTrackbarControlDeltaTime.Value = 0.01F;
			this.floatTrackbarControlDeltaTime.VisibleRangeMax = 0.01F;
			// 
			// buttonResetAll
			// 
			this.buttonResetAll.Location = new System.Drawing.Point(634, 305);
			this.buttonResetAll.Name = "buttonResetAll";
			this.buttonResetAll.Size = new System.Drawing.Size(75, 23);
			this.buttonResetAll.TabIndex = 21;
			this.buttonResetAll.Text = "Reset All";
			this.buttonResetAll.UseVisualStyleBackColor = true;
			this.buttonResetAll.Click += new System.EventHandler(this.buttonResetAll_Click);
			// 
			// integerTrackbarControlIterationsCount
			// 
			this.integerTrackbarControlIterationsCount.Location = new System.Drawing.Point(634, 105);
			this.integerTrackbarControlIterationsCount.MaximumSize = new System.Drawing.Size(10000, 20);
			this.integerTrackbarControlIterationsCount.MinimumSize = new System.Drawing.Size(70, 20);
			this.integerTrackbarControlIterationsCount.Name = "integerTrackbarControlIterationsCount";
			this.integerTrackbarControlIterationsCount.RangeMax = 100000;
			this.integerTrackbarControlIterationsCount.RangeMin = 0;
			this.integerTrackbarControlIterationsCount.Size = new System.Drawing.Size(149, 20);
			this.integerTrackbarControlIterationsCount.TabIndex = 23;
			this.integerTrackbarControlIterationsCount.Value = 200;
			this.integerTrackbarControlIterationsCount.VisibleRangeMax = 1000;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(530, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Spring Constant";
			// 
			// floatTrackbarControlSpringConstant
			// 
			this.floatTrackbarControlSpringConstant.Location = new System.Drawing.Point(639, 39);
			this.floatTrackbarControlSpringConstant.MaximumSize = new System.Drawing.Size(10000, 20);
			this.floatTrackbarControlSpringConstant.MinimumSize = new System.Drawing.Size(70, 20);
			this.floatTrackbarControlSpringConstant.Name = "floatTrackbarControlSpringConstant";
			this.floatTrackbarControlSpringConstant.RangeMax = 1000000F;
			this.floatTrackbarControlSpringConstant.RangeMin = -1000000F;
			this.floatTrackbarControlSpringConstant.Size = new System.Drawing.Size(200, 20);
			this.floatTrackbarControlSpringConstant.TabIndex = 17;
			this.floatTrackbarControlSpringConstant.Value = 10000F;
			this.floatTrackbarControlSpringConstant.VisibleRangeMax = 10000F;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(530, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(94, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Damping Constant";
			// 
			// floatTrackbarControlDampingConstant
			// 
			this.floatTrackbarControlDampingConstant.Location = new System.Drawing.Point(639, 65);
			this.floatTrackbarControlDampingConstant.MaximumSize = new System.Drawing.Size(10000, 20);
			this.floatTrackbarControlDampingConstant.MinimumSize = new System.Drawing.Size(70, 20);
			this.floatTrackbarControlDampingConstant.Name = "floatTrackbarControlDampingConstant";
			this.floatTrackbarControlDampingConstant.RangeMax = 1000000F;
			this.floatTrackbarControlDampingConstant.RangeMin = -1000000F;
			this.floatTrackbarControlDampingConstant.Size = new System.Drawing.Size(200, 20);
			this.floatTrackbarControlDampingConstant.TabIndex = 17;
			this.floatTrackbarControlDampingConstant.Value = -1000F;
			this.floatTrackbarControlDampingConstant.VisibleRangeMax = 1F;
			this.floatTrackbarControlDampingConstant.VisibleRangeMin = -2000F;
			// 
			// floatTrackbarControlK0
			// 
			this.floatTrackbarControlK0.Location = new System.Drawing.Point(639, 124);
			this.floatTrackbarControlK0.MaximumSize = new System.Drawing.Size(10000, 20);
			this.floatTrackbarControlK0.MinimumSize = new System.Drawing.Size(70, 20);
			this.floatTrackbarControlK0.Name = "floatTrackbarControlK0";
			this.floatTrackbarControlK0.RangeMax = 1000000F;
			this.floatTrackbarControlK0.RangeMin = -1000000F;
			this.floatTrackbarControlK0.Size = new System.Drawing.Size(200, 20);
			this.floatTrackbarControlK0.TabIndex = 17;
			this.floatTrackbarControlK0.Value = 1F;
			this.floatTrackbarControlK0.VisibleRangeMax = 1F;
			// 
			// floatTrackbarControlK1
			// 
			this.floatTrackbarControlK1.Location = new System.Drawing.Point(639, 150);
			this.floatTrackbarControlK1.MaximumSize = new System.Drawing.Size(10000, 20);
			this.floatTrackbarControlK1.MinimumSize = new System.Drawing.Size(70, 20);
			this.floatTrackbarControlK1.Name = "floatTrackbarControlK1";
			this.floatTrackbarControlK1.RangeMax = 1000000F;
			this.floatTrackbarControlK1.RangeMin = -1000000F;
			this.floatTrackbarControlK1.Size = new System.Drawing.Size(200, 20);
			this.floatTrackbarControlK1.TabIndex = 17;
			this.floatTrackbarControlK1.Value = 0.1F;
			this.floatTrackbarControlK1.VisibleRangeMax = 0.5F;
			// 
			// floatTrackbarControlK2
			// 
			this.floatTrackbarControlK2.Location = new System.Drawing.Point(639, 197);
			this.floatTrackbarControlK2.MaximumSize = new System.Drawing.Size(10000, 20);
			this.floatTrackbarControlK2.MinimumSize = new System.Drawing.Size(70, 20);
			this.floatTrackbarControlK2.Name = "floatTrackbarControlK2";
			this.floatTrackbarControlK2.RangeMax = 1000000F;
			this.floatTrackbarControlK2.RangeMin = -1000000F;
			this.floatTrackbarControlK2.Size = new System.Drawing.Size(200, 20);
			this.floatTrackbarControlK2.TabIndex = 17;
			this.floatTrackbarControlK2.Value = 1.5F;
			this.floatTrackbarControlK2.VisibleRangeMax = 2F;
			// 
			// floatTrackbarControlK3
			// 
			this.floatTrackbarControlK3.Location = new System.Drawing.Point(639, 223);
			this.floatTrackbarControlK3.MaximumSize = new System.Drawing.Size(10000, 20);
			this.floatTrackbarControlK3.MinimumSize = new System.Drawing.Size(70, 20);
			this.floatTrackbarControlK3.Name = "floatTrackbarControlK3";
			this.floatTrackbarControlK3.RangeMax = 1000000F;
			this.floatTrackbarControlK3.RangeMin = -1000000F;
			this.floatTrackbarControlK3.Size = new System.Drawing.Size(200, 20);
			this.floatTrackbarControlK3.TabIndex = 17;
			this.floatTrackbarControlK3.Value = 0.01F;
			this.floatTrackbarControlK3.VisibleRangeMax = 0.1F;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(610, 124);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(20, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "K0";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(610, 150);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(20, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "K1";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(608, 201);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(20, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "K2";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(608, 227);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(20, 13);
			this.label7.TabIndex = 5;
			this.label7.Text = "K3";
			// 
			// checkBoxAutoCenter
			// 
			this.checkBoxAutoCenter.AutoSize = true;
			this.checkBoxAutoCenter.Location = new System.Drawing.Point(530, 272);
			this.checkBoxAutoCenter.Name = "checkBoxAutoCenter";
			this.checkBoxAutoCenter.Size = new System.Drawing.Size(121, 17);
			this.checkBoxAutoCenter.TabIndex = 22;
			this.checkBoxAutoCenter.Text = "Auto-Center Camera";
			this.checkBoxAutoCenter.UseVisualStyleBackColor = true;
			// 
			// GraphForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(857, 543);
			this.Controls.Add(this.checkBoxAutoCenter);
			this.Controls.Add(this.buttonResetAll);
			this.Controls.Add(this.floatTrackbarControlK3);
			this.Controls.Add(this.floatTrackbarControlK2);
			this.Controls.Add(this.floatTrackbarControlK1);
			this.Controls.Add(this.floatTrackbarControlK0);
			this.Controls.Add(this.floatTrackbarControlDampingConstant);
			this.Controls.Add(this.floatTrackbarControlSpringConstant);
			this.Controls.Add(this.floatTrackbarControlDeltaTime);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonLoad);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonReload);
			this.Controls.Add(this.buttonResetObstacles);
			this.Controls.Add(this.buttonReset);
			this.Controls.Add(this.checkBoxRun);
			this.Controls.Add(this.panelOutput);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "GraphForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Graph Viz Test";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private PanelOutput panelOutput;
		private System.Windows.Forms.CheckBox checkBoxRun;
		private System.Windows.Forms.Button buttonReset;
		private System.Windows.Forms.Button buttonReload;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button buttonResetObstacles;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button buttonLoad;
		private System.Windows.Forms.Button buttonSave;
		private Nuaj.Cirrus.Utility.FloatTrackbarControl floatTrackbarControlDeltaTime;
		private System.Windows.Forms.Button buttonResetAll;
		private Nuaj.Cirrus.Utility.IntegerTrackbarControl integerTrackbarControlIterationsCount;
		private System.Windows.Forms.Label label1;
		private Nuaj.Cirrus.Utility.FloatTrackbarControl floatTrackbarControlSpringConstant;
		private System.Windows.Forms.Label label3;
		private Nuaj.Cirrus.Utility.FloatTrackbarControl floatTrackbarControlDampingConstant;
		private Nuaj.Cirrus.Utility.FloatTrackbarControl floatTrackbarControlK0;
		private Nuaj.Cirrus.Utility.FloatTrackbarControl floatTrackbarControlK1;
		private Nuaj.Cirrus.Utility.FloatTrackbarControl floatTrackbarControlK2;
		private Nuaj.Cirrus.Utility.FloatTrackbarControl floatTrackbarControlK3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox checkBoxAutoCenter;
	}
}