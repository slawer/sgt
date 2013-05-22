namespace SGT
{
    partial class SourceDiameterPumpForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonFirstExternal = new System.Windows.Forms.RadioButton();
            this.radioButtonFirstOwn = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonSecondExternal = new System.Windows.Forms.RadioButton();
            this.radioButtonSecondOnw = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxFirstDiameter = new System.Windows.Forms.ComboBox();
            this.comboBoxSecondDiameter = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBoxFirstDiameter);
            this.groupBox1.Controls.Add(this.radioButtonFirstExternal);
            this.groupBox1.Controls.Add(this.radioButtonFirstOwn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Диаметр поршня насоса 1";
            // 
            // radioButtonFirstExternal
            // 
            this.radioButtonFirstExternal.AutoSize = true;
            this.radioButtonFirstExternal.Location = new System.Drawing.Point(9, 76);
            this.radioButtonFirstExternal.Name = "radioButtonFirstExternal";
            this.radioButtonFirstExternal.Size = new System.Drawing.Size(182, 17);
            this.radioButtonFirstExternal.TabIndex = 3;
            this.radioButtonFirstExternal.TabStop = true;
            this.radioButtonFirstExternal.Text = "Определяет внешний источник";
            this.radioButtonFirstExternal.UseVisualStyleBackColor = true;
            this.radioButtonFirstExternal.CheckedChanged += new System.EventHandler(this.radioButtonFirstExternal_CheckedChanged);
            // 
            // radioButtonFirstOwn
            // 
            this.radioButtonFirstOwn.AutoSize = true;
            this.radioButtonFirstOwn.Location = new System.Drawing.Point(9, 53);
            this.radioButtonFirstOwn.Name = "radioButtonFirstOwn";
            this.radioButtonFirstOwn.Size = new System.Drawing.Size(149, 17);
            this.radioButtonFirstOwn.TabIndex = 2;
            this.radioButtonFirstOwn.TabStop = true;
            this.radioButtonFirstOwn.Text = "Определяет мастер СГТ";
            this.radioButtonFirstOwn.UseVisualStyleBackColor = true;
            this.radioButtonFirstOwn.CheckedChanged += new System.EventHandler(this.radioButtonFirstOwn_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Диаметр поршня";
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(119, 240);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 0;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(200, 240);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.comboBoxSecondDiameter);
            this.groupBox2.Controls.Add(this.radioButtonSecondExternal);
            this.groupBox2.Controls.Add(this.radioButtonSecondOnw);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Диаметр поршня насоса 2";
            // 
            // radioButtonSecondExternal
            // 
            this.radioButtonSecondExternal.AutoSize = true;
            this.radioButtonSecondExternal.Location = new System.Drawing.Point(9, 76);
            this.radioButtonSecondExternal.Name = "radioButtonSecondExternal";
            this.radioButtonSecondExternal.Size = new System.Drawing.Size(182, 17);
            this.radioButtonSecondExternal.TabIndex = 3;
            this.radioButtonSecondExternal.TabStop = true;
            this.radioButtonSecondExternal.Text = "Определяет внешний источник";
            this.radioButtonSecondExternal.UseVisualStyleBackColor = true;
            this.radioButtonSecondExternal.CheckedChanged += new System.EventHandler(this.radioButtonSecondExternal_CheckedChanged);
            // 
            // radioButtonSecondOnw
            // 
            this.radioButtonSecondOnw.AutoSize = true;
            this.radioButtonSecondOnw.Location = new System.Drawing.Point(9, 53);
            this.radioButtonSecondOnw.Name = "radioButtonSecondOnw";
            this.radioButtonSecondOnw.Size = new System.Drawing.Size(149, 17);
            this.radioButtonSecondOnw.TabIndex = 2;
            this.radioButtonSecondOnw.TabStop = true;
            this.radioButtonSecondOnw.Text = "Определяет мастер СГТ";
            this.radioButtonSecondOnw.UseVisualStyleBackColor = true;
            this.radioButtonSecondOnw.CheckedChanged += new System.EventHandler(this.radioButtonSecondOnw_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Диаметр поршня";
            // 
            // comboBoxFirstDiameter
            // 
            this.comboBoxFirstDiameter.FormattingEnabled = true;
            this.comboBoxFirstDiameter.Location = new System.Drawing.Point(106, 26);
            this.comboBoxFirstDiameter.Name = "comboBoxFirstDiameter";
            this.comboBoxFirstDiameter.Size = new System.Drawing.Size(110, 21);
            this.comboBoxFirstDiameter.TabIndex = 4;
            // 
            // comboBoxSecondDiameter
            // 
            this.comboBoxSecondDiameter.FormattingEnabled = true;
            this.comboBoxSecondDiameter.Location = new System.Drawing.Point(106, 26);
            this.comboBoxSecondDiameter.Name = "comboBoxSecondDiameter";
            this.comboBoxSecondDiameter.Size = new System.Drawing.Size(110, 21);
            this.comboBoxSecondDiameter.TabIndex = 5;
            // 
            // SourceDiameterPumpForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(287, 276);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SourceDiameterPumpForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка диаметров поршня";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SourceDiameterPumpForm_FormClosing);
            this.Load += new System.EventHandler(this.SourceDiameterPumpForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonFirstExternal;
        private System.Windows.Forms.RadioButton radioButtonFirstOwn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonSecondExternal;
        private System.Windows.Forms.RadioButton radioButtonSecondOnw;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxFirstDiameter;
        private System.Windows.Forms.ComboBox comboBoxSecondDiameter;
    }
}