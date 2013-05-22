namespace SGT
{
    partial class EditParameterForm
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
            this.textBoxParameterName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxParameterDesc = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxIsControlMinimum = new System.Windows.Forms.CheckBox();
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownNumberDecimal = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.selectChannel = new System.Windows.Forms.Button();
            this.textBoxParameterChannelName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxControlMaximum = new System.Windows.Forms.CheckBox();
            this.checkBoxControlAlarm = new System.Windows.Forms.CheckBox();
            this.textBoxAlarmValue = new System.Windows.Forms.TextBox();
            this.textBoxMax = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxParameterUnits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxIsSaveToDB = new System.Windows.Forms.CheckBox();
            this.textBoxPorogToDB = new System.Windows.Forms.TextBox();
            this.numericUpDownIntervalToSaveToDB = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberDecimal)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntervalToSaveToDB)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя параметра";
            // 
            // textBoxParameterName
            // 
            this.textBoxParameterName.Location = new System.Drawing.Point(105, 26);
            this.textBoxParameterName.Name = "textBoxParameterName";
            this.textBoxParameterName.Size = new System.Drawing.Size(255, 20);
            this.textBoxParameterName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(366, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Описатель";
            // 
            // textBoxParameterDesc
            // 
            this.textBoxParameterDesc.Location = new System.Drawing.Point(434, 26);
            this.textBoxParameterDesc.Name = "textBoxParameterDesc";
            this.textBoxParameterDesc.Size = new System.Drawing.Size(100, 20);
            this.textBoxParameterDesc.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxIsControlMinimum);
            this.groupBox1.Controls.Add(this.textBoxMin);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.numericUpDownNumberDecimal);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.selectChannel);
            this.groupBox1.Controls.Add(this.textBoxParameterChannelName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.checkBoxControlMaximum);
            this.groupBox1.Controls.Add(this.checkBoxControlAlarm);
            this.groupBox1.Controls.Add(this.textBoxAlarmValue);
            this.groupBox1.Controls.Add(this.textBoxMax);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBoxParameterUnits);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(15, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 153);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Общие настройки параметра";
            // 
            // checkBoxIsControlMinimum
            // 
            this.checkBoxIsControlMinimum.AutoSize = true;
            this.checkBoxIsControlMinimum.Location = new System.Drawing.Point(193, 92);
            this.checkBoxIsControlMinimum.Name = "checkBoxIsControlMinimum";
            this.checkBoxIsControlMinimum.Size = new System.Drawing.Size(109, 17);
            this.checkBoxIsControlMinimum.TabIndex = 18;
            this.checkBoxIsControlMinimum.Text = "Контролировать";
            this.checkBoxIsControlMinimum.UseVisualStyleBackColor = true;
            // 
            // textBoxMin
            // 
            this.textBoxMin.Location = new System.Drawing.Point(107, 89);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(80, 20);
            this.textBoxMin.TabIndex = 17;
            this.textBoxMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Минимальное";
            // 
            // numericUpDownNumberDecimal
            // 
            this.numericUpDownNumberDecimal.Location = new System.Drawing.Point(425, 115);
            this.numericUpDownNumberDecimal.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownNumberDecimal.Name = "numericUpDownNumberDecimal";
            this.numericUpDownNumberDecimal.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownNumberDecimal.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(305, 117);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Точек после запятой";
            // 
            // selectChannel
            // 
            this.selectChannel.Location = new System.Drawing.Point(488, 27);
            this.selectChannel.Name = "selectChannel";
            this.selectChannel.Size = new System.Drawing.Size(25, 20);
            this.selectChannel.TabIndex = 2;
            this.selectChannel.Text = "...";
            this.selectChannel.UseVisualStyleBackColor = true;
            this.selectChannel.Click += new System.EventHandler(this.selectChannel_Click);
            // 
            // textBoxParameterChannelName
            // 
            this.textBoxParameterChannelName.Location = new System.Drawing.Point(107, 28);
            this.textBoxParameterChannelName.Name = "textBoxParameterChannelName";
            this.textBoxParameterChannelName.ReadOnly = true;
            this.textBoxParameterChannelName.Size = new System.Drawing.Size(375, 20);
            this.textBoxParameterChannelName.TabIndex = 13;
            this.textBoxParameterChannelName.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Сигнал с датчика";
            // 
            // checkBoxControlMaximum
            // 
            this.checkBoxControlMaximum.AutoSize = true;
            this.checkBoxControlMaximum.Location = new System.Drawing.Point(193, 117);
            this.checkBoxControlMaximum.Name = "checkBoxControlMaximum";
            this.checkBoxControlMaximum.Size = new System.Drawing.Size(109, 17);
            this.checkBoxControlMaximum.TabIndex = 9;
            this.checkBoxControlMaximum.Text = "Контролировать";
            this.checkBoxControlMaximum.UseVisualStyleBackColor = true;
            // 
            // checkBoxControlAlarm
            // 
            this.checkBoxControlAlarm.AutoSize = true;
            this.checkBoxControlAlarm.Location = new System.Drawing.Point(193, 65);
            this.checkBoxControlAlarm.Name = "checkBoxControlAlarm";
            this.checkBoxControlAlarm.Size = new System.Drawing.Size(109, 17);
            this.checkBoxControlAlarm.TabIndex = 7;
            this.checkBoxControlAlarm.Text = "Контролировать";
            this.checkBoxControlAlarm.UseVisualStyleBackColor = true;
            // 
            // textBoxAlarmValue
            // 
            this.textBoxAlarmValue.Location = new System.Drawing.Point(107, 63);
            this.textBoxAlarmValue.Name = "textBoxAlarmValue";
            this.textBoxAlarmValue.Size = new System.Drawing.Size(80, 20);
            this.textBoxAlarmValue.TabIndex = 6;
            this.textBoxAlarmValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxMax
            // 
            this.textBoxMax.Location = new System.Drawing.Point(107, 115);
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.Size = new System.Drawing.Size(80, 20);
            this.textBoxMax.TabIndex = 8;
            this.textBoxMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Максимальное";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Аварийное";
            // 
            // comboBoxParameterUnits
            // 
            this.comboBoxParameterUnits.FormattingEnabled = true;
            this.comboBoxParameterUnits.Items.AddRange(new object[] {
            "т",
            "кг",
            "г",
            "кг/см2",
            "атм",
            "г/см3",
            "л/сек",
            "м3",
            "град",
            "°С",
            "см",
            "м",
            "сек",
            "%",
            "кГм",
            "тМ ",
            "кНм",
            "об/мин",
            "м3/мин",
            "л",
            "мм",
            "мин",
            "час",
            "м/сек",
            "м/час",
            "мин/м",
            "Тс",
            "кГ",
            "Н",
            "кН",
            "Единицы измерения не определены"});
            this.comboBoxParameterUnits.Location = new System.Drawing.Point(308, 88);
            this.comboBoxParameterUnits.Name = "comboBoxParameterUnits";
            this.comboBoxParameterUnits.Size = new System.Drawing.Size(174, 21);
            this.comboBoxParameterUnits.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(308, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Единицы измерения параметра";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxIsSaveToDB);
            this.groupBox2.Controls.Add(this.textBoxPorogToDB);
            this.groupBox2.Controls.Add(this.numericUpDownIntervalToSaveToDB);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(15, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(519, 90);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "База данных";
            // 
            // checkBoxIsSaveToDB
            // 
            this.checkBoxIsSaveToDB.AutoSize = true;
            this.checkBoxIsSaveToDB.Location = new System.Drawing.Point(236, 29);
            this.checkBoxIsSaveToDB.Name = "checkBoxIsSaveToDB";
            this.checkBoxIsSaveToDB.Size = new System.Drawing.Size(206, 17);
            this.checkBoxIsSaveToDB.TabIndex = 12;
            this.checkBoxIsSaveToDB.Text = "Сохранять параметр в базу данных";
            this.checkBoxIsSaveToDB.UseVisualStyleBackColor = true;
            // 
            // textBoxPorogToDB
            // 
            this.textBoxPorogToDB.Location = new System.Drawing.Point(133, 54);
            this.textBoxPorogToDB.Name = "textBoxPorogToDB";
            this.textBoxPorogToDB.Size = new System.Drawing.Size(80, 20);
            this.textBoxPorogToDB.TabIndex = 11;
            // 
            // numericUpDownIntervalToSaveToDB
            // 
            this.numericUpDownIntervalToSaveToDB.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownIntervalToSaveToDB.Location = new System.Drawing.Point(133, 28);
            this.numericUpDownIntervalToSaveToDB.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numericUpDownIntervalToSaveToDB.Name = "numericUpDownIntervalToSaveToDB";
            this.numericUpDownIntervalToSaveToDB.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownIntervalToSaveToDB.TabIndex = 10;
            this.numericUpDownIntervalToSaveToDB.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownIntervalToSaveToDB.ValueChanged += new System.EventHandler(this.numericUpDownIntervalToSaveToDB_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Порог для записи";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Интервал записи (мс)";
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(377, 316);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 13;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(458, 316);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 14;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // EditParameterForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(545, 351);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxParameterDesc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxParameterName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditParameterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование параметра";
            this.Load += new System.EventHandler(this.EditParameterForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberDecimal)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntervalToSaveToDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxParameterName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxParameterDesc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxParameterUnits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxControlMaximum;
        private System.Windows.Forms.CheckBox checkBoxControlAlarm;
        private System.Windows.Forms.TextBox textBoxAlarmValue;
        private System.Windows.Forms.TextBox textBoxMax;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button selectChannel;
        private System.Windows.Forms.TextBox textBoxParameterChannelName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxIsSaveToDB;
        private System.Windows.Forms.TextBox textBoxPorogToDB;
        private System.Windows.Forms.NumericUpDown numericUpDownIntervalToSaveToDB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.NumericUpDown numericUpDownNumberDecimal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBoxIsControlMinimum;
        private System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.Label label12;
    }
}