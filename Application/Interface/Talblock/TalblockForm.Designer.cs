namespace SGT
{
    partial class TalblockForm
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
            this.numericUpDownDevNumber = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownKoef = new System.Windows.Forms.NumericUpDown();
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDevNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKoef)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер устройства";
            // 
            // numericUpDownDevNumber
            // 
            this.numericUpDownDevNumber.Location = new System.Drawing.Point(160, 25);
            this.numericUpDownDevNumber.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numericUpDownDevNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDevNumber.Name = "numericUpDownDevNumber";
            this.numericUpDownDevNumber.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownDevNumber.TabIndex = 1;
            this.numericUpDownDevNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Коэффициент тальблока";
            // 
            // numericUpDownKoef
            // 
            this.numericUpDownKoef.Location = new System.Drawing.Point(160, 51);
            this.numericUpDownKoef.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownKoef.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownKoef.Name = "numericUpDownKoef";
            this.numericUpDownKoef.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownKoef.TabIndex = 3;
            this.numericUpDownKoef.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(103, 146);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 4;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(184, 146);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // TalblockForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(271, 181);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.numericUpDownKoef);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownDevNumber);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TalblockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка команды тальблок";
            this.Load += new System.EventHandler(this.TalblockForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDevNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKoef)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        public System.Windows.Forms.NumericUpDown numericUpDownDevNumber;
        public System.Windows.Forms.NumericUpDown numericUpDownKoef;
    }
}