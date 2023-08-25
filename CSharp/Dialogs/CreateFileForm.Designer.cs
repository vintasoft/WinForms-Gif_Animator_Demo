namespace GifAnimatorDemo
{
	partial class CreateFileForm
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.sizeGroupBox = new System.Windows.Forms.GroupBox();
            this.addPagesToNewFileCheckBox = new System.Windows.Forms.CheckBox();
            this.heightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.widthLabel = new System.Windows.Forms.Label();
            this.specifiedSizeRadioButton = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.fromImagesRadioButton = new System.Windows.Forms.RadioButton();
            this.openImageFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.sizeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(51, 104);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(85, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // sizeGroupBox
            // 
            this.sizeGroupBox.Controls.Add(this.addPagesToNewFileCheckBox);
            this.sizeGroupBox.Controls.Add(this.heightNumericUpDown);
            this.sizeGroupBox.Controls.Add(this.heightLabel);
            this.sizeGroupBox.Controls.Add(this.widthNumericUpDown);
            this.sizeGroupBox.Controls.Add(this.widthLabel);
            this.sizeGroupBox.Enabled = false;
            this.sizeGroupBox.Location = new System.Drawing.Point(6, 29);
            this.sizeGroupBox.Name = "sizeGroupBox";
            this.sizeGroupBox.Size = new System.Drawing.Size(260, 66);
            this.sizeGroupBox.TabIndex = 13;
            this.sizeGroupBox.TabStop = false;
            // 
            // addPagesToNewFileCheckBox
            // 
            this.addPagesToNewFileCheckBox.AutoSize = true;
            this.addPagesToNewFileCheckBox.Checked = true;
            this.addPagesToNewFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.addPagesToNewFileCheckBox.Location = new System.Drawing.Point(6, 43);
            this.addPagesToNewFileCheckBox.Name = "addPagesToNewFileCheckBox";
            this.addPagesToNewFileCheckBox.Size = new System.Drawing.Size(128, 17);
            this.addPagesToNewFileCheckBox.TabIndex = 13;
            this.addPagesToNewFileCheckBox.Text = "Add pages to new file";
            this.addPagesToNewFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // heightNumericUpDown
            // 
            this.heightNumericUpDown.Location = new System.Drawing.Point(180, 18);
            this.heightNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.heightNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.heightNumericUpDown.Name = "heightNumericUpDown";
            this.heightNumericUpDown.Size = new System.Drawing.Size(64, 20);
            this.heightNumericUpDown.TabIndex = 12;
            this.heightNumericUpDown.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(136, 20);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(38, 13);
            this.heightLabel.TabIndex = 11;
            this.heightLabel.Text = "Height";
            // 
            // widthNumericUpDown
            // 
            this.widthNumericUpDown.Location = new System.Drawing.Point(61, 18);
            this.widthNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.widthNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.widthNumericUpDown.Name = "widthNumericUpDown";
            this.widthNumericUpDown.Size = new System.Drawing.Size(64, 20);
            this.widthNumericUpDown.TabIndex = 10;
            this.widthNumericUpDown.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(17, 20);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(35, 13);
            this.widthLabel.TabIndex = 9;
            this.widthLabel.Text = "Width";
            // 
            // specifiedSizeRadioButton
            // 
            this.specifiedSizeRadioButton.AutoSize = true;
            this.specifiedSizeRadioButton.Location = new System.Drawing.Point(12, 25);
            this.specifiedSizeRadioButton.Name = "specifiedSizeRadioButton";
            this.specifiedSizeRadioButton.Size = new System.Drawing.Size(92, 17);
            this.specifiedSizeRadioButton.TabIndex = 13;
            this.specifiedSizeRadioButton.Text = "Specified Size";
            this.specifiedSizeRadioButton.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(142, 104);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(85, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // fromImagesRadioButton
            // 
            this.fromImagesRadioButton.AutoSize = true;
            this.fromImagesRadioButton.Checked = true;
            this.fromImagesRadioButton.Location = new System.Drawing.Point(12, 6);
            this.fromImagesRadioButton.Name = "fromImagesRadioButton";
            this.fromImagesRadioButton.Size = new System.Drawing.Size(85, 17);
            this.fromImagesRadioButton.TabIndex = 14;
            this.fromImagesRadioButton.TabStop = true;
            this.fromImagesRadioButton.Text = "From Images";
            this.fromImagesRadioButton.UseVisualStyleBackColor = true;
            this.fromImagesRadioButton.CheckedChanged += new System.EventHandler(this.fromImageRadioButton_CheckedChanged);
            // 
            // CreateFileForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(275, 137);
            this.Controls.Add(this.specifiedSizeRadioButton);
            this.Controls.Add(this.fromImagesRadioButton);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.sizeGroupBox);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateFileForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New GIF File";
            this.sizeGroupBox.ResumeLayout(false);
            this.sizeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.GroupBox sizeGroupBox;
		private System.Windows.Forms.NumericUpDown heightNumericUpDown;
		private System.Windows.Forms.Label heightLabel;
		private System.Windows.Forms.NumericUpDown widthNumericUpDown;
		private System.Windows.Forms.Label widthLabel;
		private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton specifiedSizeRadioButton;
        private System.Windows.Forms.RadioButton fromImagesRadioButton;
        private System.Windows.Forms.OpenFileDialog openImageFileDialog;
        private System.Windows.Forms.CheckBox addPagesToNewFileCheckBox;
	}
}