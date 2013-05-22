namespace SGT
{
    partial class AsyCommandTunerForm
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
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPult = new System.Windows.Forms.TextBox();
            this.buttonPult = new System.Windows.Forms.Button();
            this.buttonResetVes = new System.Windows.Forms.Button();
            this.textBoxResetVes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(314, 90);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 2;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(395, 90);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Пульт управления";
            // 
            // textBoxPult
            // 
            this.textBoxPult.Location = new System.Drawing.Point(120, 25);
            this.textBoxPult.Name = "textBoxPult";
            this.textBoxPult.ReadOnly = true;
            this.textBoxPult.Size = new System.Drawing.Size(318, 20);
            this.textBoxPult.TabIndex = 3;
            // 
            // buttonPult
            // 
            this.buttonPult.Location = new System.Drawing.Point(444, 25);
            this.buttonPult.Name = "buttonPult";
            this.buttonPult.Size = new System.Drawing.Size(27, 20);
            this.buttonPult.TabIndex = 0;
            this.buttonPult.Text = "...";
            this.buttonPult.UseVisualStyleBackColor = true;
            this.buttonPult.Click += new System.EventHandler(this.buttonPult_Click);
            // 
            // buttonResetVes
            // 
            this.buttonResetVes.Location = new System.Drawing.Point(444, 51);
            this.buttonResetVes.Name = "buttonResetVes";
            this.buttonResetVes.Size = new System.Drawing.Size(27, 20);
            this.buttonResetVes.TabIndex = 1;
            this.buttonResetVes.Text = "...";
            this.buttonResetVes.UseVisualStyleBackColor = true;
            this.buttonResetVes.Click += new System.EventHandler(this.buttonResetVes_Click);
            // 
            // textBoxResetVes
            // 
            this.textBoxResetVes.Location = new System.Drawing.Point(120, 51);
            this.textBoxResetVes.Name = "textBoxResetVes";
            this.textBoxResetVes.ReadOnly = true;
            this.textBoxResetVes.Size = new System.Drawing.Size(318, 20);
            this.textBoxResetVes.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Команды АСУ";
            // 
            // AsyCommandTunerForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(482, 125);
            this.Controls.Add(this.buttonResetVes);
            this.Controls.Add(this.textBoxResetVes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonPult);
            this.Controls.Add(this.textBoxPult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsyCommandTunerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка команд управления";
            this.Load += new System.EventHandler(this.AsyCommandTunerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPult;
        private System.Windows.Forms.Button buttonPult;
        private System.Windows.Forms.Button buttonResetVes;
        private System.Windows.Forms.TextBox textBoxResetVes;
        private System.Windows.Forms.Label label2;

    }
}