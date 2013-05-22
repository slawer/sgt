namespace SGT
{
    partial class DriveParametersForm
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
            this.checkBoxIsControlMinimum = new System.Windows.Forms.CheckBox();
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownNumberDecimal = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
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
            this.buttonSaveToSelect = new System.Windows.Forms.Button();
            this.listViewParameters = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberDecimal)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntervalToSaveToDB)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxIsControlMinimum);
            this.groupBox1.Controls.Add(this.textBoxMin);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.numericUpDownNumberDecimal);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.checkBoxControlMaximum);
            this.groupBox1.Controls.Add(this.checkBoxControlAlarm);
            this.groupBox1.Controls.Add(this.textBoxAlarmValue);
            this.groupBox1.Controls.Add(this.textBoxMax);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBoxParameterUnits);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(441, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 197);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Общие настройки";
            // 
            // checkBoxIsControlMinimum
            // 
            this.checkBoxIsControlMinimum.AutoSize = true;
            this.checkBoxIsControlMinimum.Location = new System.Drawing.Point(190, 48);
            this.checkBoxIsControlMinimum.Name = "checkBoxIsControlMinimum";
            this.checkBoxIsControlMinimum.Size = new System.Drawing.Size(109, 17);
            this.checkBoxIsControlMinimum.TabIndex = 3;
            this.checkBoxIsControlMinimum.Text = "Контролировать";
            this.checkBoxIsControlMinimum.UseVisualStyleBackColor = true;
            // 
            // textBoxMin
            // 
            this.textBoxMin.Location = new System.Drawing.Point(104, 45);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(80, 20);
            this.textBoxMin.TabIndex = 2;
            this.textBoxMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Минимальное";
            // 
            // numericUpDownNumberDecimal
            // 
            this.numericUpDownNumberDecimal.Location = new System.Drawing.Point(130, 154);
            this.numericUpDownNumberDecimal.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownNumberDecimal.Name = "numericUpDownNumberDecimal";
            this.numericUpDownNumberDecimal.Size = new System.Drawing.Size(89, 20);
            this.numericUpDownNumberDecimal.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 156);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Точек после запятой";
            // 
            // checkBoxControlMaximum
            // 
            this.checkBoxControlMaximum.AutoSize = true;
            this.checkBoxControlMaximum.Location = new System.Drawing.Point(190, 73);
            this.checkBoxControlMaximum.Name = "checkBoxControlMaximum";
            this.checkBoxControlMaximum.Size = new System.Drawing.Size(109, 17);
            this.checkBoxControlMaximum.TabIndex = 5;
            this.checkBoxControlMaximum.Text = "Контролировать";
            this.checkBoxControlMaximum.UseVisualStyleBackColor = true;
            // 
            // checkBoxControlAlarm
            // 
            this.checkBoxControlAlarm.AutoSize = true;
            this.checkBoxControlAlarm.Location = new System.Drawing.Point(190, 21);
            this.checkBoxControlAlarm.Name = "checkBoxControlAlarm";
            this.checkBoxControlAlarm.Size = new System.Drawing.Size(109, 17);
            this.checkBoxControlAlarm.TabIndex = 1;
            this.checkBoxControlAlarm.Text = "Контролировать";
            this.checkBoxControlAlarm.UseVisualStyleBackColor = true;
            // 
            // textBoxAlarmValue
            // 
            this.textBoxAlarmValue.Location = new System.Drawing.Point(104, 19);
            this.textBoxAlarmValue.Name = "textBoxAlarmValue";
            this.textBoxAlarmValue.Size = new System.Drawing.Size(80, 20);
            this.textBoxAlarmValue.TabIndex = 0;
            this.textBoxAlarmValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxMax
            // 
            this.textBoxMax.Location = new System.Drawing.Point(104, 71);
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.Size = new System.Drawing.Size(80, 20);
            this.textBoxMax.TabIndex = 4;
            this.textBoxMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Максимальное";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 22;
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
            this.comboBoxParameterUnits.Location = new System.Drawing.Point(13, 127);
            this.comboBoxParameterUnits.Name = "comboBoxParameterUnits";
            this.comboBoxParameterUnits.Size = new System.Drawing.Size(206, 21);
            this.comboBoxParameterUnits.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Единицы измерения параметра";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBoxIsSaveToDB);
            this.groupBox2.Controls.Add(this.textBoxPorogToDB);
            this.groupBox2.Controls.Add(this.numericUpDownIntervalToSaveToDB);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(441, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 121);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "База данных";
            // 
            // checkBoxIsSaveToDB
            // 
            this.checkBoxIsSaveToDB.AutoSize = true;
            this.checkBoxIsSaveToDB.Enabled = false;
            this.checkBoxIsSaveToDB.Location = new System.Drawing.Point(13, 80);
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
            this.textBoxPorogToDB.ReadOnly = true;
            this.textBoxPorogToDB.Size = new System.Drawing.Size(80, 20);
            this.textBoxPorogToDB.TabIndex = 11;
            // 
            // numericUpDownIntervalToSaveToDB
            // 
            this.numericUpDownIntervalToSaveToDB.Enabled = false;
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
            this.numericUpDownIntervalToSaveToDB.ReadOnly = true;
            this.numericUpDownIntervalToSaveToDB.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownIntervalToSaveToDB.TabIndex = 10;
            this.numericUpDownIntervalToSaveToDB.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
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
            // buttonSaveToSelect
            // 
            this.buttonSaveToSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveToSelect.Location = new System.Drawing.Point(441, 342);
            this.buttonSaveToSelect.Name = "buttonSaveToSelect";
            this.buttonSaveToSelect.Size = new System.Drawing.Size(182, 23);
            this.buttonSaveToSelect.TabIndex = 33;
            this.buttonSaveToSelect.Text = "Сохранить настройки параметра";
            this.buttonSaveToSelect.UseVisualStyleBackColor = true;
            this.buttonSaveToSelect.Click += new System.EventHandler(this.buttonSaveToSelect_Click);
            // 
            // listViewParameters
            // 
            this.listViewParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewParameters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewParameters.FullRowSelect = true;
            this.listViewParameters.GridLines = true;
            this.listViewParameters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewParameters.HideSelection = false;
            this.listViewParameters.Location = new System.Drawing.Point(12, 12);
            this.listViewParameters.MultiSelect = false;
            this.listViewParameters.Name = "listViewParameters";
            this.listViewParameters.Size = new System.Drawing.Size(423, 462);
            this.listViewParameters.TabIndex = 0;
            this.listViewParameters.UseCompatibleStateImageBehavior = false;
            this.listViewParameters.View = System.Windows.Forms.View.Details;
            this.listViewParameters.SelectedIndexChanged += new System.EventHandler(this.listViewParameters_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 39;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Параметр";
            this.columnHeader2.Width = 335;
            // 
            // DriveParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 486);
            this.Controls.Add(this.listViewParameters);
            this.Controls.Add(this.buttonSaveToSelect);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimizeBox = false;
            this.Name = "DriveParametersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Управление параметрами";
            this.Load += new System.EventHandler(this.DriveParametersForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberDecimal)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntervalToSaveToDB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxIsControlMinimum;
        private System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDownNumberDecimal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBoxControlMaximum;
        private System.Windows.Forms.CheckBox checkBoxControlAlarm;
        private System.Windows.Forms.TextBox textBoxAlarmValue;
        private System.Windows.Forms.TextBox textBoxMax;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxParameterUnits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxIsSaveToDB;
        private System.Windows.Forms.TextBox textBoxPorogToDB;
        private System.Windows.Forms.NumericUpDown numericUpDownIntervalToSaveToDB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonSaveToSelect;
        private System.Windows.Forms.ListView listViewParameters;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}