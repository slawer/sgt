namespace SGT
{
    partial class FlowInletForm
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
            this.radioButtonHodam = new System.Windows.Forms.RadioButton();
            this.radioButtonOreol = new System.Windows.Forms.RadioButton();
            this.cancel = new System.Windows.Forms.Button();
            this.accept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButtonHodam
            // 
            this.radioButtonHodam.AutoSize = true;
            this.radioButtonHodam.Location = new System.Drawing.Point(166, 28);
            this.radioButtonHodam.Name = "radioButtonHodam";
            this.radioButtonHodam.Size = new System.Drawing.Size(149, 17);
            this.radioButtonHodam.TabIndex = 7;
            this.radioButtonHodam.TabStop = true;
            this.radioButtonHodam.Text = "Расход по ходам насоса";
            this.radioButtonHodam.UseVisualStyleBackColor = true;
            // 
            // radioButtonOreol
            // 
            this.radioButtonOreol.AutoSize = true;
            this.radioButtonOreol.Location = new System.Drawing.Point(12, 28);
            this.radioButtonOreol.Name = "radioButtonOreol";
            this.radioButtonOreol.Size = new System.Drawing.Size(148, 17);
            this.radioButtonOreol.TabIndex = 6;
            this.radioButtonOreol.TabStop = true;
            this.radioButtonOreol.Text = "Расход на входе Датчик";
            this.radioButtonOreol.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(240, 74);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(159, 74);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 4;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // FlowInletForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(327, 109);
            this.Controls.Add(this.radioButtonHodam);
            this.Controls.Add(this.radioButtonOreol);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FlowInletForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Расход на входе";
            this.Load += new System.EventHandler(this.FlowInletForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonHodam;
        private System.Windows.Forms.RadioButton radioButtonOreol;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button accept;
    }
}