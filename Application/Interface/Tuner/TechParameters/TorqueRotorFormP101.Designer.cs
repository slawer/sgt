namespace SGT
{
    partial class TorqueRotorFormP101
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
            this.radioButtonASY = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(104, 97);
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
            this.cancel.Location = new System.Drawing.Point(185, 97);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonAnalog
            // 
            this.radioButtonAnalog.AutoSize = true;
            this.radioButtonAnalog.Location = new System.Drawing.Point(15, 33);
            this.radioButtonAnalog.Name = "radioButtonAnalog";
            this.radioButtonAnalog.Size = new System.Drawing.Size(135, 17);
            this.radioButtonAnalog.TabIndex = 2;
            this.radioButtonAnalog.TabStop = true;
            this.radioButtonAnalog.Text = "Аналоговое значение";
            this.radioButtonAnalog.UseVisualStyleBackColor = true;
            // 
            // radioButtonASY
            // 
            this.radioButtonASY.AutoSize = true;
            this.radioButtonASY.Location = new System.Drawing.Point(156, 33);
            this.radioButtonASY.Name = "radioButtonASY";
            this.radioButtonASY.Size = new System.Drawing.Size(92, 17);
            this.radioButtonASY.TabIndex = 3;
            this.radioButtonASY.TabStop = true;
            this.radioButtonASY.Text = "АСУ Буровой";
            this.radioButtonASY.UseVisualStyleBackColor = true;
            // 
            // TorqueRotorFormP101
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(272, 132);
            this.Controls.Add(this.radioButtonASY);
            this.Controls.Add(this.radioButtonAnalog);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TorqueRotorFormP101";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Крутящий момент ротора";
            this.Load += new System.EventHandler(this.TorqueRotorFormP101_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.RadioButton radioButtonAnalog;
        private System.Windows.Forms.RadioButton radioButtonASY;
    }
}