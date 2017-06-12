namespace Turtle_Graphics_Example
{
	partial class DemoForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonDraw = new System.Windows.Forms.Button();
			this.buttonDrawSpiral = new System.Windows.Forms.Button();
			this.buttonReset = new System.Windows.Forms.Button();
			this.buttonShowHideTurtle = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.panelSurface = new System.Windows.Forms.Panel();
			this.Coordinates = new System.Windows.Forms.ToolStripStatusLabel();
			this.Angle = new System.Windows.Forms.ToolStripStatusLabel();
			this.buttonSetAngle = new System.Windows.Forms.Button();
			this.buttonPenStatus = new System.Windows.Forms.Button();
			this.PenStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.buttonColor = new System.Windows.Forms.Button();
			this.PenColor = new System.Windows.Forms.ToolStripStatusLabel();
			this.serviceController1 = new System.ServiceProcess.ServiceController();
			this.buttonPenSize = new System.Windows.Forms.Button();
			this.PenSize = new System.Windows.Forms.ToolStripStatusLabel();
			this.buttonForward = new System.Windows.Forms.Button();
			this.buttonBackward = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.statusBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonDraw
			// 
			this.buttonDraw.Location = new System.Drawing.Point(11, 9);
			this.buttonDraw.Name = "buttonDraw";
			this.buttonDraw.Size = new System.Drawing.Size(78, 35);
			this.buttonDraw.TabIndex = 0;
			this.buttonDraw.Text = "Draw";
			this.buttonDraw.UseVisualStyleBackColor = true;
			this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
			// 
			// buttonDrawSpiral
			// 
			this.buttonDrawSpiral.Location = new System.Drawing.Point(11, 50);
			this.buttonDrawSpiral.Name = "buttonDrawSpiral";
			this.buttonDrawSpiral.Size = new System.Drawing.Size(78, 35);
			this.buttonDrawSpiral.TabIndex = 0;
			this.buttonDrawSpiral.Text = "Spiral";
			this.buttonDrawSpiral.UseVisualStyleBackColor = true;
			this.buttonDrawSpiral.Click += new System.EventHandler(this.buttonDrawSpiral_Click);
			// 
			// buttonReset
			// 
			this.buttonReset.Location = new System.Drawing.Point(11, 93);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.Size = new System.Drawing.Size(78, 35);
			this.buttonReset.TabIndex = 1;
			this.buttonReset.Text = "Reset";
			this.buttonReset.UseVisualStyleBackColor = true;
			this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
			// 
			// buttonShowHideTurtle
			// 
			this.buttonShowHideTurtle.Location = new System.Drawing.Point(12, 134);
			this.buttonShowHideTurtle.Name = "buttonShowHideTurtle";
			this.buttonShowHideTurtle.Size = new System.Drawing.Size(78, 34);
			this.buttonShowHideTurtle.TabIndex = 2;
			this.buttonShowHideTurtle.Text = "Hide &Turtle";
			this.buttonShowHideTurtle.UseVisualStyleBackColor = true;
			this.buttonShowHideTurtle.Click += new System.EventHandler(this.buttonShowHideTurtle_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonBackward);
			this.panel1.Controls.Add(this.buttonForward);
			this.panel1.Controls.Add(this.buttonPenSize);
			this.panel1.Controls.Add(this.buttonColor);
			this.panel1.Controls.Add(this.buttonPenStatus);
			this.panel1.Controls.Add(this.buttonSetAngle);
			this.panel1.Controls.Add(this.buttonShowHideTurtle);
			this.panel1.Controls.Add(this.buttonReset);
			this.panel1.Controls.Add(this.buttonDraw);
			this.panel1.Controls.Add(this.buttonDrawSpiral);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(101, 720);
			this.panel1.TabIndex = 4;
			// 
			// statusBar
			// 
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Coordinates,
            this.Angle,
            this.PenStatus,
            this.PenColor,
            this.PenSize});
			this.statusBar.Location = new System.Drawing.Point(101, 698);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(912, 22);
			this.statusBar.TabIndex = 6;
			this.statusBar.Text = "statusStrip1";
			// 
			// panelSurface
			// 
			this.panelSurface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelSurface.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelSurface.Location = new System.Drawing.Point(101, 0);
			this.panelSurface.Name = "panelSurface";
			this.panelSurface.Size = new System.Drawing.Size(912, 698);
			this.panelSurface.TabIndex = 7;
			// 
			// Coordinates
			// 
			this.Coordinates.Name = "Coordinates";
			this.Coordinates.Size = new System.Drawing.Size(65, 17);
			this.Coordinates.Text = "Coordinates";
			// 
			// Angle
			// 
			this.Angle.Name = "Angle";
			this.Angle.Size = new System.Drawing.Size(34, 17);
			this.Angle.Text = "Angle";
			// 
			// buttonSetAngle
			// 
			this.buttonSetAngle.Location = new System.Drawing.Point(12, 174);
			this.buttonSetAngle.Name = "buttonSetAngle";
			this.buttonSetAngle.Size = new System.Drawing.Size(78, 35);
			this.buttonSetAngle.TabIndex = 3;
			this.buttonSetAngle.Text = "&Angle";
			this.buttonSetAngle.UseVisualStyleBackColor = true;
			this.buttonSetAngle.Click += new System.EventHandler(this.buttonSetAngle_Click);
			// 
			// buttonPenStatus
			// 
			this.buttonPenStatus.Location = new System.Drawing.Point(11, 215);
			this.buttonPenStatus.Name = "buttonPenStatus";
			this.buttonPenStatus.Size = new System.Drawing.Size(78, 34);
			this.buttonPenStatus.TabIndex = 4;
			this.buttonPenStatus.Text = "Pen &Up";
			this.buttonPenStatus.UseVisualStyleBackColor = true;
			this.buttonPenStatus.Click += new System.EventHandler(this.buttonPenStatus_Click);
			// 
			// PenStatus
			// 
			this.PenStatus.Name = "PenStatus";
			this.PenStatus.Size = new System.Drawing.Size(56, 17);
			this.PenStatus.Text = "PenStatus";
			// 
			// buttonColor
			// 
			this.buttonColor.Location = new System.Drawing.Point(11, 255);
			this.buttonColor.Name = "buttonColor";
			this.buttonColor.Size = new System.Drawing.Size(78, 35);
			this.buttonColor.TabIndex = 5;
			this.buttonColor.Text = "Pen &Color";
			this.buttonColor.UseVisualStyleBackColor = true;
			this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
			// 
			// PenColor
			// 
			this.PenColor.Name = "PenColor";
			this.PenColor.Size = new System.Drawing.Size(50, 17);
			this.PenColor.Text = "PenColor";
			// 
			// buttonPenSize
			// 
			this.buttonPenSize.Location = new System.Drawing.Point(11, 296);
			this.buttonPenSize.Name = "buttonPenSize";
			this.buttonPenSize.Size = new System.Drawing.Size(78, 35);
			this.buttonPenSize.TabIndex = 6;
			this.buttonPenSize.Text = "Pen &Size";
			this.buttonPenSize.UseVisualStyleBackColor = true;
			this.buttonPenSize.Click += new System.EventHandler(this.buttonPenSize_Click);
			// 
			// PenSize
			// 
			this.PenSize.Name = "PenSize";
			this.PenSize.Size = new System.Drawing.Size(44, 17);
			this.PenSize.Text = "PenSize";
			// 
			// buttonForward
			// 
			this.buttonForward.Location = new System.Drawing.Point(11, 337);
			this.buttonForward.Name = "buttonForward";
			this.buttonForward.Size = new System.Drawing.Size(78, 35);
			this.buttonForward.TabIndex = 7;
			this.buttonForward.Text = "&Forward";
			this.buttonForward.UseVisualStyleBackColor = true;
			this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
			// 
			// buttonBackward
			// 
			this.buttonBackward.Location = new System.Drawing.Point(12, 378);
			this.buttonBackward.Name = "buttonBackward";
			this.buttonBackward.Size = new System.Drawing.Size(78, 35);
			this.buttonBackward.TabIndex = 8;
			this.buttonBackward.Text = "&Backward";
			this.buttonBackward.UseVisualStyleBackColor = true;
			this.buttonBackward.Click += new System.EventHandler(this.buttonBackward_Click);
			// 
			// DemoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1013, 720);
			this.Controls.Add(this.panelSurface);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.panel1);
			this.Name = "DemoForm";
			this.Text = "Nakov.TurtleGraphics - Demo";
			this.Load += new System.EventHandler(this.DemoForm_Load);
			this.panel1.ResumeLayout(false);
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonDraw;
		private System.Windows.Forms.Button buttonDrawSpiral;
		private System.Windows.Forms.Button buttonReset;
		private System.Windows.Forms.Button buttonShowHideTurtle;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.ToolStripStatusLabel Coordinates;
		private System.Windows.Forms.ToolStripStatusLabel Angle;
		private System.Windows.Forms.Panel panelSurface;
		private System.Windows.Forms.Button buttonSetAngle;
		private System.Windows.Forms.Button buttonPenStatus;
		private System.Windows.Forms.ToolStripStatusLabel PenStatus;
		private System.Windows.Forms.Button buttonColor;
		private System.Windows.Forms.ToolStripStatusLabel PenColor;
		private System.ServiceProcess.ServiceController serviceController1;
		private System.Windows.Forms.Button buttonPenSize;
		private System.Windows.Forms.ToolStripStatusLabel PenSize;
		private System.Windows.Forms.Button buttonForward;
		private System.Windows.Forms.Button buttonBackward;
	}
}