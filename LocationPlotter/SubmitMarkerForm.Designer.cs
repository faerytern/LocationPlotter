namespace LocationPlotter
{
    partial class SubmitMarkerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelUserID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.descBox = new System.Windows.Forms.RichTextBox();
            this.labelLatitude = new System.Windows.Forms.Label();
            this.labelLongitude = new System.Windows.Forms.Label();
            this.numericUpDownLat = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLog = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownUserID = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUserID)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Secret Marker Submitter Form!";
            // 
            // labelUserID
            // 
            this.labelUserID.AutoSize = true;
            this.labelUserID.Location = new System.Drawing.Point(129, 76);
            this.labelUserID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.Size = new System.Drawing.Size(71, 26);
            this.labelUserID.TabIndex = 1;
            this.labelUserID.Text = "UserID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 227);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Description of location";
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(87, 384);
            this.buttonSubmit.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(155, 47);
            this.buttonSubmit.TabIndex = 5;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(111, 439);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(107, 46);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // descBox
            // 
            this.descBox.Location = new System.Drawing.Point(87, 256);
            this.descBox.Name = "descBox";
            this.descBox.Size = new System.Drawing.Size(155, 120);
            this.descBox.TabIndex = 7;
            this.descBox.Text = "";
            // 
            // labelLatitude
            // 
            this.labelLatitude.AutoSize = true;
            this.labelLatitude.Location = new System.Drawing.Point(42, 145);
            this.labelLatitude.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelLatitude.Name = "labelLatitude";
            this.labelLatitude.Size = new System.Drawing.Size(83, 26);
            this.labelLatitude.TabIndex = 10;
            this.labelLatitude.Text = "Latitude";
            // 
            // labelLongitude
            // 
            this.labelLongitude.AutoSize = true;
            this.labelLongitude.Location = new System.Drawing.Point(197, 145);
            this.labelLongitude.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelLongitude.Name = "labelLongitude";
            this.labelLongitude.Size = new System.Drawing.Size(98, 26);
            this.labelLongitude.TabIndex = 11;
            this.labelLongitude.Text = "Longitude";
            // 
            // numericUpDownLat
            // 
            this.numericUpDownLat.DecimalPlaces = 6;
            this.numericUpDownLat.Location = new System.Drawing.Point(12, 174);
            this.numericUpDownLat.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numericUpDownLat.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.numericUpDownLat.Name = "numericUpDownLat";
            this.numericUpDownLat.Size = new System.Drawing.Size(142, 33);
            this.numericUpDownLat.TabIndex = 12;
            this.numericUpDownLat.Value = new decimal(new int[] {
            123,
            0,
            0,
            196608});
            // 
            // numericUpDownLog
            // 
            this.numericUpDownLog.DecimalPlaces = 6;
            this.numericUpDownLog.Location = new System.Drawing.Point(175, 174);
            this.numericUpDownLog.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDownLog.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.numericUpDownLog.Name = "numericUpDownLog";
            this.numericUpDownLog.Size = new System.Drawing.Size(142, 33);
            this.numericUpDownLog.TabIndex = 13;
            this.numericUpDownLog.Value = new decimal(new int[] {
            123,
            0,
            0,
            196608});
            // 
            // numericUpDownUserID
            // 
            this.numericUpDownUserID.Location = new System.Drawing.Point(92, 109);
            this.numericUpDownUserID.Maximum = new decimal(new int[] {
            2022999999,
            0,
            0,
            0});
            this.numericUpDownUserID.Name = "numericUpDownUserID";
            this.numericUpDownUserID.Size = new System.Drawing.Size(144, 33);
            this.numericUpDownUserID.TabIndex = 14;
            this.numericUpDownUserID.Value = new decimal(new int[] {
            2022123456,
            0,
            0,
            0});
            // 
            // SubmitMarkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 497);
            this.Controls.Add(this.numericUpDownUserID);
            this.Controls.Add(this.numericUpDownLog);
            this.Controls.Add(this.numericUpDownLat);
            this.Controls.Add(this.labelLongitude);
            this.Controls.Add(this.labelLatitude);
            this.Controls.Add(this.descBox);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelUserID);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "SubmitMarkerForm";
            this.Text = "Submit A Marker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubmitMarkerForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUserID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label labelUserID;
        private Label label3;
        private Button buttonSubmit;
        private Button buttonCancel;
        private RichTextBox descBox;
        private Label labelLatitude;
        private Label labelLongitude;
        private NumericUpDown numericUpDownLat;
        private NumericUpDown numericUpDownLog;
        private NumericUpDown numericUpDownUserID;
    }
}