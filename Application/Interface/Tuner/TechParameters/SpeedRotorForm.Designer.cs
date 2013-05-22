namespace SGT
{
    partial class SpeedRotorForm
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
            this.radioButtonAnalog = new System.Windows.Forms.RadioButton();
            this.radioButtonAsy = new System.Windows.Forms.RadioButton();
            this.radioButtonSvp = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(13, 131);
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
            this.cancel.Location = new System.Drawing.Point(94, 131);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonAnalog
            // 
            this.radioButtonAnalog.AutoSize = true;
            this.radioButtonAnalog.Location = new System.Drawing.Point(24, 45);
            this.radioButtonAnalog.Name = "radioButtonAnalog";
            this.radioButtonAnalog.Size = new System.Drawing.Size(124, 17);
            this.radioButtonAnalog.TabIndex = 2;
            this.radioButtonAnalog.TabStop = true;
            this.radioButtonAnalog.Text = "Аналоговый датчик";
            this.radioButtonAnalog.UseVisualStyleBackColor = true;
            // 
            // radioButtonAsy
            // 
            this.radioButtonAsy.AutoSize = true;
            this.radioButtonAsy.Location = new System.Drawing.Point(24, 68);
            this.radioButtonAsy.Name = "radioButtonAsy";
            this.radioButtonAsy.Size = new System.Drawing.Size(132, 17);
            this.radioButtonAsy.TabIndex = 3;
            this.radioButtonAsy.TabStop = true;
            this.radioButtonAsy.Text = "Датчик АСУ Буровой";
            this.radioButtonAsy.UseVisualStyleBackColor = true;
            // 
            // radioButtonSvp
            // 
            this.radioButtonSvp.AutoSize = true;
            this.radioButtonSvp.Location = new System.Drawing.Point(24, 22);
            this.radioButtonSvp.Name = "radioButtonSvp";
            this.radioButtonSvp.Size = new System.Drawing.Size(95, 17);
            this.radioButtonSvp.TabIndex = 4;
            this.radioButtonSvp.TabStop = true;
            this.radioButtonSvp.Text = "Обороты СВП";
            this.radioButtonSvp.UseVisualStyleBackColor = true;
            // 
            // SpeedRotorForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(181, 166);
            this.Controls.Add(this.radioButtonSvp);
            this.Controls.Add(this.radioButtonAsy);
            this.Controls.Add(this.radioButtonAnalog);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpeedRotorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Обороты ротора";
            this.Load += new System.EventHandler(this.SpeedRotorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.RadioButton radioButtonAnalog;
        private System.Windows.Forms.RadioButton radioButtonAsy;
        private System.Windows.Forms.RadioButton radioButtonSvp;
    }
}