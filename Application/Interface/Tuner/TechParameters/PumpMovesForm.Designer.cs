namespace SGT
{
    partial class PumpMovesForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAnalogPervii = new System.Windows.Forms.TextBox();
            this.textBoxAnalogVtoroi = new System.Windows.Forms.TextBox();
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxAsyVtoroi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAsyPervii = new System.Windows.Forms.TextBox();
            this.radioButtonAnalog = new System.Windows.Forms.RadioButton();
            this.radioButtonASY = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Коэффициент наполнения насоса 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Коэффициент наполнения насоса 2";
            // 
            // textBoxAnalogPervii
            // 
            this.textBoxAnalogPervii.Location = new System.Drawing.Point(243, 27);
            this.textBoxAnalogPervii.Name = "textBoxAnalogPervii";
            this.textBoxAnalogPervii.Size = new System.Drawing.Size(100, 20);
            this.textBoxAnalogPervii.TabIndex = 4;
            this.textBoxAnalogPervii.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxAnalogVtoroi
            // 
            this.textBoxAnalogVtoroi.Location = new System.Drawing.Point(243, 53);
            this.textBoxAnalogVtoroi.Name = "textBoxAnalogVtoroi";
            this.textBoxAnalogVtoroi.Size = new System.Drawing.Size(100, 20);
            this.textBoxAnalogVtoroi.TabIndex = 5;
            this.textBoxAnalogVtoroi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(229, 301);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 6;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(310, 301);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 7;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxAnalogVtoroi);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxAnalogPervii);
            this.groupBox1.Location = new System.Drawing.Point(12, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 98);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Расход на входе по Ходам Насоса с Аналогового сигнала";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxAsyVtoroi);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxAsyPervii);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 95);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Расход на входе Ходам Насоса АСУ";
            // 
            // textBoxAsyVtoroi
            // 
            this.textBoxAsyVtoroi.Location = new System.Drawing.Point(243, 53);
            this.textBoxAsyVtoroi.Name = "textBoxAsyVtoroi";
            this.textBoxAsyVtoroi.Size = new System.Drawing.Size(100, 20);
            this.textBoxAsyVtoroi.TabIndex = 2;
            this.textBoxAsyVtoroi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Коэффициент наполнения насоса 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Коэффициент наполнения насоса 2";
            // 
            // textBoxAsyPervii
            // 
            this.textBoxAsyPervii.Location = new System.Drawing.Point(243, 27);
            this.textBoxAsyPervii.Name = "textBoxAsyPervii";
            this.textBoxAsyPervii.Size = new System.Drawing.Size(100, 20);
            this.textBoxAsyPervii.TabIndex = 1;
            this.textBoxAsyPervii.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // radioButtonAnalog
            // 
            this.radioButtonAnalog.AutoSize = true;
            this.radioButtonAnalog.Location = new System.Drawing.Point(12, 301);
            this.radioButtonAnalog.Name = "radioButtonAnalog";
            this.radioButtonAnalog.Size = new System.Drawing.Size(87, 17);
            this.radioButtonAnalog.TabIndex = 3;
            this.radioButtonAnalog.TabStop = true;
            this.radioButtonAnalog.Text = "Аналоговый";
            this.radioButtonAnalog.UseVisualStyleBackColor = true;
            // 
            // radioButtonASY
            // 
            this.radioButtonASY.AutoSize = true;
            this.radioButtonASY.Location = new System.Drawing.Point(105, 301);
            this.radioButtonASY.Name = "radioButtonASY";
            this.radioButtonASY.Size = new System.Drawing.Size(92, 17);
            this.radioButtonASY.TabIndex = 4;
            this.radioButtonASY.TabStop = true;
            this.radioButtonASY.Text = "АСУ Буровой";
            this.radioButtonASY.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(372, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Таблица идеальных расходов по Ходам насоса АСУ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 246);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(372, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Таблица идеальных расходов по Аналоговому сигналу";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PumpMovesForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(397, 336);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButtonAnalog);
            this.Controls.Add(this.radioButtonASY);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PumpMovesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ходы насоса";
            this.Load += new System.EventHandler(this.PumpMovesForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBoxAnalogPervii;
        public System.Windows.Forms.TextBox textBoxAnalogVtoroi;
        public System.Windows.Forms.TextBox textBoxAsyVtoroi;
        public System.Windows.Forms.TextBox textBoxAsyPervii;
        private System.Windows.Forms.RadioButton radioButtonAnalog;
        private System.Windows.Forms.RadioButton radioButtonASY;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}