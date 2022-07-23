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
			this.panelSurface = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.panel2 = new System.Windows.Forms.Panel();
			this.buttonExecute = new System.Windows.Forms.Button();
			this.txtScript = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtScriptTokenizedDecompiled = new System.Windows.Forms.TextBox();
			this.txtScriptTokenized = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.buttonRunTokenized = new System.Windows.Forms.Button();
			this.buttonLoadTokenized = new System.Windows.Forms.Button();
			this.buttonSaveTokenized = new System.Windows.Forms.Button();
			this.buttonShowTokens = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.statusBar.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
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
			this.buttonReset.Location = new System.Drawing.Point(11, 91);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.Size = new System.Drawing.Size(78, 35);
			this.buttonReset.TabIndex = 1;
			this.buttonReset.Text = "Reset";
			this.buttonReset.UseVisualStyleBackColor = true;
			this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
			// 
			// buttonShowHideTurtle
			// 
			this.buttonShowHideTurtle.Location = new System.Drawing.Point(11, 132);
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
			this.panel1.Size = new System.Drawing.Size(101, 716);
			this.panel1.TabIndex = 4;
			// 
			// buttonBackward
			// 
			this.buttonBackward.Location = new System.Drawing.Point(11, 376);
			this.buttonBackward.Name = "buttonBackward";
			this.buttonBackward.Size = new System.Drawing.Size(78, 35);
			this.buttonBackward.TabIndex = 8;
			this.buttonBackward.Text = "&Backward";
			this.buttonBackward.UseVisualStyleBackColor = true;
			this.buttonBackward.Click += new System.EventHandler(this.buttonBackward_Click);
			// 
			// buttonForward
			// 
			this.buttonForward.Location = new System.Drawing.Point(11, 335);
			this.buttonForward.Name = "buttonForward";
			this.buttonForward.Size = new System.Drawing.Size(78, 35);
			this.buttonForward.TabIndex = 7;
			this.buttonForward.Text = "&Forward";
			this.buttonForward.UseVisualStyleBackColor = true;
			this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
			// 
			// buttonPenSize
			// 
			this.buttonPenSize.Location = new System.Drawing.Point(11, 294);
			this.buttonPenSize.Name = "buttonPenSize";
			this.buttonPenSize.Size = new System.Drawing.Size(78, 35);
			this.buttonPenSize.TabIndex = 6;
			this.buttonPenSize.Text = "Pen &Size";
			this.buttonPenSize.UseVisualStyleBackColor = true;
			this.buttonPenSize.Click += new System.EventHandler(this.buttonPenSize_Click);
			// 
			// buttonColor
			// 
			this.buttonColor.Location = new System.Drawing.Point(11, 253);
			this.buttonColor.Name = "buttonColor";
			this.buttonColor.Size = new System.Drawing.Size(78, 35);
			this.buttonColor.TabIndex = 5;
			this.buttonColor.Text = "Pen &Color";
			this.buttonColor.UseVisualStyleBackColor = true;
			this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
			// 
			// buttonPenStatus
			// 
			this.buttonPenStatus.Location = new System.Drawing.Point(11, 213);
			this.buttonPenStatus.Name = "buttonPenStatus";
			this.buttonPenStatus.Size = new System.Drawing.Size(78, 34);
			this.buttonPenStatus.TabIndex = 4;
			this.buttonPenStatus.Text = "Pen &Up";
			this.buttonPenStatus.UseVisualStyleBackColor = true;
			this.buttonPenStatus.Click += new System.EventHandler(this.buttonPenStatus_Click);
			// 
			// buttonSetAngle
			// 
			this.buttonSetAngle.Location = new System.Drawing.Point(11, 172);
			this.buttonSetAngle.Name = "buttonSetAngle";
			this.buttonSetAngle.Size = new System.Drawing.Size(78, 35);
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
			this.statusBar.Location = new System.Drawing.Point(101, 694);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(914, 22);
			this.statusBar.TabIndex = 6;
			this.statusBar.Text = "statusStrip1";
			// 
			// Coordinates
			// 
			this.Coordinates.Name = "Coordinates";
			this.Coordinates.Size = new System.Drawing.Size(71, 17);
			this.Coordinates.Text = "Coordinates";
			// 
			// Angle
			// 
			this.Angle.Name = "Angle";
			this.Angle.Size = new System.Drawing.Size(38, 17);
			this.Angle.Text = "Angle";
			// 
			// PenStatus
			// 
			this.PenStatus.Name = "PenStatus";
			this.PenStatus.Size = new System.Drawing.Size(59, 17);
			this.PenStatus.Text = "PenStatus";
			// 
			// PenColor
			// 
			this.PenColor.Name = "PenColor";
			this.PenColor.Size = new System.Drawing.Size(56, 17);
			this.PenColor.Text = "PenColor";
			// 
			// PenSize
			// 
			this.PenSize.Name = "PenSize";
			this.PenSize.Size = new System.Drawing.Size(47, 17);
			this.PenSize.Text = "PenSize";
			// 
			// panelSurface
			// 
			this.panelSurface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelSurface.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelSurface.Location = new System.Drawing.Point(101, 0);
			this.panelSurface.Name = "panelSurface";
			this.panelSurface.Size = new System.Drawing.Size(914, 499);
			this.panelSurface.TabIndex = 9;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tabControl1.Location = new System.Drawing.Point(101, 499);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(914, 195);
			this.tabControl1.TabIndex = 10;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.panel2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(906, 169);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Direct Interpretation";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.tableLayoutPanel1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(906, 169);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Tokenized Interpretation";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.buttonExecute);
			this.panel2.Controls.Add(this.txtScript);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(900, 163);
			this.panel2.TabIndex = 9;
			// 
			// buttonExecute
			// 
			this.buttonExecute.Dock = System.Windows.Forms.DockStyle.Right;
			this.buttonExecute.Location = new System.Drawing.Point(825, 0);
			this.buttonExecute.Name = "buttonExecute";
			this.buttonExecute.Size = new System.Drawing.Size(75, 163);
			this.buttonExecute.TabIndex = 1;
			this.buttonExecute.Text = "&Run";
			this.buttonExecute.UseVisualStyleBackColor = true;
			this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
			// 
			// txtScript
			// 
			this.txtScript.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtScript.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtScript.Location = new System.Drawing.Point(0, 0);
			this.txtScript.Multiline = true;
			this.txtScript.Name = "txtScript";
			this.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtScript.Size = new System.Drawing.Size(900, 163);
			this.txtScript.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.tableLayoutPanel1.Controls.Add(this.txtScriptTokenized, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtScriptTokenizedDecompiled, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(900, 163);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// txtScriptTokenizedDecompiled
			// 
			this.txtScriptTokenizedDecompiled.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtScriptTokenizedDecompiled.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtScriptTokenizedDecompiled.Location = new System.Drawing.Point(415, 31);
			this.txtScriptTokenizedDecompiled.Multiline = true;
			this.txtScriptTokenizedDecompiled.Name = "txtScriptTokenizedDecompiled";
			this.txtScriptTokenizedDecompiled.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtScriptTokenizedDecompiled.Size = new System.Drawing.Size(406, 129);
			this.txtScriptTokenizedDecompiled.TabIndex = 1;
			// 
			// txtScriptTokenized
			// 
			this.txtScriptTokenized.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtScriptTokenized.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtScriptTokenized.Location = new System.Drawing.Point(3, 31);
			this.txtScriptTokenized.Multiline = true;
			this.txtScriptTokenized.Name = "txtScriptTokenized";
			this.txtScriptTokenized.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtScriptTokenized.Size = new System.Drawing.Size(406, 129);
			this.txtScriptTokenized.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(406, 28);
			this.label1.TabIndex = 3;
			this.label1.Text = "User Entered Script";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(415, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(406, 28);
			this.label2.TabIndex = 4;
			this.label2.Text = "Decompiled Script";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.buttonShowTokens, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.buttonSaveTokenized, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.buttonLoadTokenized, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.buttonRunTokenized, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(827, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 4;
			this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(70, 157);
			this.tableLayoutPanel2.TabIndex = 5;
			// 
			// buttonRunTokenized
			// 
			this.buttonRunTokenized.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonRunTokenized.Location = new System.Drawing.Point(3, 3);
			this.buttonRunTokenized.Name = "buttonRunTokenized";
			this.buttonRunTokenized.Size = new System.Drawing.Size(64, 33);
			this.buttonRunTokenized.TabIndex = 2;
			this.buttonRunTokenized.Text = "&Run";
			this.buttonRunTokenized.UseVisualStyleBackColor = true;
			this.buttonRunTokenized.Click += new System.EventHandler(this.buttonRunTokenized_Click);
			// 
			// buttonLoadTokenized
			// 
			this.buttonLoadTokenized.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonLoadTokenized.Location = new System.Drawing.Point(3, 42);
			this.buttonLoadTokenized.Name = "buttonLoadTokenized";
			this.buttonLoadTokenized.Size = new System.Drawing.Size(64, 33);
			this.buttonLoadTokenized.TabIndex = 3;
			this.buttonLoadTokenized.Text = "&Load";
			this.buttonLoadTokenized.UseVisualStyleBackColor = true;
			// 
			// buttonSaveTokenized
			// 
			this.buttonSaveTokenized.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonSaveTokenized.Location = new System.Drawing.Point(3, 81);
			this.buttonSaveTokenized.Name = "buttonSaveTokenized";
			this.buttonSaveTokenized.Size = new System.Drawing.Size(64, 33);
			this.buttonSaveTokenized.TabIndex = 4;
			this.buttonSaveTokenized.Text = "&Save";
			this.buttonSaveTokenized.UseVisualStyleBackColor = true;
			// 
			// buttonShowTokens
			// 
			this.buttonShowTokens.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonShowTokens.Location = new System.Drawing.Point(3, 120);
			this.buttonShowTokens.Name = "buttonShowTokens";
			this.buttonShowTokens.Size = new System.Drawing.Size(64, 34);
			this.buttonShowTokens.TabIndex = 5;
			this.buttonShowTokens.Text = "&Tokens";
			this.buttonShowTokens.UseVisualStyleBackColor = true;
			// 
			// ScriptedTurtleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1015, 716);
			this.Controls.Add(this.panelSurface);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.panel1);
			this.Name = "ScriptedTurtleForm";
			this.Text = "Nakov.TurtleGraphics - Demo";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.DemoForm_Load);
			this.panel1.ResumeLayout(false);
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
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
		private System.Windows.Forms.Panel panelSurface;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button buttonExecute;
		private System.Windows.Forms.TextBox txtScript;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox txtScriptTokenized;
		private System.Windows.Forms.TextBox txtScriptTokenizedDecompiled;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button buttonSaveTokenized;
		private System.Windows.Forms.Button buttonLoadTokenized;
		private System.Windows.Forms.Button buttonRunTokenized;
		private System.Windows.Forms.Button buttonShowTokens;
	}
}