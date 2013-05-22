namespace SGT
{
    partial class CommandP0105Form
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
            this.textBoxPotok = new System.Windows.Forms.TextBox();
            this.set_new_btn = new System.Windows.Forms.Button();
            this.reset_new_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Точка отсчета";
            // 
            // textBoxPotok
            // 
            this.textBoxPotok.Location = new System.Drawing.Point(113, 46);
            this.textBoxPotok.Name = "textBoxPotok";
            this.textBoxPotok.Size = new System.Drawing.Size(100, 20);
            this.textBoxPotok.TabIndex = 1;
            this.textBoxPotok.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // set_new_btn
            // 
            this.set_new_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.set_new_btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.set_new_btn.Location = new System.Drawing.Point(91, 131);
            this.set_new_btn.Name = "set_new_btn";
            this.set_new_btn.Size = new System.Drawing.Size(75, 23);
            this.set_new_btn.TabIndex = 2;
            this.set_new_btn.Text = "Принять";
            this.set_new_btn.UseVisualStyleBackColor = true;
            this.set_new_btn.Click += new System.EventHandler(this.set_new_btn_Click);
            // 
            // reset_new_btn
            // 
            this.reset_new_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reset_new_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.reset_new_btn.Location = new System.Drawing.Point(172, 131);
            this.reset_new_btn.Name = "reset_new_btn";
            this.reset_new_btn.Size = new System.Drawing.Size(75, 23);
            this.reset_new_btn.TabIndex = 3;
            this.reset_new_btn.Text = "Отмена";
            this.reset_new_btn.UseVisualStyleBackColor = true;
            // 
            // CommandP0105Form
            // 
            this.AcceptButton = this.set_new_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.reset_new_btn;
            this.ClientSize = new System.Drawing.Size(259, 166);
            this.Controls.Add(this.reset_new_btn);
            this.Controls.Add(this.textBoxPotok);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.set_new_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommandP0105Form";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Изменение потока на выходе";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommandP0105Form_FormClosing);
            this.Load += new System.EventHandler(this.CommandP0105Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPotok;
        private System.Windows.Forms.Button set_new_btn;
        private System.Windows.Forms.Button reset_new_btn;
    }
}