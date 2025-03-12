namespace DWIS.OPCUA.ConfigurationEditor
{
    partial class VerbOPCUAConfigurationEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainGroupBox = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.BaseReferenceComboBox = new System.Windows.Forms.ComboBox();
            this.ExportCheckBox = new System.Windows.Forms.CheckBox();
            this.MainGroupBox.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Controls.Add(this.flowLayoutPanel1);
            this.MainGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGroupBox.Location = new System.Drawing.Point(0, 0);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Size = new System.Drawing.Size(449, 240);
            this.MainGroupBox.TabIndex = 0;
            this.MainGroupBox.TabStop = false;
            this.MainGroupBox.Text = "groupBox1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.BaseReferenceComboBox);
            this.flowLayoutPanel1.Controls.Add(this.ExportCheckBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(443, 218);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // BaseReferenceComboBox
            // 
            this.BaseReferenceComboBox.FormattingEnabled = true;
            this.BaseReferenceComboBox.Location = new System.Drawing.Point(3, 3);
            this.BaseReferenceComboBox.Name = "BaseReferenceComboBox";
            this.BaseReferenceComboBox.Size = new System.Drawing.Size(121, 23);
            this.BaseReferenceComboBox.TabIndex = 0;
            this.BaseReferenceComboBox.Text = "Base reference";
            // 
            // ExportCheckBox
            // 
            this.ExportCheckBox.AutoSize = true;
            this.ExportCheckBox.Location = new System.Drawing.Point(130, 3);
            this.ExportCheckBox.Name = "ExportCheckBox";
            this.ExportCheckBox.Size = new System.Drawing.Size(60, 19);
            this.ExportCheckBox.TabIndex = 1;
            this.ExportCheckBox.Text = "Export";
            this.ExportCheckBox.UseVisualStyleBackColor = true;
            // 
            // VerbOPCUAConfigurationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainGroupBox);
            this.Name = "VerbOPCUAConfigurationEditor";
            this.Size = new System.Drawing.Size(449, 240);
            this.MainGroupBox.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox MainGroupBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private ComboBox BaseReferenceComboBox;
        private CheckBox ExportCheckBox;
    }
}
