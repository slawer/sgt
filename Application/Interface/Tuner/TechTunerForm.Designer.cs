namespace SGT
{
    partial class TechTunerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonDavlenieINagryska = new System.Windows.Forms.RadioButton();
            this.radioButtonDavlenie = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonRotorIliTalblok = new System.Windows.Forms.RadioButton();
            this.radioButtonSkorostTalbloka = new System.Windows.Forms.RadioButton();
            this.radioButtonOborotiRotora = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonPoVesyIliKlinam = new System.Windows.Forms.RadioButton();
            this.radioButtonPoKlinam = new System.Windows.Forms.RadioButton();
            this.radioButtonPoVesy = new System.Windows.Forms.RadioButton();
            this.pump_movies_btn = new System.Windows.Forms.Button();
            this.gas_sensors_btn = new System.Windows.Forms.Button();
            this.weight_hook_btn = new System.Windows.Forms.Button();
            this.volume_solution_btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.flow_inlet_btn = new System.Windows.Forms.Button();
            this.gonfig_parameters_btn = new System.Windows.Forms.Button();
            this.rotor_movie_btn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.rotor_btn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(404, 313);
            this.dataGridView.TabIndex = 15;
            this.dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView_CellBeginEdit);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dataGridView_CellParsing);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Параметр";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 300;
            // 
            // Column2
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = null;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.HeaderText = "Значение";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonDavlenieINagryska);
            this.groupBox1.Controls.Add(this.radioButtonDavlenie);
            this.groupBox1.Location = new System.Drawing.Point(422, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 62);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Метод расчета режима: Бурения";
            // 
            // radioButtonDavlenieINagryska
            // 
            this.radioButtonDavlenieINagryska.AutoSize = true;
            this.radioButtonDavlenieINagryska.Location = new System.Drawing.Point(99, 27);
            this.radioButtonDavlenieINagryska.Name = "radioButtonDavlenieINagryska";
            this.radioButtonDavlenieINagryska.Size = new System.Drawing.Size(134, 17);
            this.radioButtonDavlenieINagryska.TabIndex = 1;
            this.radioButtonDavlenieINagryska.TabStop = true;
            this.radioButtonDavlenieINagryska.Text = "Давление и нагрузка";
            this.radioButtonDavlenieINagryska.UseVisualStyleBackColor = true;
            this.radioButtonDavlenieINagryska.CheckedChanged += new System.EventHandler(this.radioButtonDavlenieINagryska_CheckedChanged);
            // 
            // radioButtonDavlenie
            // 
            this.radioButtonDavlenie.AutoSize = true;
            this.radioButtonDavlenie.Location = new System.Drawing.Point(17, 27);
            this.radioButtonDavlenie.Name = "radioButtonDavlenie";
            this.radioButtonDavlenie.Size = new System.Drawing.Size(76, 17);
            this.radioButtonDavlenie.TabIndex = 0;
            this.radioButtonDavlenie.TabStop = true;
            this.radioButtonDavlenie.Text = "Давление";
            this.radioButtonDavlenie.UseVisualStyleBackColor = true;
            this.radioButtonDavlenie.CheckedChanged += new System.EventHandler(this.radioButtonDavlenie_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonRotorIliTalblok);
            this.groupBox2.Controls.Add(this.radioButtonSkorostTalbloka);
            this.groupBox2.Controls.Add(this.radioButtonOborotiRotora);
            this.groupBox2.Location = new System.Drawing.Point(422, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 114);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Метод расчета режима: Проработка";
            // 
            // radioButtonRotorIliTalblok
            // 
            this.radioButtonRotorIliTalblok.Location = new System.Drawing.Point(17, 73);
            this.radioButtonRotorIliTalblok.Name = "radioButtonRotorIliTalblok";
            this.radioButtonRotorIliTalblok.Size = new System.Drawing.Size(200, 31);
            this.radioButtonRotorIliTalblok.TabIndex = 2;
            this.radioButtonRotorIliTalblok.TabStop = true;
            this.radioButtonRotorIliTalblok.Text = "По оборотам ротора или скорости тальблока";
            this.radioButtonRotorIliTalblok.UseVisualStyleBackColor = true;
            this.radioButtonRotorIliTalblok.CheckedChanged += new System.EventHandler(this.radioButtonRotorIliTalblok_CheckedChanged);
            // 
            // radioButtonSkorostTalbloka
            // 
            this.radioButtonSkorostTalbloka.AutoSize = true;
            this.radioButtonSkorostTalbloka.Location = new System.Drawing.Point(17, 50);
            this.radioButtonSkorostTalbloka.Name = "radioButtonSkorostTalbloka";
            this.radioButtonSkorostTalbloka.Size = new System.Drawing.Size(145, 17);
            this.radioButtonSkorostTalbloka.TabIndex = 1;
            this.radioButtonSkorostTalbloka.TabStop = true;
            this.radioButtonSkorostTalbloka.Text = "По скорости тальблока";
            this.radioButtonSkorostTalbloka.UseVisualStyleBackColor = true;
            this.radioButtonSkorostTalbloka.CheckedChanged += new System.EventHandler(this.radioButtonSkorostTalbloka_CheckedChanged);
            // 
            // radioButtonOborotiRotora
            // 
            this.radioButtonOborotiRotora.AutoSize = true;
            this.radioButtonOborotiRotora.Location = new System.Drawing.Point(17, 27);
            this.radioButtonOborotiRotora.Name = "radioButtonOborotiRotora";
            this.radioButtonOborotiRotora.Size = new System.Drawing.Size(129, 17);
            this.radioButtonOborotiRotora.TabIndex = 0;
            this.radioButtonOborotiRotora.TabStop = true;
            this.radioButtonOborotiRotora.Text = "По оборотам ротора";
            this.radioButtonOborotiRotora.UseVisualStyleBackColor = true;
            this.radioButtonOborotiRotora.CheckedChanged += new System.EventHandler(this.radioButtonOborotiRotora_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonPoVesyIliKlinam);
            this.groupBox3.Controls.Add(this.radioButtonPoKlinam);
            this.groupBox3.Controls.Add(this.radioButtonPoVesy);
            this.groupBox3.Location = new System.Drawing.Point(422, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(241, 102);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Метод расчета состояния: Вес на крюке";
            // 
            // radioButtonPoVesyIliKlinam
            // 
            this.radioButtonPoVesyIliKlinam.AutoSize = true;
            this.radioButtonPoVesyIliKlinam.Location = new System.Drawing.Point(17, 73);
            this.radioButtonPoVesyIliKlinam.Name = "radioButtonPoVesyIliKlinam";
            this.radioButtonPoVesyIliKlinam.Size = new System.Drawing.Size(200, 17);
            this.radioButtonPoVesyIliKlinam.TabIndex = 2;
            this.radioButtonPoVesyIliKlinam.TabStop = true;
            this.radioButtonPoVesyIliKlinam.Text = "Расчитывать по весу или клиньям";
            this.radioButtonPoVesyIliKlinam.UseVisualStyleBackColor = true;
            this.radioButtonPoVesyIliKlinam.CheckedChanged += new System.EventHandler(this.radioButtonPoVesyIliKlinam_CheckedChanged);
            // 
            // radioButtonPoKlinam
            // 
            this.radioButtonPoKlinam.AutoSize = true;
            this.radioButtonPoKlinam.Location = new System.Drawing.Point(17, 50);
            this.radioButtonPoKlinam.Name = "radioButtonPoKlinam";
            this.radioButtonPoKlinam.Size = new System.Drawing.Size(153, 17);
            this.radioButtonPoKlinam.TabIndex = 1;
            this.radioButtonPoKlinam.TabStop = true;
            this.radioButtonPoKlinam.Text = "Расчитывать по клиньям";
            this.radioButtonPoKlinam.UseVisualStyleBackColor = true;
            this.radioButtonPoKlinam.CheckedChanged += new System.EventHandler(this.radioButtonPoKlinam_CheckedChanged);
            // 
            // radioButtonPoVesy
            // 
            this.radioButtonPoVesy.AutoSize = true;
            this.radioButtonPoVesy.Location = new System.Drawing.Point(17, 27);
            this.radioButtonPoVesy.Name = "radioButtonPoVesy";
            this.radioButtonPoVesy.Size = new System.Drawing.Size(132, 17);
            this.radioButtonPoVesy.TabIndex = 0;
            this.radioButtonPoVesy.TabStop = true;
            this.radioButtonPoVesy.Text = "Расчитывать по весу";
            this.radioButtonPoVesy.UseVisualStyleBackColor = true;
            this.radioButtonPoVesy.CheckedChanged += new System.EventHandler(this.radioButtonPoVesy_CheckedChanged);
            // 
            // pump_movies_btn
            // 
            this.pump_movies_btn.Location = new System.Drawing.Point(216, 376);
            this.pump_movies_btn.Name = "pump_movies_btn";
            this.pump_movies_btn.Size = new System.Drawing.Size(96, 39);
            this.pump_movies_btn.TabIndex = 26;
            this.pump_movies_btn.Text = "Ходы насоса";
            this.pump_movies_btn.UseVisualStyleBackColor = true;
            this.pump_movies_btn.Click += new System.EventHandler(this.pump_movies_btn_Click);
            // 
            // gas_sensors_btn
            // 
            this.gas_sensors_btn.Location = new System.Drawing.Point(318, 376);
            this.gas_sensors_btn.Name = "gas_sensors_btn";
            this.gas_sensors_btn.Size = new System.Drawing.Size(96, 39);
            this.gas_sensors_btn.TabIndex = 27;
            this.gas_sensors_btn.Text = "Датчики газа";
            this.gas_sensors_btn.UseVisualStyleBackColor = true;
            this.gas_sensors_btn.Click += new System.EventHandler(this.gas_sensors_btn_Click);
            // 
            // weight_hook_btn
            // 
            this.weight_hook_btn.Location = new System.Drawing.Point(114, 331);
            this.weight_hook_btn.Name = "weight_hook_btn";
            this.weight_hook_btn.Size = new System.Drawing.Size(96, 39);
            this.weight_hook_btn.TabIndex = 29;
            this.weight_hook_btn.Text = "Вес на крюке";
            this.weight_hook_btn.UseVisualStyleBackColor = true;
            this.weight_hook_btn.Click += new System.EventHandler(this.weight_hook_btn_Click);
            // 
            // volume_solution_btn
            // 
            this.volume_solution_btn.Location = new System.Drawing.Point(216, 331);
            this.volume_solution_btn.Name = "volume_solution_btn";
            this.volume_solution_btn.Size = new System.Drawing.Size(96, 39);
            this.volume_solution_btn.TabIndex = 30;
            this.volume_solution_btn.Text = "Объем раствора";
            this.volume_solution_btn.UseVisualStyleBackColor = true;
            this.volume_solution_btn.Click += new System.EventHandler(this.volume_solution_btn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 376);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 39);
            this.button1.TabIndex = 33;
            this.button1.Text = "Сигнал тревоги газа 20%";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flow_inlet_btn
            // 
            this.flow_inlet_btn.Location = new System.Drawing.Point(12, 421);
            this.flow_inlet_btn.Name = "flow_inlet_btn";
            this.flow_inlet_btn.Size = new System.Drawing.Size(96, 39);
            this.flow_inlet_btn.TabIndex = 34;
            this.flow_inlet_btn.Text = "Расход на входе";
            this.flow_inlet_btn.UseVisualStyleBackColor = true;
            this.flow_inlet_btn.Click += new System.EventHandler(this.flow_inlet_btn_Click);
            // 
            // gonfig_parameters_btn
            // 
            this.gonfig_parameters_btn.Location = new System.Drawing.Point(216, 420);
            this.gonfig_parameters_btn.Name = "gonfig_parameters_btn";
            this.gonfig_parameters_btn.Size = new System.Drawing.Size(96, 39);
            this.gonfig_parameters_btn.TabIndex = 35;
            this.gonfig_parameters_btn.Text = "Настройка параметров";
            this.gonfig_parameters_btn.UseVisualStyleBackColor = true;
            this.gonfig_parameters_btn.Click += new System.EventHandler(this.gonfig_parameters_btn_Click);
            // 
            // rotor_movie_btn
            // 
            this.rotor_movie_btn.Location = new System.Drawing.Point(318, 331);
            this.rotor_movie_btn.Name = "rotor_movie_btn";
            this.rotor_movie_btn.Size = new System.Drawing.Size(96, 39);
            this.rotor_movie_btn.TabIndex = 36;
            this.rotor_movie_btn.Text = "Обороты ротора";
            this.rotor_movie_btn.UseVisualStyleBackColor = true;
            this.rotor_movie_btn.Click += new System.EventHandler(this.rotor_movie_btn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(114, 376);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 39);
            this.button2.TabIndex = 37;
            this.button2.Text = "Сигнал тревоги газа 50%";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // rotor_btn
            // 
            this.rotor_btn.Location = new System.Drawing.Point(12, 331);
            this.rotor_btn.Name = "rotor_btn";
            this.rotor_btn.Size = new System.Drawing.Size(96, 39);
            this.rotor_btn.TabIndex = 38;
            this.rotor_btn.Text = "Крутящий момент ротора";
            this.rotor_btn.UseVisualStyleBackColor = true;
            this.rotor_btn.Click += new System.EventHandler(this.rotor_btn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(318, 420);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 39);
            this.button3.TabIndex = 39;
            this.button3.Text = "Настройка команд";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(114, 421);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 39);
            this.button4.TabIndex = 40;
            this.button4.Text = "Диаметр поршня";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(12, 466);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(96, 39);
            this.button8.TabIndex = 41;
            this.button8.Text = "Буровая колонна";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButton2);
            this.groupBox5.Controls.Add(this.radioButton1);
            this.groupBox5.Location = new System.Drawing.Point(420, 308);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(243, 84);
            this.groupBox5.TabIndex = 42;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Метод расчета количества свечей";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(15, 50);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(165, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "По таблице компоновки БК";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(15, 27);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(149, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "По средней длине свечи";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // TechTunerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 519);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.rotor_btn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.rotor_movie_btn);
            this.Controls.Add(this.gonfig_parameters_btn);
            this.Controls.Add(this.flow_inlet_btn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.volume_solution_btn);
            this.Controls.Add(this.weight_hook_btn);
            this.Controls.Add(this.gas_sensors_btn);
            this.Controls.Add(this.pump_movies_btn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TechTunerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка технологии";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TechTunerForm_FormClosing);
            this.Load += new System.EventHandler(this.TechTunerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonDavlenieINagryska;
        private System.Windows.Forms.RadioButton radioButtonDavlenie;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonRotorIliTalblok;
        private System.Windows.Forms.RadioButton radioButtonSkorostTalbloka;
        private System.Windows.Forms.RadioButton radioButtonOborotiRotora;
        private System.Windows.Forms.RadioButton radioButtonPoKlinam;
        private System.Windows.Forms.RadioButton radioButtonPoVesy;
        private System.Windows.Forms.RadioButton radioButtonPoVesyIliKlinam;
        private System.Windows.Forms.Button pump_movies_btn;
        private System.Windows.Forms.Button gas_sensors_btn;
        private System.Windows.Forms.Button weight_hook_btn;
        private System.Windows.Forms.Button volume_solution_btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button flow_inlet_btn;
        private System.Windows.Forms.Button gonfig_parameters_btn;
        private System.Windows.Forms.Button rotor_movie_btn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button rotor_btn;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}