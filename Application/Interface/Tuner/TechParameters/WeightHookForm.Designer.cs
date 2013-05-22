namespace SGT
{
    partial class WeightHookForm
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
            this.cancel = new System.Windows.Forms.Button();
            this.accept = new System.Windows.Forms.Button();
            this.radioButtonVesYralmas = new System.Windows.Forms.RadioButton();
            this.radioButtonVesOreol = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(260, 96);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 7;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(179, 96);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 6;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // radioButtonVesYralmas
            // 
            this.radioButtonVesYralmas.AutoSize = true;
            this.radioButtonVesYralmas.Location = new System.Drawing.Point(164, 35);
            this.radioButtonVesYralmas.Name = "radioButtonVesYralmas";
            this.radioButtonVesYralmas.Size = new System.Drawing.Size(159, 17);
            this.radioButtonVesYralmas.TabIndex = 5;
            this.radioButtonVesYralmas.TabStop = true;
            this.radioButtonVesYralmas.Text = "Вес на крюке Аналоговый";
            this.radioButtonVesYralmas.UseVisualStyleBackColor = true;
            // 
            // radioButtonVesOreol
            // 
            this.radioButtonVesOreol.AutoSize = true;
            this.radioButtonVesOreol.Location = new System.Drawing.Point(24, 35);
            this.radioButtonVesOreol.Name = "radioButtonVesOreol";
            this.radioButtonVesOreol.Size = new System.Drawing.Size(134, 17);
            this.radioButtonVesOreol.TabIndex = 4;
            this.radioButtonVesOreol.TabStop = true;
            this.radioButtonVesOreol.Text = "Вес на крюке Датчик";
            this.radioButtonVesOreol.UseVisualStyleBackColor = true;
            // 
            // WeightHookForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(347, 131);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.radioButtonVesYralmas);
            this.Controls.Add(this.radioButtonVesOreol);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WeightHookForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Вес на крюке";
            this.Load += new System.EventHandler(this.WeightHookForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.RadioButton radioButtonVesYralmas;
        private System.Windows.Forms.RadioButton radioButtonVesOreol;
    }
}