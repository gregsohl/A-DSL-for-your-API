namespace ScriptedTurtle
{
	partial class ScriptedTurtleForm
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
            this.buttonBackward = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonPenSize = new System.Windows.Forms.Button();
            this.buttonColor = new System.Windows.Forms.Button();
            this.buttonPenStatus = new System.Windows.Forms.Button();
            this.buttonSetAngle = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.Coordinates = new System.Windows.Forms.ToolStripStatusLabel();
            this.Angle = new System.Windows.Forms.ToolStripStatusLabel();
            this.PenStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.PenColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.PenSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.serviceController1 = new System.ServiceProcess.ServiceController();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.txtScript = new System.Windows.Forms.TextBox();
            this.panelSurface = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDraw
            // 
            this.buttonDraw.Location = new System.Drawing.Point(15, 11);
            this.buttonDraw.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(104, 43);
            this.buttonDraw.TabIndex = 0;
            this.buttonDraw.Text = "Draw";
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // buttonDrawSpiral
            // 
            this.buttonDrawSpiral.Location = new System.Drawing.Point(15, 62);
            this.buttonDrawSpiral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonDrawSpiral.Name = "buttonDrawSpiral";
            this.buttonDrawSpiral.Size = new System.Drawing.Size(104, 43);
            this.buttonDrawSpiral.TabIndex = 0;
            this.buttonDrawSpiral.Text = "Spiral";
            this.buttonDrawSpiral.UseVisualStyleBackColor = true;
            this.buttonDrawSpiral.Click += new System.EventHandler(this.buttonDrawSpiral_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(15, 112);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(104, 43);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonShowHideTurtle
            // 
            this.buttonShowHideTurtle.Location = new System.Drawing.Point(15, 162);
            this.buttonShowHideTurtle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonShowHideTurtle.Name = "buttonShowHideTurtle";
            this.buttonShowHideTurtle.Size = new System.Drawing.Size(104, 42);
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
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(135, 885);
            this.panel1.TabIndex = 4;
            // 
            // buttonBackward
            // 
            this.buttonBackward.Location = new System.Drawing.Point(15, 463);
            this.buttonBackward.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonBackward.Name = "buttonBackward";
            this.buttonBackward.Size = new System.Drawing.Size(104, 43);
            this.buttonBackward.TabIndex = 8;
            this.buttonBackward.Text = "&Backward";
            this.buttonBackward.UseVisualStyleBackColor = true;
            this.buttonBackward.Click += new System.EventHandler(this.buttonBackward_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.Location = new System.Drawing.Point(15, 412);
            this.buttonForward.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(104, 43);
            this.buttonForward.TabIndex = 7;
            this.buttonForward.Text = "&Forward";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonPenSize
            // 
            this.buttonPenSize.Location = new System.Drawing.Point(15, 362);
            this.buttonPenSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonPenSize.Name = "buttonPenSize";
            this.buttonPenSize.Size = new System.Drawing.Size(104, 43);
            this.buttonPenSize.TabIndex = 6;
            this.buttonPenSize.Text = "Pen &Size";
            this.buttonPenSize.UseVisualStyleBackColor = true;
            this.buttonPenSize.Click += new System.EventHandler(this.buttonPenSize_Click);
            // 
            // buttonColor
            // 
            this.buttonColor.Location = new System.Drawing.Point(15, 311);
            this.buttonColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(104, 43);
            this.buttonColor.TabIndex = 5;
            this.buttonColor.Text = "Pen &Color";
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
            // 
            // buttonPenStatus
            // 
            this.buttonPenStatus.Location = new System.Drawing.Point(15, 262);
            this.buttonPenStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonPenStatus.Name = "buttonPenStatus";
            this.buttonPenStatus.Size = new System.Drawing.Size(104, 42);
            this.buttonPenStatus.TabIndex = 4;
            this.buttonPenStatus.Text = "Pen &Up";
            this.buttonPenStatus.UseVisualStyleBackColor = true;
            this.buttonPenStatus.Click += new System.EventHandler(this.buttonPenStatus_Click);
            // 
            // buttonSetAngle
            // 
            this.buttonSetAngle.Location = new System.Drawing.Point(15, 212);
            this.buttonSetAngle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSetAngle.Name = "buttonSetAngle";
            this.buttonSetAngle.Size = new System.Drawing.Size(104, 43);
            this.buttonSetAngle.TabIndex = 3;
            this.buttonSetAngle.Text = "&Angle";
            this.buttonSetAngle.UseVisualStyleBackColor = true;
            this.buttonSetAngle.Click += new System.EventHandler(this.buttonSetAngle_Click);
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Coordinates,
            this.Angle,
            this.PenStatus,
            this.PenColor,
            this.PenSize});
            this.statusBar.Location = new System.Drawing.Point(135, 860);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusBar.Size = new System.Drawing.Size(1218, 25);
            this.statusBar.TabIndex = 6;
            this.statusBar.Text = "statusStrip1";
            // 
            // Coordinates
            // 
            this.Coordinates.Name = "Coordinates";
            this.Coordinates.Size = new System.Drawing.Size(89, 20);
            this.Coordinates.Text = "Coordinates";
            // 
            // Angle
            // 
            this.Angle.Name = "Angle";
            this.Angle.Size = new System.Drawing.Size(48, 20);
            this.Angle.Text = "Angle";
            // 
            // PenStatus
            // 
            this.PenStatus.Name = "PenStatus";
            this.PenStatus.Size = new System.Drawing.Size(73, 20);
            this.PenStatus.Text = "PenStatus";
            // 
            // PenColor
            // 
            this.PenColor.Name = "PenColor";
            this.PenColor.Size = new System.Drawing.Size(69, 20);
            this.PenColor.Text = "PenColor";
            // 
            // PenSize
            // 
            this.PenSize.Name = "PenSize";
            this.PenSize.Size = new System.Drawing.Size(60, 20);
            this.PenSize.Text = "PenSize";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonExecute);
            this.panel2.Controls.Add(this.txtScript);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(135, 651);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1218, 209);
            this.panel2.TabIndex = 8;
            // 
            // buttonExecute
            // 
            this.buttonExecute.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonExecute.Location = new System.Drawing.Point(1118, 0);
            this.buttonExecute.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(100, 209);
            this.buttonExecute.TabIndex = 1;
            this.buttonExecute.Text = "&Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // txtScript
            // 
            this.txtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScript.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScript.Location = new System.Drawing.Point(0, 0);
            this.txtScript.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtScript.Multiline = true;
            this.txtScript.Name = "txtScript";
            this.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtScript.Size = new System.Drawing.Size(1218, 209);
            this.txtScript.TabIndex = 0;
            // 
            // panelSurface
            // 
            this.panelSurface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSurface.Location = new System.Drawing.Point(135, 0);
            this.panelSurface.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSurface.Name = "panelSurface";
            this.panelSurface.Size = new System.Drawing.Size(1218, 651);
            this.panelSurface.TabIndex = 9;
            // 
            // ScriptedTurtleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 885);
            this.Controls.Add(this.panelSurface);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ScriptedTurtleForm";
            this.Text = "Nakov.TurtleGraphics - Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DemoForm_Load);
            this.panel1.ResumeLayout(false);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button buttonExecute;
		private System.Windows.Forms.TextBox txtScript;
		private System.Windows.Forms.Panel panelSurface;
	}
}