using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Net.NetworkInformation;

using Buffering;
using GraphicComponent;

using WCF;
using WCF.WCF_Client;

using DataBase;
using NumericTable;

namespace SGT
{
    public partial class mainForm : Form
    {
        private SgtApplication _app = null;         // основное приложение

        public mainForm()
        {
            InitializeComponent();

            tMutex = new Mutex();
            _app = SgtApplication.CreateInstance();

            if (_app != null)
            {
                _app.Technology.onComplete += new EventHandler(Technology_onComplete);

                _app.DrillingPanel.init(numericTable1.Panel, graphicsSheet1.InstanceManager());
                _app.SpoPanel.init(graphicsSheet4.InstanceManager(), graphicsSheet5.InstanceManager(),
                    label26, label41, label29, label27, label37, label35, label33, label31, label43, label39);

                _app.SolutionPanel.init(graphicsSheet2.InstanceManager(), graphicsSheet3.InstanceManager(),
                    textBox1, textBox2, textBox3, textBox4, textBox7, textBox5, textBox6, textBox8, textBox9,
                    textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17, textBox18);
            }

            dStatuser = new devMnStatuser(DevStatuse);
            tStatuser = new techStatuser(TechStatuse);

            sStatuser = new mainForm.sqlStatuser(sqlStatuserF);
            startTimerDB = new dbTimerStarted(dbTimerStartedF);

            _app.DB_Manager.ServerDisconneted += new EventHandler(DB_Manager_ServerDisconneted);
            _app.DB_Manager.ServerConnected += new EventHandler(DB_Manager_ServerConnected);

            tabControl1.TabPages[0].Tag = _app.DrillingPanel;
            tabControl1.TabPages[1].Tag = _app.SolutionPanel;
            tabControl1.TabPages[2].Tag = _app.SpoPanel;

            _app.DrillingPanel.NShow = true;
        }

        protected long critical = 0;

