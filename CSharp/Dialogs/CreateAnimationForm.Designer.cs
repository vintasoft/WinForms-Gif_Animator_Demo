namespace GifAnimatorDemo
{
    partial class CreateAnimationForm
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
            Vintasoft.Imaging.Utils.WinFormsSystemClipboard winFormsSystemClipboard1 = new Vintasoft.Imaging.Utils.WinFormsSystemClipboard();
            Vintasoft.Imaging.Codecs.Decoders.RenderingSettings renderingSettings1 = new Vintasoft.Imaging.Codecs.Decoders.RenderingSettings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAnimationForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.animatedImageViewer1 = new Vintasoft.Imaging.UI.AnimatedImageViewer();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.delayNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel6 = new System.Windows.Forms.Panel();
            this.animationStartButton = new System.Windows.Forms.Button();
            this.animationStopButton = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.methodsTabControl = new System.Windows.Forms.TabControl();
            this.colorBlendingTabPage = new System.Windows.Forms.TabPage();
            this.secondColorPanelControl = new DemosCommonCode.CustomControls.ColorPanelControl();
            this.firstColorPanelControl = new DemosCommonCode.CustomControls.ColorPanelControl();
            this.colorBlendingComboBox = new System.Windows.Forms.ComboBox();
            this.pixelateTabPage = new System.Windows.Forms.TabPage();
            this.pixelateDeltaGroupBox = new System.Windows.Forms.GroupBox();
            this.pixelateDeltaTrackBar = new System.Windows.Forms.TrackBar();
            this.animationControlsPanel = new System.Windows.Forms.Panel();
            this.reverseCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.framesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayNumericUpDown)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.methodsTabControl.SuspendLayout();
            this.colorBlendingTabPage.SuspendLayout();
            this.pixelateTabPage.SuspendLayout();
            this.pixelateDeltaGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pixelateDeltaTrackBar)).BeginInit();
            this.animationControlsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.framesNumericUpDown)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 348);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(444, 313);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(442, 311);
            this.panel4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(213, 311);
            this.panel5.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.animatedImageViewer1);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 32);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(1);
            this.panel8.Size = new System.Drawing.Size(213, 251);
            this.panel8.TabIndex = 2;
            // 
            // animatedImageViewer1
            // 
            this.animatedImageViewer1.AnimationRepeat = true;
            this.animatedImageViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.animatedImageViewer1.Clipboard = winFormsSystemClipboard1;
            this.animatedImageViewer1.DefaultDelay = 1000;
            this.animatedImageViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.animatedImageViewer1.ImageRenderingSettings = renderingSettings1;
            this.animatedImageViewer1.ImageRotationAngle = 0;
            this.animatedImageViewer1.Location = new System.Drawing.Point(1, 1);
            this.animatedImageViewer1.Name = "animatedImageViewer1";
            this.animatedImageViewer1.Size = new System.Drawing.Size(211, 249);
            this.animatedImageViewer1.TabIndex = 0;
            this.animatedImageViewer1.Text = "animatedImageViewer1";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.delayNumericUpDown);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 283);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(213, 28);
            this.panel7.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "1/100 sec";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Delay";
            // 
            // delayNumericUpDown
            // 
            this.delayNumericUpDown.Location = new System.Drawing.Point(69, 4);
            this.delayNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.delayNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.delayNumericUpDown.Name = "delayNumericUpDown";
            this.delayNumericUpDown.Size = new System.Drawing.Size(71, 20);
            this.delayNumericUpDown.TabIndex = 1;
            this.delayNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.delayNumericUpDown.ValueChanged += new System.EventHandler(this.delayNumericUpDown_ValueChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.animationStartButton);
            this.panel6.Controls.Add(this.animationStopButton);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(213, 32);
            this.panel6.TabIndex = 0;
            // 
            // animationStartButton
            // 
            this.animationStartButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.animationStartButton.Image = ((System.Drawing.Image)(resources.GetObject("animationStartButton.Image")));
            this.animationStartButton.Location = new System.Drawing.Point(93, 3);
            this.animationStartButton.Name = "animationStartButton";
            this.animationStartButton.Size = new System.Drawing.Size(26, 26);
            this.animationStartButton.TabIndex = 3;
            this.animationStartButton.UseVisualStyleBackColor = true;
            this.animationStartButton.Click += new System.EventHandler(this.animationStartButton_Click);
            // 
            // animationStopButton
            // 
            this.animationStopButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.animationStopButton.Image = ((System.Drawing.Image)(resources.GetObject("animationStopButton.Image")));
            this.animationStopButton.Location = new System.Drawing.Point(93, 3);
            this.animationStopButton.Name = "animationStopButton";
            this.animationStopButton.Size = new System.Drawing.Size(26, 26);
            this.animationStopButton.TabIndex = 7;
            this.animationStopButton.UseVisualStyleBackColor = true;
            this.animationStopButton.Visible = false;
            this.animationStopButton.Click += new System.EventHandler(this.animationStopButton_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Controls.Add(this.animationControlsPanel);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(213, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(229, 311);
            this.panel9.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.methodsTabControl);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(229, 283);
            this.panel10.TabIndex = 1;
            // 
            // methodsTabControl
            // 
            this.methodsTabControl.Controls.Add(this.colorBlendingTabPage);
            this.methodsTabControl.Controls.Add(this.pixelateTabPage);
            this.methodsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.methodsTabControl.Location = new System.Drawing.Point(0, 0);
            this.methodsTabControl.Name = "methodsTabControl";
            this.methodsTabControl.SelectedIndex = 0;
            this.methodsTabControl.Size = new System.Drawing.Size(229, 283);
            this.methodsTabControl.TabIndex = 0;
            // 
            // colorBlendingTabPage
            // 
            this.colorBlendingTabPage.Controls.Add(this.secondColorPanelControl);
            this.colorBlendingTabPage.Controls.Add(this.firstColorPanelControl);
            this.colorBlendingTabPage.Controls.Add(this.colorBlendingComboBox);
            this.colorBlendingTabPage.Location = new System.Drawing.Point(4, 22);
            this.colorBlendingTabPage.Name = "colorBlendingTabPage";
            this.colorBlendingTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.colorBlendingTabPage.Size = new System.Drawing.Size(221, 257);
            this.colorBlendingTabPage.TabIndex = 0;
            this.colorBlendingTabPage.Text = "Color Blending";
            this.colorBlendingTabPage.UseVisualStyleBackColor = true;
            // 
            // secondColorPanelControl
            // 
            this.secondColorPanelControl.CanEditAlphaChannel = false;
            this.secondColorPanelControl.Color = System.Drawing.Color.Red;
            this.secondColorPanelControl.ColorButtonMargin = 6;
            this.secondColorPanelControl.ColorButtonText = "Second Color...";
            this.secondColorPanelControl.ColorButtonWidth = 98;
            this.secondColorPanelControl.ColorRightToLeft = true;
            this.secondColorPanelControl.DefaultColor = System.Drawing.Color.Empty;
            this.secondColorPanelControl.DefaultColorButtonMargin = 0;
            this.secondColorPanelControl.Location = new System.Drawing.Point(6, 68);
            this.secondColorPanelControl.Name = "secondColorPanelControl";
            this.secondColorPanelControl.Size = new System.Drawing.Size(123, 23);
            this.secondColorPanelControl.TabIndex = 6;
            // 
            // firstColorPanelControl
            // 
            this.firstColorPanelControl.CanEditAlphaChannel = false;
            this.firstColorPanelControl.Color = System.Drawing.Color.Lime;
            this.firstColorPanelControl.ColorButtonMargin = 6;
            this.firstColorPanelControl.ColorButtonText = "First Color...";
            this.firstColorPanelControl.ColorButtonWidth = 98;
            this.firstColorPanelControl.ColorRightToLeft = true;
            this.firstColorPanelControl.DefaultColor = System.Drawing.Color.Empty;
            this.firstColorPanelControl.DefaultColorButtonMargin = 0;
            this.firstColorPanelControl.Location = new System.Drawing.Point(6, 42);
            this.firstColorPanelControl.Name = "firstColorPanelControl";
            this.firstColorPanelControl.Size = new System.Drawing.Size(123, 23);
            this.firstColorPanelControl.TabIndex = 5;
            // 
            // colorBlendingComboBox
            // 
            this.colorBlendingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorBlendingComboBox.FormattingEnabled = true;
            this.colorBlendingComboBox.Location = new System.Drawing.Point(6, 11);
            this.colorBlendingComboBox.Name = "colorBlendingComboBox";
            this.colorBlendingComboBox.Size = new System.Drawing.Size(208, 21);
            this.colorBlendingComboBox.TabIndex = 4;
            // 
            // pixelateTabPage
            // 
            this.pixelateTabPage.Controls.Add(this.pixelateDeltaGroupBox);
            this.pixelateTabPage.Location = new System.Drawing.Point(4, 22);
            this.pixelateTabPage.Name = "pixelateTabPage";
            this.pixelateTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.pixelateTabPage.Size = new System.Drawing.Size(221, 257);
            this.pixelateTabPage.TabIndex = 1;
            this.pixelateTabPage.Text = "Pixelate";
            this.pixelateTabPage.UseVisualStyleBackColor = true;
            // 
            // pixelateDeltaGroupBox
            // 
            this.pixelateDeltaGroupBox.Controls.Add(this.pixelateDeltaTrackBar);
            this.pixelateDeltaGroupBox.Location = new System.Drawing.Point(6, 6);
            this.pixelateDeltaGroupBox.Name = "pixelateDeltaGroupBox";
            this.pixelateDeltaGroupBox.Size = new System.Drawing.Size(209, 51);
            this.pixelateDeltaGroupBox.TabIndex = 3;
            this.pixelateDeltaGroupBox.TabStop = false;
            this.pixelateDeltaGroupBox.Text = "Delta";
            // 
            // pixelateDeltaTrackBar
            // 
            this.pixelateDeltaTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pixelateDeltaTrackBar.Location = new System.Drawing.Point(3, 16);
            this.pixelateDeltaTrackBar.Minimum = 1;
            this.pixelateDeltaTrackBar.Name = "pixelateDeltaTrackBar";
            this.pixelateDeltaTrackBar.Size = new System.Drawing.Size(203, 32);
            this.pixelateDeltaTrackBar.TabIndex = 2;
            this.pixelateDeltaTrackBar.Value = 1;
            // 
            // animationControlsPanel
            // 
            this.animationControlsPanel.Controls.Add(this.reverseCheckBox);
            this.animationControlsPanel.Controls.Add(this.label1);
            this.animationControlsPanel.Controls.Add(this.framesNumericUpDown);
            this.animationControlsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.animationControlsPanel.Location = new System.Drawing.Point(0, 283);
            this.animationControlsPanel.Name = "animationControlsPanel";
            this.animationControlsPanel.Size = new System.Drawing.Size(229, 28);
            this.animationControlsPanel.TabIndex = 0;
            // 
            // reverseCheckBox
            // 
            this.reverseCheckBox.AutoSize = true;
            this.reverseCheckBox.Location = new System.Drawing.Point(145, 8);
            this.reverseCheckBox.Name = "reverseCheckBox";
            this.reverseCheckBox.Size = new System.Drawing.Size(66, 17);
            this.reverseCheckBox.TabIndex = 2;
            this.reverseCheckBox.Text = "Reverse";
            this.reverseCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Frames";
            // 
            // framesNumericUpDown
            // 
            this.framesNumericUpDown.Location = new System.Drawing.Point(50, 5);
            this.framesNumericUpDown.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.framesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.framesNumericUpDown.Name = "framesNumericUpDown";
            this.framesNumericUpDown.Size = new System.Drawing.Size(71, 20);
            this.framesNumericUpDown.TabIndex = 0;
            this.framesNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonOk);
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 313);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(444, 35);
            this.panel2.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonOk.Location = new System.Drawing.Point(137, 6);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(218, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // CreateAnimationForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(444, 348);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(460, 360);
            this.Name = "CreateAnimationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Animation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateAnimationDialog_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayNumericUpDown)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.methodsTabControl.ResumeLayout(false);
            this.colorBlendingTabPage.ResumeLayout(false);
            this.pixelateTabPage.ResumeLayout(false);
            this.pixelateDeltaGroupBox.ResumeLayout(false);
            this.pixelateDeltaGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pixelateDeltaTrackBar)).EndInit();
            this.animationControlsPanel.ResumeLayout(false);
            this.animationControlsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.framesNumericUpDown)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel8;
        private Vintasoft.Imaging.UI.AnimatedImageViewer animatedImageViewer1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown delayNumericUpDown;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button animationStartButton;
        private System.Windows.Forms.Button animationStopButton;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TabControl methodsTabControl;
        private System.Windows.Forms.TabPage colorBlendingTabPage;
        private System.Windows.Forms.TabPage pixelateTabPage;
        private System.Windows.Forms.Panel animationControlsPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown framesNumericUpDown;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox pixelateDeltaGroupBox;
        private System.Windows.Forms.TrackBar pixelateDeltaTrackBar;
        private System.Windows.Forms.CheckBox reverseCheckBox;
        private System.Windows.Forms.ComboBox colorBlendingComboBox;
        private DemosCommonCode.CustomControls.ColorPanelControl secondColorPanelControl;
        private DemosCommonCode.CustomControls.ColorPanelControl firstColorPanelControl;


    }
}