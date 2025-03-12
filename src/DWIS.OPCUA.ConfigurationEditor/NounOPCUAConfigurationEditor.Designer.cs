namespace DWIS.OPCUA.ConfigurationEditor
{
    partial class NounOPCUAConfigurationEditor
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
            this.ExportAsTypeCheckBox = new System.Windows.Forms.CheckBox();
            this.MainGroupBox.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Controls.Add(this.flowLayoutPanel1);
            this.MainGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainGroupBox.Location = new System.Drawing.Point(0, 0);
            this.MainGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainGroupBox.Size = new System.Drawing.Size(697, 517);
            this.MainGroupBox.TabIndex = 0;
            this.MainGroupBox.TabStop = false;
            this.MainGroupBox.Text = "groupBox1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ExportAsTypeCheckBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 29);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(689, 483);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // ExportAsTypeCheckBox
            // 
            this.ExportAsTypeCheckBox.AutoSize = true;
            this.ExportAsTypeCheckBox.Location = new System.Drawing.Point(4, 5);
            this.ExportAsTypeCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ExportAsTypeCheckBox.Name = "ExportAsTypeCheckBox";
            this.ExportAsTypeCheckBox.Size = new System.Drawing.Size(156, 29);
            this.ExportAsTypeCheckBox.TabIndex = 0;
            this.ExportAsTypeCheckBox.Text = "Export As Type";
            this.ExportAsTypeCheckBox.UseVisualStyleBackColor = true;
            this.ExportAsTypeCheckBox.CheckedChanged += new System.EventHandler(this.ExportAsTypeCheckBox_CheckedChanged);
            // 
            // NounOPCUAConfigurationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainGroupBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "NounOPCUAConfigurationEditor";
            this.Size = new System.Drawing.Size(697, 517);
            this.MainGroupBox.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox MainGroupBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private CheckBox ExportAsTypeCheckBox;
    }
}