        /// <summary>
        /// установлено соединение с сервером
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DB_Manager_ServerConnected(object sender, EventArgs e)
        {
            Invoke(sStatuser, Color.LimeGreen);
            try
            {
                if (Interlocked.Read(ref critical) == 0)
                {
                    Interlocked.Exchange(ref critical, 1);

                    Work cur_work = _app.CurrentWork;
                    if (cur_work != null)
                    {
                        Session session = cur_work.Current;
                        if (session != null)
                        {
                            if (_app.DB_Manager.State == DBState.Default)
                            {
                                bool find = false;
                                string[] dbs = _app.DB_Manager.DataBases;

                                if (dbs != null)
                                {
                                    foreach (string db in dbs)
                                    {
                                        if (db == session.DataBase)
                                        {
                                            find = true;
                                            break;
                                        }
                                    }
                                }

                                if (find)
                                {
                                    _app.DB_Manager.InitialCatalog = session.DataBase;
                                    _app.DB_Manager.LoadDB(session.DataBase);

                                    if (_app.DB_Manager.State == DBState.Loaded)
                                    {
                                        _app.DB_Manager.TurnOnSave();
                                        Invoke(startTimerDB);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// нету соединения с сервером
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DB_Manager_ServerDisconneted(object sender, EventArgs e)
        {
            Invoke(sStatuser, Color.Salmon);
        }

        private delegate void devMnStatuser(string status, Color color);
        private delegate void techStatuser();
        private delegate void sqlStatuser(Color color);
        private delegate void dbTimerStarted();

        private devMnStatuser dStatuser;
        private techStatuser tStatuser;

        private sqlStatuser sStatuser;
        private dbTimerStarted startTimerDB;
        
        private void DevStatuse(string status, Color color)
        {
            //toolStripStatusLabelDevManStatus.Text = status;
            toolStripStatusLabelDevManStatus.BackColor = color;
        }

        private void sqlStatuserF(Color color)
        {
            toolStripStatusLabel10.BackColor = color;
        }

        private void dbTimerStartedF()
        {
            timerToDBSaver.Start();
        }

        /// <summary>
        /// вывести данные на главную форму
        /// </summary>
        private void TechStatuse()
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd  dd MMMM yyyy  HH:mm:ss  ", CultureInfo.CurrentCulture);
            //lblDateTime.Text = _app.Technology.TechnologyTime.ToLongTimeString();

            lblZaboi.Text = string.Format("{0:F3}", _app.Technology.P0205.Value);
            lblInstrument.Text = string.Format("{0:F3}", _app.Technology.P0204.Value);

            lblTehEtap.Text = _app.Technology.TechnologicalStage;
            lblTehRezim.Text = _app.Technology.TechnologicalRegime;

            lblTechHook.Text = _app.Technology.TechnologicalHook;

            Work work = _app.CurrentWork;
            if (work != null)
            {
                Session session = work.Current;
                if (session != null)
                {
                    lblNomerReisa.Text = session.Number.ToString();
                }
            }
        }

        /// <summary>
        /// установлено соединение с сервером данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevManClient_onConnected(object sender, EventArgs e)
        {
            try
            {
                Invoke(dStatuser, "Подключен с серверу данных", Color.LimeGreen);
            }
            catch { }
        }

        /// <summary>
        /// разорвано соединенеи с сервером данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevManClient_onDisconnected(object sender, EventArgs e)
        {
            try
            {
            }
            catch { }
        }

        /// <summary>
        /// настраиваем параметры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void настройкаПараметровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParametersTunerForm frm = new ParametersTunerForm();
            frm.ShowDialog(this);

            _app.Technology.ActualizedParameters();

            try
            {
                VPanel[] panels = _app.OptPanels;
                if (panels != null)
                {
                    foreach (VPanel panel in panels)
                    {
                        panel.Update();
                    }
                }
            }
            catch { }
        }

        private void данныеСервераДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevManDataForm frm = new DevManDataForm();
            frm.Show();
        }

        private void настройкаТехнологииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TechTunerForm frm = new TechTunerForm();
            frm.ShowDialog(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void данныеТехнологииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TechDataForm frm = new TechDataForm();
            frm.Show();
        }

        /// <summary>
        /// изменение потока на выходе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void изменениеПотокаНаВыходеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandP0105Form frm = new CommandP0105Form();
            frm.ShowDialog(this);
        }

        private void потокПотериБуровогоРаствораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandP0106Form frm = new CommandP0106Form();
            frm.ShowDialog(this);
        }

        private void весИнструментаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandP0200Form frm = new CommandP0200Form();
            frm.ShowDialog(this);
        }

        private void длинаИнструментаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandP0202Form frm = new CommandP0202Form();
            frm.ShowDialog(this);
        }

        private void номерСвечиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandP0203Form frm = new CommandP0203Form();
            frm.ShowDialog(this);
        }

        private void подачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandP0207Form frm = new CommandP0207Form();
            frm.ShowDialog(this);
        }

