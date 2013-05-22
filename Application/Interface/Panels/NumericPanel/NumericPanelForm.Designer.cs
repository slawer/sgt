namespace SGT
{
    partial class NumericPanelForm
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonNewParameter = new System.Windows.Forms.Button();
            this.buttonEditParameter = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lb_RashodMax = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lb_RashodMin = new System.Windows.Forms.TextBox();
            this.lb_pressureMax = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonRashodColor = new System.Windows.Forms.Button();
            this.lb_pressureMin = new System.Windows.Forms.TextBox();
            this.lb_VesMax = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.buttonPressureColor = new System.Windows.Forms.Button();
            this.lb_VesMin = new System.Windows.Forms.TextBox();
            this.lb_MehSkorostMax = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.buttonVesColor = new System.Windows.Forms.Button();
            this.lb_MehSkorostMin = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.buttonMehSkorostColor = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.lb_GlubinaMax = new System.Windows.Forms.TextBox();
            this.lb_GlubinaMin = new System.Windows.Forms.TextBox();
            this.buttonGlubinaColor = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listViewGraphics = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPanelName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSelectFontParameter = new System.Windows.Forms.Button();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // accept
            // 
            this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(575, 498);
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
            this.cancel.Location = new System.Drawing.Point(656, 498);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(378, 433);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Параметр";
            this.columnHeader2.Width = 258;
            // 
            // buttonNewParameter
            // 
            this.buttonNewParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNewParameter.Location = new System.Drawing.Point(12, 451);
            this.buttonNewParameter.Name = "buttonNewParameter";
            this.buttonNewParameter.Size = new System.Drawing.Size(122, 23);
            this.buttonNewParameter.TabIndex = 4;
            this.buttonNewParameter.Text = "Добавить параметр";
            this.buttonNewParameter.UseVisualStyleBackColor = true;
            this.buttonNewParameter.Click += new System.EventHandler(this.buttonNewParameter_Click);
            // 
            // buttonEditParameter
            // 
            this.buttonEditParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEditParameter.Location = new System.Drawing.Point(140, 451);
            this.buttonEditParameter.Name = "buttonEditParameter";
            this.buttonEditParameter.Size = new System.Drawing.Size(122, 23);
            this.buttonEditParameter.TabIndex = 5;
            this.buttonEditParameter.Text = "Изменить параметр";
            this.buttonEditParameter.UseVisualStyleBackColor = true;
            this.buttonEditParameter.Click += new System.EventHandler(this.buttonEditParameter_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(268, 451);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Удалить параметр";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lb_RashodMax);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lb_RashodMin);
            this.groupBox1.Controls.Add(this.lb_pressureMax);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.buttonRashodColor);
            this.groupBox1.Controls.Add(this.lb_pressureMin);
            this.groupBox1.Controls.Add(this.lb_VesMax);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.buttonPressureColor);
            this.groupBox1.Controls.Add(this.lb_VesMin);
            this.groupBox1.Controls.Add(this.lb_MehSkorostMax);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.buttonVesColor);
            this.groupBox1.Controls.Add(this.lb_MehSkorostMin);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.buttonMehSkorostColor);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.lb_GlubinaMax);
            this.groupBox1.Controls.Add(this.lb_GlubinaMin);
            this.groupBox1.Controls.Add(this.buttonGlubinaColor);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(396, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 168);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки графиков";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(244, 126);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(10, 13);
            this.label13.TabIndex = 56;
            this.label13.Text = "-";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(244, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 13);
            this.label10.TabIndex = 59;
            this.label10.Text = "-";
            // 
            // lb_RashodMax
            // 
            this.lb_RashodMax.Location = new System.Drawing.Point(260, 123);
            this.lb_RashodMax.Name = "lb_RashodMax";
            this.lb_RashodMax.Size = new System.Drawing.Size(62, 20);
            this.lb_RashodMax.TabIndex = 70;
            this.lb_RashodMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lb_RashodMax.TextChanged += new System.EventHandler(this.lb_RashodMax_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(244, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(10, 13);
            this.label11.TabIndex = 58;
            this.label11.Text = "-";
            // 
            // lb_RashodMin
            // 
            this.lb_RashodMin.Location = new System.Drawing.Point(176, 123);
            this.lb_RashodMin.Name = "lb_RashodMin";
            this.lb_RashodMin.Size = new System.Drawing.Size(62, 20);
            this.lb_RashodMin.TabIndex = 69;
            this.lb_RashodMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lb_RashodMin.TextChanged += new System.EventHandler(this.lb_RashodMin_TextChanged);
            // 
            // lb_pressureMax
            // 
            this.lb_pressureMax.Location = new System.Drawing.Point(260, 97);
            this.lb_pressureMax.Name = "lb_pressureMax";
            this.lb_pressureMax.Size = new System.Drawing.Size(62, 20);
            this.lb_pressureMax.TabIndex = 67;
            this.lb_pressureMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lb_pressureMax.TextChanged += new System.EventHandler(this.lb_pressureMax_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(114, 126);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 47;
            this.label14.Text = "Интервал";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(244, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(10, 13);
            this.label12.TabIndex = 60;
            this.label12.Text = "-";
            // 
            // buttonRashodColor
            // 
            this.buttonRashodColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRashodColor.Location = new System.Drawing.Point(92, 124);
            this.buttonRashodColor.Name = "buttonRashodColor";
            this.buttonRashodColor.Size = new System.Drawing.Size(16, 16);
            this.buttonRashodColor.TabIndex = 68;
            this.buttonRashodColor.UseVisualStyleBackColor = true;
            this.buttonRashodColor.Click += new System.EventHandler(this.buttonRashodColor_Click);
            // 
            // lb_pressureMin
            // 
            this.lb_pressureMin.Location = new System.Drawing.Point(176, 97);
            this.lb_pressureMin.Name = "lb_pressureMin";
            this.lb_pressureMin.Size = new System.Drawing.Size(62, 20);
            this.lb_pressureMin.TabIndex = 66;
            this.lb_pressureMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lb_pressureMin.TextChanged += new System.EventHandler(this.lb_pressureMin_TextChanged);
            // 
            // lb_VesMax
            // 
            this.lb_VesMax.Location = new System.Drawing.Point(260, 71);
            this.lb_VesMax.Name = "lb_VesMax";
            this.lb_VesMax.Size = new System.Drawing.Size(62, 20);
            this.lb_VesMax.TabIndex = 64;
            this.lb_VesMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lb_VesMax.TextChanged += new System.EventHandler(this.lb_VesMax_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(114, 100);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 13);
            this.label15.TabIndex = 48;
            this.label15.Text = "Интервал";
            // 
            // buttonPressureColor
            // 
            this.buttonPressureColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPressureColor.Location = new System.Drawing.Point(92, 98);
            this.buttonPressureColor.Name = "buttonPressureColor";
            this.buttonPressureColor.Size = new System.Drawing.Size(16, 16);
            this.buttonPressureColor.TabIndex = 65;
            this.buttonPressureColor.UseVisualStyleBackColor = true;
            this.buttonPressureColor.Click += new System.EventHandler(this.buttonPressureColor_Click);
            // 
            // lb_VesMin
            // 
            this.lb_VesMin.Location = new System.Drawing.Point(176, 71);
            this.lb_VesMin.Name = "lb_VesMin";
            this.lb_VesMin.Size = new System.Drawing.Size(62, 20);
            this.lb_VesMin.TabIndex = 63;
            this.lb_VesMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lb_VesMin.TextChanged += new System.EventHandler(this.lb_VesMin_TextChanged);
            // 
            // lb_MehSkorostMax
            // 
            this.lb_MehSkorostMax.Location = new System.Drawing.Point(260, 45);
            this.lb_MehSkorostMax.Name = "lb_MehSkorostMax";
            this.lb_MehSkorostMax.Size = new System.Drawing.Size(62, 20);
            this.lb_MehSkorostMax.TabIndex = 61;
            this.lb_MehSkorostMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lb_MehSkorostMax.TextChanged += new System.EventHandler(this.lb_MehSkorostMax_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(114, 74);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 49;
            this.label16.Text = "Интервал";
            // 
            // buttonVesColor
            // 
            this.buttonVesColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonVesColor.Location = new System.Drawing.Point(92, 72);
            this.buttonVesColor.Name = "buttonVesColor";
            this.buttonVesColor.Size = new System.Drawing.Size(16, 16);
            this.buttonVesColor.TabIndex = 62;
            this.buttonVesColor.UseVisualStyleBackColor = true;
            this.buttonVesColor.Click += new System.EventHandler(this.buttonVesColor_Click);
            // 
            // lb_MehSkorostMin
            // 
            this.lb_MehSkorostMin.Location = new System.Drawing.Point(176, 45);
            this.lb_MehSkorostMin.Name = "lb_MehSkorostMin";
            this.lb_MehSkorostMin.Size = new System.Drawing.Size(62, 20);
            this.lb_MehSkorostMin.TabIndex = 57;
            this.lb_MehSkorostMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lb_MehSkorostMin.TextChanged += new System.EventHandler(this.lb_MehSkorostMin_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(114, 48);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 13);
            this.label17.TabIndex = 50;
            this.label17.Text = "Интервал";
            // 
            // buttonMehSkorostColor
            // 
            this.buttonMehSkorostColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMehSkorostColor.Location = new System.Drawing.Point(92, 46);
            this.buttonMehSkorostColor.Name = "buttonMehSkorostColor";
            this.buttonMehSkorostColor.Size = new System.Drawing.Size(16, 16);
            this.buttonMehSkorostColor.TabIndex = 54;
            this.buttonMehSkorostColor.UseVisualStyleBackColor = true;
            this.buttonMehSkorostColor.Click += new System.EventHandler(this.buttonMehSkorostColor_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(244, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(10, 13);
            this.label18.TabIndex = 55;
            this.label18.Text = "-";
            // 
            // lb_GlubinaMax
            // 
            this.lb_GlubinaMax.Location = new System.Drawing.Point(260, 19);
            this.lb_GlubinaMax.Name = "lb_GlubinaMax";
            this.lb_GlubinaMax.Size = new System.Drawing.Size(62, 20);
            this.lb_GlubinaMax.TabIndex = 53;
            this.lb_GlubinaMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lb_GlubinaMax.TextChanged += new System.EventHandler(this.lb_GlubinaMax_TextChanged);
            // 
            // lb_GlubinaMin
            // 
            this.lb_GlubinaMin.Location = new System.Drawing.Point(176, 19);
            this.lb_GlubinaMin.Name = "lb_GlubinaMin";
            this.lb_GlubinaMin.Size = new System.Drawing.Size(62, 20);
            this.lb_GlubinaMin.TabIndex = 52;
            this.lb_GlubinaMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lb_GlubinaMin.TextChanged += new System.EventHandler(this.lb_GlubinaMin_TextChanged);
            // 
            // buttonGlubinaColor
            // 
            this.buttonGlubinaColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGlubinaColor.Location = new System.Drawing.Point(92, 21);
            this.buttonGlubinaColor.Name = "buttonGlubinaColor";
            this.buttonGlubinaColor.Size = new System.Drawing.Size(16, 16);
            this.buttonGlubinaColor.TabIndex = 46;
            this.buttonGlubinaColor.UseVisualStyleBackColor = true;
            this.buttonGlubinaColor.Click += new System.EventHandler(this.buttonGlubinaColor_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(114, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 13);
            this.label19.TabIndex = 51;
            this.label19.Text = "Интервал";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "График №5";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 44;
            this.label8.Text = "Грайик №4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 43;
            this.label7.Text = "График №3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "График №2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "График №1";
            // 
            // listViewGraphics
            // 
            this.listViewGraphics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listViewGraphics.FullRowSelect = true;
            this.listViewGraphics.GridLines = true;
            this.listViewGraphics.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewGraphics.HideSelection = false;
            this.listViewGraphics.Location = new System.Drawing.Point(396, 12);
            this.listViewGraphics.Name = "listViewGraphics";
            this.listViewGraphics.Size = new System.Drawing.Size(336, 127);
            this.listViewGraphics.TabIndex = 8;
            this.listViewGraphics.UseCompatibleStateImageBehavior = false;
            this.listViewGraphics.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "График";
            this.columnHeader3.Width = 76;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Параметр";
            this.columnHeader4.Width = 227;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(396, 319);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Настроить график";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(523, 319);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(108, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "Очистить график";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(396, 454);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Название панели";
            // 
            // textBoxPanelName
            // 
            this.textBoxPanelName.Location = new System.Drawing.Point(498, 451);
            this.textBoxPanelName.Name = "textBoxPanelName";
            this.textBoxPanelName.Size = new System.Drawing.Size(234, 20);
            this.textBoxPanelName.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Шрифт";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(52, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(247, 20);
            this.textBox1.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSelectFontParameter);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(396, 348);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 97);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки параметра цифровой панели";
            // 
            // buttonSelectFontParameter
            // 
            this.buttonSelectFontParameter.Location = new System.Drawing.Point(305, 31);
            this.buttonSelectFontParameter.Name = "buttonSelectFontParameter";
            this.buttonSelectFontParameter.Size = new System.Drawing.Size(25, 20);
            this.buttonSelectFontParameter.TabIndex = 17;
            this.buttonSelectFontParameter.Text = "...";
            this.buttonSelectFontParameter.UseVisualStyleBackColor = true;
            this.buttonSelectFontParameter.Click += new System.EventHandler(this.buttonSelectFontParameter_Click);
            // 
            // fontDialog
            // 
            this.fontDialog.ShowColor = true;
            // 
            // NumericPanelForm
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(743, 533);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBoxPanelName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listViewGraphics);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonEditParameter);
            this.Controls.Add(this.buttonNewParameter);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NumericPanelForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Цифровая панель";
            this.Load += new System.EventHandler(this.NumericPanelForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonNewParameter;
        private System.Windows.Forms.Button buttonEditParameter;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox lb_RashodMax;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox lb_RashodMin;
        private System.Windows.Forms.TextBox lb_pressureMax;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonRashodColor;
        private System.Windows.Forms.TextBox lb_pressureMin;
        private System.Windows.Forms.TextBox lb_VesMax;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonPressureColor;
        private System.Windows.Forms.TextBox lb_VesMin;
        private System.Windows.Forms.TextBox lb_MehSkorostMax;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button buttonVesColor;
        private System.Windows.Forms.TextBox lb_MehSkorostMin;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button buttonMehSkorostColor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox lb_GlubinaMax;
        private System.Windows.Forms.TextBox lb_GlubinaMin;
        private System.Windows.Forms.Button buttonGlubinaColor;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView listViewGraphics;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPanelName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonSelectFontParameter;
        private System.Windows.Forms.FontDialog fontDialog;
    }
}