        private void времяЦиркуляцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandP0212Form frm = new CommandP0212Form();
            frm.ShowDialog(this);
        }

        private void времяБуренияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandP0213Form frm = new CommandP0213Form();
            frm.ShowDialog(this);
        }

        private void глубинаЗабояToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandP0205Form frm = new CommandP0205Form();
            frm.ShowDialog(this);
        }

        private void надЗабоемToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandP0211Form frm = new CommandP0211Form();
            frm.ShowDialog(this);
        }

        // ----------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Technology_onComplete(object sender, EventArgs e)
        {
            BeginInvoke(tStatuser);
        }

        /// <summary>
        /// подача. запоминаем глубину забоя для последующего вычисления длинны инструмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            _app.Technology.P0207.ResetStartingPoint();
        }

        /// <summary>
        /// нагрузка. запоминается вес на крюке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            _app.Technology.P0200.ResetStartingPoint();
        }

        /// <summary>
        /// над забоем. длина инструмента подгоняется под глубину забоя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            _app.Technology.P0202.ModeProccess = P0202.TModeProcess.mpCMDzaboi;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _app.Technology.P0105.StartingPoint = float.NaN;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _app.Technology.P0106.StartingPoint = float.NaN;
        }

        private void настройкаПанелейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelsForm frm = new PanelsForm();
            frm.ShowDialog(this);

            VPanel[] panels = _app.OptPanels;
            if (panels != null)
            {
                foreach (VPanel panel in panels)
                {
                    if (findPanel(panel) == false)
                    {
                        InsertNewPanel(panel);
                    }
                    else
                    {
                        panel.Update();
                        foreach (TabPage page in tabControl1.TabPages)
                        {
                            if (page.Tag != null)
                            {
                                VPanel v_panel = page.Tag as VPanel;
                                if (v_panel != null)
                                {
                                    if (panel == v_panel)
                                    {
                                        page.Text = panel.VPanelName;
                                        if (v_panel.VPanelType == VPanelType.FullPanel)
                                        {
                                            SplitContainer container1 = page.Controls[0] as SplitContainer;
                                            if (container1 != null)
                                            {
                                                SplitContainer container2 = container1.Panel2.Controls[0] as SplitContainer;
                                                if (container2 != null)
                                                {
                                                    SplitContainer container3 = container2.Panel2.Controls[0] as SplitContainer;
                                                    if (container3 != null)
                                                    {
                                                        ShowPanels(v_panel as FullPanel, container1, container2, container3);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            UpdatePages();
        }

        /// <summary>
        /// актуализировать панели на главной форме
        /// </summary>
        protected void UpdatePages()
        {
            try
            {
                VPanel[] panels = _app.OptPanels;
                List<TabPage> pages = new List<TabPage>();

                foreach (TabPage page in tabControl1.TabPages)
                {
                    switch (page.Name)
                    {
                        case "tabPage1":
                            break;

                        case "tabPage2":
                            break;

                        case "tabPage3":
                            break;

                        default:

                            VPanel panel = page.Tag as VPanel;
                            if (panel != null)
                            {
                                bool finded = false;
                                foreach (VPanel item in panels)
                                {
                                    if (item == panel)
                                    {
                                        finded = true;
                                        break;
                                    }
                                }

                                if (finded == false)
                                {
                                    pages.Add(page);
                                }
                            }
                            else
                                pages.Add(page);

                            break;
                    }
                }

                foreach (TabPage page in pages)
                {
                    tabControl1.TabPages.Remove(page);
                }

                pages.Clear();
            }
            catch { }
        }

        /// <summary>
        /// проверить имеется ли панель на форме или нет
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        protected bool findPanel(VPanel panel)
        {
            try
            {
                foreach (TabPage page in tabControl1.TabPages)
                {
                    if (page.Tag != null)
                    {
                        VPanel v_panel = page.Tag as VPanel;
                        if (v_panel != null)
                        {
                            if (panel == v_panel)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Добавить панель на главную форму приложения
        /// </summary>
        /// <param name="panel">Добавляемая панель</param>
        protected void InsertNewPanel(VPanel panel)
        {
            try
            {
                switch (panel.VPanelType)
                {
                    case VPanelType.NumericPanel:

                        InsertNumericPanel(panel as NumericPanel);
                        break;

                    case VPanelType.FullPanel:

                        InsertFullPanel(panel as FullPanel);
                        break;

                    default:
                        break;
                }
            }
            catch { }
        }

        /// <summary>
        /// Добавить цифровую панель
        /// </summary>
        /// <param name="panel"></param>
        protected void InsertNumericPanel(NumericPanel panel)
        {
            if (panel != null)
            {
                TabPage page = new TabPage(panel.VPanelName);
                SplitContainer contaiter = new SplitContainer();

                GraphicsSheet g_sheet = new GraphicsSheet();
                NumericTable.NumericTable n_table = new NumericTable.NumericTable();

                n_table.Dock = DockStyle.Fill;
                n_table.BorderStyle = BorderStyle.FixedSingle;

                g_sheet.Dock = DockStyle.Fill;

                contaiter.Panel1.Controls.Add(n_table);
                contaiter.Panel2.Controls.Add(g_sheet);

                contaiter.Dock = DockStyle.Fill;

                page.Controls.Add(contaiter);
                page.Tag = panel;

                tabControl1.TabPages.Add(page);
                panel.init(n_table.Panel, g_sheet.InstanceManager());

                contaiter.SplitterDistance = panel.SplitterDistance;
            }
        }

        private void splitContainer3_Panel1_Resize(object sender, EventArgs e)
        {
            numericTable1.Panel.Redraw();
        }

        /// <summary>
        /// Добавить общую панель
        /// </summary>
        private void InsertFullPanel(FullPanel panel)
        {
            try
            {
                if (panel != null)
                {
                    TabPage page = new TabPage(panel.VPanelName);

                    SplitContainer container1 = new SplitContainer();
                    
                    container1.BorderStyle = BorderStyle.FixedSingle;
                    container1.Dock = DockStyle.Fill;

                    container1.Name = "container1";

                    SplitContainer container2 = new SplitContainer();

                    container2.BorderStyle = BorderStyle.FixedSingle;
                    container2.Dock = DockStyle.Fill;
                    
                    SplitContainer container3 = new SplitContainer();
                    
                    container3.BorderStyle = BorderStyle.FixedSingle;
                    container3.Dock = DockStyle.Fill;

                    container1.Panel2.Controls.Add(container2);
                    container2.Panel2.Controls.Add(container3);

                    GraphicsSheet g_sheet1 = new GraphicsSheet();
                    g_sheet1.Dock = DockStyle.Fill;
                    g_sheet1.BorderStyle = BorderStyle.None;

                    GraphicsSheet g_sheet2 = new GraphicsSheet();
                    g_sheet2.Dock = DockStyle.Fill;
                    g_sheet2.BorderStyle = BorderStyle.None;

                    GraphicsSheet g_sheet3 = new GraphicsSheet();
                    g_sheet3.Dock = DockStyle.Fill;
                    g_sheet3.BorderStyle = BorderStyle.None;

                    NumericTable.NumericTable n_table = new NumericTable.NumericTable();
                    n_table.Dock = DockStyle.Fill;

                    container1.Panel1.Controls.Add(n_table);
                    container2.Panel1.Controls.Add(g_sheet1);
                    container3.Panel1.Controls.Add(g_sheet2);
                    container3.Panel2.Controls.Add(g_sheet3);
                    
                    page.Controls.Add(container1);
                    page.Tag = panel;

                    tabControl1.TabPages.Add(page);

                    panel.init(n_table.Panel, g_sheet1.InstanceManager(), g_sheet2.InstanceManager(), g_sheet3.InstanceManager());

                    ShowPanels(panel, container1, container2, container3);

                    if (panel.Scale_gr_1 > 0)
                    {
                        container1.SplitterDistance = (int)(container1.Size.Width * panel.Scale_gr_1);
                    }
                    /*else
                        container1.SplitterDistance = (int)(container1.Size.Width * panel.Scale_gr_1);*/

                    if (panel.Scale_gr_2 > 0)
                    {
                        container2.SplitterDistance = (int)(container2.Size.Width * panel.Scale_gr_2);
                    }
                    /*else
                        container2.SplitterDistance = (int)(container2.Size.Width * panel.Scale_gr_2);*/

                    if (panel.Scale_gr_3 > 0)
                    {
                        container3.SplitterDistance = (int)(container3.Size.Width * panel.Scale_gr_3);
                    }
                    /*else
                        container3.SplitterDistance = (int)(container3.Size.Width * panel.Scale_gr_3);*/

                    // --------- блок инициализации общей панели ---------
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void ShowPanels(FullPanel panel, SplitContainer container1, 
            SplitContainer container2, SplitContainer container3)
        {
            try
            {
                if (panel != null)
                {
                    container1.Panel1Collapsed = false;
                    container1.Panel2Collapsed = false;

                    container2.Panel1Collapsed = false;
                    container2.Panel2Collapsed = false;

                    container3.Panel1Collapsed = false;
                    container3.Panel2Collapsed = false;

                    int total = 7;
                    if (!panel.Show_gr1) total = total & 3;
                    if (!panel.Show_gr2) total = total & 5;
                    if (!panel.Show_gr3) total = total & 6;

                    switch (total)
                    {
                        case 0:

                            container1.Panel2Collapsed = true;
                            break;

                        case 1:

                            container2.Panel1Collapsed = true;
                            container3.Panel1Collapsed = true;

                            break;

                        case 2:

                            container2.Panel1Collapsed = true;
                            container3.Panel2Collapsed = true;

                            break;

                        case 3:

                            container2.Panel1Collapsed = true;
                            break;

                        case 4:

                            container2.Panel2Collapsed = true;
                            break;

                        case 5:

                            container3.Panel1Collapsed = true;
                            break;

                        case 6:

                            container3.Panel2Collapsed = true;
                            break;

                        case 7:

                            break;

                        default:
                            break;
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// работаем с заданиями
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void заданияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorksForm frm = new WorksForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Work selected = frm.SelectedWork;
                if (selected != null)
                {
                    _app.StartNewWork(selected);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void настройкаСоединенияССерверомДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            devManConnectorForm frm = new devManConnectorForm();
            frm.ShowDialog(this);

            _app.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void настройкаСоединенияСБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBOptionsForm frm = new DBOptionsForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Work cur_work = _app.CurrentWork;
                if (cur_work != null)
                {
                    Session session = cur_work.Current;
                    if (session != null)
                    {
                        if (session.IsActived)
                        {
                            MessageBox.Show(this, "Нельзя переключить сервер БД, во время выполнения работы.", "Сообщение", 
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            return;
                        }
                    }
                }

                //if (_app.Commutator.Technology.Stages.IsWork == false)
                {
                    switch (_app.DB_Manager.State)
                    {
                        case DBState.Loaded:

                            MessageBox.Show(this, "Нельзя переключить сервер БД, если загружена база данных для проекта", "Сообщение",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            break;

                        case DBState.Saving:

                            MessageBox.Show(this, "Нельзя переключить сервер БД во время записи параметров", "Сообщение",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            break;

                        default:

                            try
                            {
                                _app.DB_Manager.UserID = frm.UserID;
                                _app.DB_Manager.Password = frm.Password;

                                _app.DB_Manager.DataSource = frm.DataSource;
                                if (_app.DB_Manager.IsConnectValid)
                                {
                                    toolStripStatusLabel10.Text = "Соединение с SQL сервер установлено";
                                }
                                else
                                {
                                    toolStripStatusLabel10.Text = "Соединение с SQL не установлено";
                                }

                                _app.Save();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// начинаем новый рейс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void начатьНовыйРейсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Work currentWork = _app.CurrentWork;
                if (currentWork != null)
                {
                    CreateNewSessionForm frm = new CreateNewSessionForm();
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        Session nSession = frm.Session;
                        if (nSession != null)
                        {
                            _app.CreateNewSession(nSession);

                            if (_app.DB_Manager.State == DBState.Saving)
                            {
                                if (timerToDBSaver.Enabled == false)
                                {
                                    timerToDBSaver.Start();
                                }
                            }
                            _app.Save();
                        }
                    }
                }

                else
                {
                    MessageBox.Show(this, "Не созданно задание!" + Microsoft.VisualBasic.Constants.vbCrLf + "Осуществить начало рейса не представляется возможным.",
                        "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected Mutex tMutex = new Mutex();   // синхронизирует таймер

        /// <summary>
        /// Передаем данные для сохранения и проверяем наличие БД и сервера БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerToDBSaver_Tick(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (tMutex.WaitOne(50))
                {
                    blocked = true;
                    DateTime now = DateTime.Now;

                    if (_app.DB_Manager.State == DBState.Saving)
                    {
                        toolStripStatusLabel4.BackColor = Color.LimeGreen;
                        Parameter[] parameters = _app.Commutator.Parameters;

                        if (parameters != null)
                        {
                            long ticks = now.Ticks;
                            foreach (Parameter parameter in parameters)
                            {
                                if (parameter.SaveToDB)
                                {
                                    if (CheckThreashold(parameter) == false)
                                    {
                                        if (now > parameter.DB_Time)
                                        {
                                            TimeSpan interval = now - parameter.DB_Time;
                                            TimeSpan pInterval = new TimeSpan(0, 0, 0, 0, parameter.IntervalToSaveToDB);

                                            if (interval.Ticks > pInterval.Ticks)
                                            {
                                                parameter.DB_Time = now;
                                                _app.DB_Manager.ToSaveParameter(parameter.Identifier, now, parameter.CurrentValue);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (now > parameter.DB_Time)
                                        {
                                            parameter.DB_Time = now;
                                            _app.DB_Manager.ToSaveParameter(parameter.Identifier, now, parameter.CurrentValue);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        toolStripStatusLabel4.BackColor = Color.Salmon;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.Unknown));
            }
            finally
            {
                if (blocked) tMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Проверить пороговое значение
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected bool CheckThreashold(Parameter parameter)
        {
            try
            {
                if (parameter != null)
                {
                    float delta = Math.Abs(parameter.LastValue - parameter.CalculatedValue);
                    if (float.IsNaN(parameter.ThresholdToBD) == false && float.IsInfinity(parameter.ThresholdToBD) == false
                        && float.IsNegativeInfinity(parameter.ThresholdToBD) == false && float.IsPositiveInfinity(parameter.ThresholdToBD) == false)
                    {
                        if (delta >= parameter.ThresholdToBD)
                        {
                            return true;
                        }
                    }
                }
            }
            catch { }
            return false;
        }

        private void диагностикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParameterCheckerForm frm = new ParameterCheckerForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        /// <summary>
        /// yfcnhfbdftv rjvfyle тальблок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void настройкаКомандыТальблокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TalblockForm frm = new TalblockForm();
            frm.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _app.DoTalblock(0);
        }

        /// <summary>
        /// Выполнить команду тальблок тальблок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void тальблокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandTalblockForm frm = new CommandTalblockForm();
            frm.ShowDialog(this);
        }

        /// <summary>
        /// проверка параметров на валидность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCheckParameters_Tick(object sender, EventArgs e)
        {
            try
            {
                button7.BackColor = Color.LimeGreen;

                if (CheckAlarm())
                {
                    button7.BackColor = Color.Salmon;
                    return;
                }

                if (CheckInValid())
                {
                    button7.BackColor = Color.Goldenrod;
                    return;
                }

                if (CheckMaximum())
                {
                    button7.BackColor = Color.DarkOrange;
                    return;
                }

                if (CheckMinimum())
                {
                    button7.BackColor = Color.Orchid;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// проверить наличие валидных параметров
        /// </summary>
        /// <returns></returns>
        protected bool CheckInValid()
        {
            try
            {
                Parameter[] parameters = _app.Commutator.Parameters;
                if (parameters != null)
                {
                    foreach (Parameter parameter in parameters)
                    {
                        if (parameter != null && !parameter.IsValidValue)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
            catch { }
            return true;
        }

        /// <summary>
        /// проверить наличие аварийных параметров
        /// </summary>
        /// <returns></returns>
        protected bool CheckAlarm()
        {
            try
            {
                Parameter[] parameters = _app.Commutator.Parameters;
                if (parameters != null)
                {
                    foreach (Parameter parameter in parameters)
                    {
                        if (parameter != null && parameter.IsValidValue)
                        {
                            if (parameter.IsControlAlarm)
                            {
                                if (parameter.CalculatedValue >= parameter.Alarm)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// проверить наличие минимальных параметров
        /// </summary>
        /// <returns></returns>
        protected bool CheckMinimum()
        {
            try
            {
                Parameter[] parameters = _app.Commutator.Parameters;
                if (parameters != null)
                {
                    foreach (Parameter parameter in parameters)
                    {
                        if (parameter != null && parameter.IsValidValue)
                        {
                            if (parameter.IsControlMinimum)
                            {
                                if (parameter.CalculatedValue <= parameter.Range.Min)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// проверить наличие максимальных параметров
        /// </summary>
        /// <returns></returns>
        protected bool CheckMaximum()
        {
            try
            {
                Parameter[] parameters = _app.Commutator.Parameters;
                if (parameters != null)
                {
                    foreach (Parameter parameter in parameters)
                    {
                        if (parameter != null && parameter.IsValidValue)
                        {
                            if (parameter.IsControlMaximum)
                            {
                                if (parameter.CalculatedValue >= parameter.Range.Max)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }

        private void калибровкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeviceManager.AddTransformationForm frm = new DeviceManager.AddTransformationForm(_app);
            frm.ShowDialog(this);

            _app.Save();
        }

        private void splitContainer3_SplitterMoved(object sender, SplitterEventArgs e)
        {
            numericTable1.Invalidate();
            //_app.DrillingPanel.SplitterDistance = splitContainer3.SplitterDistance;
        }

        private void вклВыклЗаписьВБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_app.DB_Manager.State == DBState.Saving)
            {
                _app.DB_Manager.TurnOffSave();
            }
            else
                if (_app.DB_Manager.State == DBState.Loaded)
                {
                    _app.DB_Manager.TurnOnSave();
                }
        }

        private void текущееЗаданиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentWorkForm frm = new CurrentWorkForm();
            frm.ShowDialog(this);
        }

        private void закрытьПрограммуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private Mutex tdMutex = new Mutex();
        private TimeSpan tInterval = new TimeSpan(0, 0, 0, 5, 0);

        private void timerDevManState_Tick(object sender, EventArgs e)
        {
            bool blocked = false;
            try
            {
                if (tdMutex.WaitOne(100))
                {
                    blocked = true;

                    DateTime now = DateTime.Now;
                    DateTime wcf = _app.Commutator.TimeDatAacquisition;

                    if (now > wcf)
                    {
                        TimeSpan span = now - wcf;
                        if (span.Ticks > tInterval.Ticks)
                        {
                            _app.Commutator.PingWcf();
                        }
                    }

                    if (_app.Commutator.ConnectionState == WcfConnectionState.Conected)
                    {
                        toolStripStatusLabelDevManStatus.BackColor = Color.LimeGreen;
                    }
                    else
                        if (_app.Commutator.ConnectionState == WcfConnectionState.Disconnected)
                        {
                            toolStripStatusLabelDevManStatus.BackColor = Color.Salmon;
                        }
                        else
                            if (_app.Commutator.ConnectionState == WcfConnectionState.Default)
                            {
                                Ping ping = new Ping();
                                PingOptions options = new PingOptions();

                                Uri uri = DevManClient.Uri;
                                PingReply reply = ping.Send(uri.Host, 100);

                                if (reply.Status == IPStatus.Success)
                                {
                                    _app.Commutator.ConnectToServer();
                                }

                                //_app.Commutator.ConnectToServer();
                            }
                }
            }
            finally
            {
                if (blocked) tdMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// загружаемся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_Load(object sender, EventArgs e)
        {
            BeginInvoke(tStatuser);

            if (_app.GuiMode == GuiMode.User)
            {
                настройкаПараметровToolStripMenuItem.Visible = false;
                настройкаСоединенияСБДToolStripMenuItem.Visible = false;

                настройкаКомандыТальблокToolStripMenuItem.Visible = false;
            }

            VPanel[] panels = _app.OptPanels;
            if (panels != null)
            {
                foreach (VPanel panel in panels)
                {
                    if (panel != null)
                    {
                        if (findPanel(panel) == false)
                        {
                            InsertNewPanel(panel);
                        }
                        else
                            panel.Update();

                        panel.Actualize();
                    }
                }

                if (_app.OptPanels != null && _app.OptPanels.Length > 0)
                {
                    if (_app.ShowDrilling == false)
                    {
                        tabControl1.TabPages.Remove(tabPage1);
                        _app.DrillingPanel.NShow = false;
                    }

                    if (_app.ShowSolution == false)
                    {
                        tabControl1.TabPages.Remove(tabPage2);
                        _app.SolutionPanel.NShow = false;
                    }

                    if (_app.ShowSpo == false)
                    {
                        tabControl1.TabPages.Remove(tabPage3);
                        _app.SpoPanel.NShow = false;
                    }
                }
                else
                {
                    _app.ShowDrilling = true;
                    _app.ShowSolution = true;
                    _app.ShowSpo = true;

                    _app.DrillingPanel.NShow = true;
                    _app.SolutionPanel.NShow = true;
                    _app.SpoPanel.NShow = true;
                }

            }

            //SerializeTimer.Start();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.TabPages != null)
            {
                foreach (TabPage page in tabControl1.TabPages)
                {
                    if (page != e.TabPage)
                    {
                        if (page.Tag != null)
                        {
                            VPanel panel = page.Tag as VPanel;
                            if (panel != null)
                            {
                                panel.NShow = false;
                            }
                        }
                    }
                }

                if (e.TabPage.Tag != null)
                {
                    VPanel panel = e.TabPage.Tag as VPanel;
                    if (panel != null)
                    {
                        panel.NShow = true;
                    }
                }
            }
        }

        private void SerializeTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //_app.Commutator.Serialize();
            }
            catch { }
        }

        private void управлениеПараметрамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DriveParametersForm frm = new DriveParametersForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        /// <summary>
        /// проверяем завершение работы программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                switch (e.CloseReason)
                {
                    case CloseReason.ApplicationExitCall:

                        break;

                    case CloseReason.FormOwnerClosing:

                        break;

                    case CloseReason.MdiFormClosing:

                        break;

                    case CloseReason.None:

                        break;

                    case CloseReason.TaskManagerClosing:

                        break;

                    case CloseReason.UserClosing:

                        if (MessageBox.Show(this, "Вы действительно хотите завершить работу программы?", "Внимание",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            _app.X = this.Location.X;
                            _app.Y = this.Location.Y;

                            _app.width = this.Size.Width;
                            _app.height = this.Size.Height;

                            _app.state = (int)this.WindowState;

                            _app.DrillingPanel.SplitterDistance = (float)splitContainer3.SplitterDistance / (float)splitContainer3.Size.Width;
                            _app.SolutionPanel.SplitterDistance = (float)splitContainer1.SplitterDistance / (float)splitContainer1.Size.Width;
                            _app.SpoPanel.SplitterDistance = (float)splitContainer2.SplitterDistance / (float)splitContainer2.Size.Width;

                            if (_app.OptPanels != null)
                            {
                                foreach (VPanel panel in _app.OptPanels)
                                {
                                    if (panel != null)
                                    {
                                        switch (panel.VPanelType)
                                        {
                                            case VPanelType.NumericPanel:

                                                NumericPanel n_panel = panel as NumericPanel;
                                                TabPage page = findPanelT(n_panel);
                                                if (page != null)
                                                {
                                                    foreach (Control control in page.Controls)
                                                    {
                                                        if (control is SplitContainer)
                                                        {
                                                            n_panel.SplitterDistance = (control as SplitContainer).SplitterDistance;
                                                        }
                                                    }
                                                }
                                                break;

                                            case VPanelType.FullPanel:

                                                FullPanel fullPanel = panel as FullPanel;
                                                TabPage f_page = findPanelT(fullPanel);
                                                
                                                if (f_page != null && fullPanel != null)
                                                {
                                                    SplitContainer container1 = f_page.Controls[0] as SplitContainer;
                                                    if (container1 != null)
                                                    {
                                                        SplitContainer container2 = container1.Panel2.Controls[0] as SplitContainer;
                                                        if (container2 != null)
                                                        {
                                                            SplitContainer container3 = container2.Panel2.Controls[0] as SplitContainer;
                                                            if (container3 != null)
                                                            {
                                                                fullPanel.Scale_gr_1 = (float)container1.SplitterDistance / (float)container1.Size.Width;
                                                                fullPanel.Scale_gr_2 = (float)container2.SplitterDistance / (float)container2.Size.Width;
                                                                fullPanel.Scale_gr_3 = (float)container3.SplitterDistance / (float)container3.Size.Width;
                                                            }
                                                        }
                                                    }

                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case CloseReason.WindowsShutDown:

                        break;

                    default:
                        break;
                }
            }
            catch { }
        }

        /// <summary>
        /// проверить имеется ли панель на форме или нет
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        protected TabPage findPanelT(VPanel panel)
        {
            try
            {
                foreach (TabPage page in tabControl1.TabPages)
                {
                    if (page.Tag != null)
                    {
                        VPanel v_panel = page.Tag as VPanel;
                        if (v_panel != null)
                        {
                            if (panel == v_panel)
                            {
                                return page;
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private void блокировкаРазблокировкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuiModeForm frm = new GuiModeForm();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                if (_app.GuiMode == GuiMode.Techolog)
                {
                    настройкаПараметровToolStripMenuItem.Visible = true;
                    настройкаСоединенияСБДToolStripMenuItem.Visible = true;

                    настройкаКомандыТальблокToolStripMenuItem.Visible = true;
                }
                else
                    if (_app.GuiMode == GuiMode.User)
                    {
                        настройкаПараметровToolStripMenuItem.Visible = false;
                        настройкаСоединенияСБДToolStripMenuItem.Visible = false;

                        настройкаКомандыТальблокToolStripMenuItem.Visible = false;
                    }
            }
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                if (_app.X >= 0 && _app.Y >= 0)
                {
                    this.Location = new Point(_app.X, _app.Y);
                }

                FormWindowState w_state = (FormWindowState)_app.state;
                this.WindowState = w_state;

                if (WindowState != FormWindowState.Maximized)
                {
                    if (_app.width >= 1024 && _app.height >= 768)
                    {
                        this.Size = new Size(_app.width, _app.height);
                    }
                }

                splitContainer3.SplitterDistance = (int)(splitContainer3.Size.Width * _app.DrillingPanel.SplitterDistance);
                splitContainer1.SplitterDistance = (int)(splitContainer1.Size.Width * _app.SolutionPanel.SplitterDistance);
                splitContainer2.SplitterDistance = (int)(splitContainer2.Size.Width * _app.SpoPanel.SplitterDistance);
            }
            catch { }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox frm = new AboutBox();
            frm.ShowDialog(this);
        }
    }
}