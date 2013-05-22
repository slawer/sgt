using System;
using System.IO;
using System.Xml;
using System.Media;
using System.Globalization;
using System.Collections.Generic;

using DataBase;

namespace SGT
{
    /// <summary>
    /// Реализует основное приложение
    /// </summary>
    public partial class SgtApplication
    {
        protected static SgtApplication _app = null;        // приложение

        // --------------------------------------------------------------------------

        protected Commutator commutator = null;         // коммутатор
        protected Technology technology = null;         // технология

        protected DataBaseManager db_manager = null;    // осуществляет работу с БД

        protected List<Work> works = null;              // проекты

        protected SpoPanel s_panel = null;              // отображает данные панели экран СПО
        protected DrillingPanel d_panel = null;         // отображает данные панели буровая площадка
        protected SolutionPanel sol_panel = null;       // отображает данные панели параметры бурового раствора

        protected List<VPanel> panels;                  // дополнительные панели
        protected GuiMode guiMode = GuiMode.User;       // режим работы gui

        protected bool show_drilling;                   // отображать или нет панель Бурение
        protected bool show_solution;                   // отображать или нет панель Параметры раствора
        protected bool show_spo;                        // отображать или нет панель СПО
        
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        protected SgtApplication()
        {
            c_slim = new System.Threading.ReaderWriterLockSlim();

            works = new List<Work>();

            commutator = new Commutator();
            technology = new Technology();

            db_manager = new DataBaseManager();

            commutator.SaverTechData = technology.SaverTechnologyData;
            //commutator.onUpdated += new CommutatorEventHandler(technology.Calculate);

            technology.onComplete += new EventHandler(technology_onComplete);
            
            s_panel = new SpoPanel(this);
            d_panel = new DrillingPanel(this);

            sol_panel = new SolutionPanel(this);

            panels = new List<VPanel>();

            show_drilling = true;
            show_solution = true;
            show_spo = true;
        }

        /// <summary>
        /// Определяет режим в котором отображать интерфейс
        /// </summary>
        public GuiMode GuiMode
        {
            get
            {
                return guiMode;
            }

            set
            {
                guiMode = value;
            }
        }

        /// <summary>
        /// Возвращяет коммутатор
        /// </summary>
        public Commutator Commutator
        {
            get
            {
                return commutator;
            }
        }

        /// <summary>
        /// Возвращяет технологию
        /// </summary>
        public Technology Technology
        {
            get
            {
                return technology;
            }
        }

        /// <summary>
        /// Возвращяет управляющего базами данных
        /// </summary>
        public DataBaseManager DB_Manager
        {
            get
            {
                return db_manager;
            }
        }

        /// <summary>
        /// Список работ
        /// </summary>
        public List<Work> Works
        {
            get
            {
                return works;
            }
        }

        /// <summary>
        /// Текущая работа
        /// </summary>
        public Work CurrentWork
        {
            get
            {
                foreach (Work work in works)
                {
                    if (work.IsActived)
                    {
                        return work;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Панель Буровая площадка
        /// </summary>
        public DrillingPanel DrillingPanel
        {
            get
            {
                return d_panel;
            }
        }

        /// <summary>
        /// Панель Экран СПО
        /// </summary>
        public SpoPanel SpoPanel
        {
            get
            {
                return s_panel;
            }
        }

        /// <summary>
        /// Панель Параметры бурового раствора
        /// </summary>
        public SolutionPanel SolutionPanel
        {
            get
            {
                return sol_panel;
            }
        }

        /// <summary>
        /// Возвращяет список панелей отображающих данные
        /// </summary>
        public VPanel[] Panels
        {
            get
            {
                VPanel[] panels = new VPanel[3];
                
                panels[0] = DrillingPanel;
                panels[1] = SolutionPanel;
                panels[2] = SpoPanel;

                return panels;
            }
        }

        /// <summary>
        /// Дополнительные панели
        /// </summary>
        public VPanel[] OptPanels
        {
            get
            {
                return panels.ToArray();
            }
        }

        /// <summary>
        /// отображать или нет панель Бурение
        /// </summary>
        public bool ShowDrilling
        {
            get { return show_drilling; }
            set { show_drilling = value; }
        }

        /// <summary>
        /// отображать или нет панель Параметры раствора
        /// </summary>
        public bool ShowSolution
        {
            get { return show_solution; }
            set { show_solution = value; }
        }

        /// <summary>
        /// отображать или нет панель СПО
        /// </summary>
        public bool ShowSpo
        {
            get { return show_spo; }
            set { show_spo = value; }
        }

        /// <summary>
        /// Добавить панель для отображения
        /// </summary>
        /// <param name="panel">Добавляемая панель</param>
        public void InsertPanel(VPanel panel)
        {
            panels.Add(panel);
        }

        /// <summary>
        /// Удалить панель
        /// </summary>
        /// <param name="panel">Удаляемая панель</param>
        public void RemovePanel(VPanel panel)
        {
            panels.Remove(panel);
        }

        /// <summary>
        /// Получить основное приложение
        /// </summary>
        /// <returns></returns>
        public static SgtApplication CreateInstance()
        {
            if (_app == null)
            {
                _app = new SgtApplication();
                
                _app.Initialize();
                ErrorHandler.InitializeErrorHandler();
            }

            return _app;
        }

        /// <summary>
        /// Выполнить инициализацию приложения
        /// </summary>
        protected void Initialize()
        {
            try
            {
            }
            catch { }
        }

        /// <summary>
        /// Подключиться к серверу данных
        /// </summary>
        public void Connect()
        {
            try
            {
                commutator.ConnectToServer();

                /*Work cur_work = CurrentWork;
                if (cur_work != null)
                {
                    Session session = cur_work.Current;
                    if (session != null)
                    {
                        if (db_manager.State == DBState.Default)
                        {
                            bool find = false;
                            string[] dbs = db_manager.DataBases;

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
                                db_manager.InitialCatalog = session.DataBase;
                                db_manager.LoadDB(session.DataBase);
                            }
                        }
                    }
                }*/
            }
            catch { }
        }

        /// <summary>
        /// Папка в которой сохранены ресурсы параметров
        /// </summary>
        protected const string ParametersDir = "Parameters";

        /// <summary>
        /// Название файла аварийного значения
        /// </summary>
        protected const string ParameterAlarmFile = "Alarm.wav";

        /// <summary>
        /// Название файла максимального значения
        /// </summary>
        protected const string ParameterMaximumFile = "maximum.wav";

        /// <summary>
        /// Название файла минимального значения
        /// </summary>
        protected const string ParameterMinimumFile = "minimum.wav";


        private DateTime lastTime = DateTime.MinValue;                  // время воспроизведения файла
        private TimeSpan tInterval = new TimeSpan(0, 0, 0, 15, 0);      // время тишины для проигрывания файла

        /// <summary>
        /// Обработать событие технолгии
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        protected void technology_onComplete(object sender, EventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                TimeSpan span = now - lastTime;

                if (span.Ticks >= tInterval.Ticks)
                {
                    string currentDir = Environment.CurrentDirectory;
                    if (Directory.Exists(string.Format("{0}\\{1}", currentDir, ParametersDir)))
                    {
                        int p_number = 1;
                        Parameter[] parameters = commutator.Parameters;

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
                                            if (PlayAlarm(p_number))
                                            {
                                                lastTime = now;
                                                break;
                                            }
                                        }
                                    }

                                    if (parameter.IsControlMaximum)
                                    {
                                        if (parameter.CalculatedValue >= parameter.Range.Max)
                                        {                                                
                                            if (PlayMaximum(p_number))
                                            {
                                                lastTime = now;
                                                break;
                                            }
                                        }
                                    }

                                    if (parameter.IsControlMinimum)
                                    {
                                        if (parameter.CalculatedValue <= parameter.Range.Min)
                                        {
                                            if (PlayMinimum(p_number))
                                            {
                                                lastTime = now;
                                                break;
                                            }
                                        }
                                    }
                                    
                                }

                                p_number = p_number + 1;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        SoundPlayer sp = new SoundPlayer();

        /// <summary>
        /// Воспроизвести файл Блокировочное значение параметра
        /// </summary>
        /// <param name="p_number">Номер параметра</param>
        protected bool PlayMaximum(int p_number)
        {
            try
            {
                string currentDir = Environment.CurrentDirectory;
                string parameter_path = string.Format("{0}\\{1}\\{2}", currentDir, ParametersDir,
                    string.Format("{0:D4}", p_number));

                if (Directory.Exists(parameter_path))
                {
                    string file = string.Format("{0}\\{1}", parameter_path, ParameterMaximumFile);
                    if (File.Exists(file))
                    {
                        sp.SoundLocation = file;
                        sp.Play();

                        return true;
                    }
                }
            }
            catch 
            {
                System.Windows.Forms.MessageBox.Show("PlayBlocking");
            }
            return false;
        }

        /// <summary>
        /// Воспроизвести файл Блокировочное значение параметра
        /// </summary>
        /// <param name="p_number">Номер параметра</param>
        protected bool PlayMinimum(int p_number)
        {
            try
            {
                string currentDir = Environment.CurrentDirectory;
                string parameter_path = string.Format("{0}\\{1}\\{2}", currentDir, ParametersDir,
                    string.Format("{0:D4}", p_number));

                if (Directory.Exists(parameter_path))
                {
                    string file = string.Format("{0}\\{1}", parameter_path, ParameterMinimumFile);
                    if (File.Exists(file))
                    {
                        sp.SoundLocation = file;
                        sp.Play();

                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Воспроизвести файл Аварийное значение параметра
        /// </summary>
        /// <param name="p_number">Номер параметра</param>
        protected bool PlayAlarm(int p_number)
        {
            try
            {
                string currentDir = Environment.CurrentDirectory;
                string parameter_path = string.Format("{0}\\{1}\\{2}", currentDir, ParametersDir,
                    string.Format("{0:D4}", p_number));

                if (Directory.Exists(parameter_path))
                {
                    string file = string.Format("{0}\\{1}", parameter_path, ParameterAlarmFile);
                    if (File.Exists(file))
                    {
                        sp.SoundLocation = file;
                        sp.Play();

                        return true;
                    }
                }
            }
            catch 
            {
                System.Windows.Forms.MessageBox.Show("PlayAlarm");
            }
            return false;
        }

        /// <summary>
        /// Выделить число с плавающей точкой из строки
        /// </summary>
        /// <param name="single">Строка содержащая число</param>
        /// <returns>Значение или Nan если не удалось выполнить преобразование</returns>
        public static float ParseSingle(string single)
        {
            try
            {
                string ds = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                string value = single;

                value = value.Replace(".", ds);
                value = value.Replace(",", ds);

                return float.Parse(value);
            }
            catch
            { }

            return float.NaN;
        }

        /// <summary>
        /// Выделить число с плавающей точкой из строки
        /// </summary>
        /// <param name="single">Строка содержащая число</param>
        /// <returns>Значение или Nan если не удалось выполнить преобразование</returns>
        public static double ParseDouble(string single)
        {
            try
            {
                string ds = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                string value = single;

                value = value.Replace(".", ds);
                value = value.Replace(",", ds);

                return double.Parse(value);
            }
            catch
            { }

            return double.NaN;
        }
        /*
        /// <summary>
        /// Получить параметр, по его индексу в срезе данных
        /// </summary>
        /// <param name="IndexOnSlice">Индекс параметра в срезе данных</param>
        /// <returns>Параметр, который соответствует индексу в срезе данных</returns>
        public Parameter GetParameter(int IndexOnSlice)
        {
            try
            {
                foreach (Parameter parameter in commutator.Parameters)
                {
                    if (parameter != null)
                    {
                        if (parameter.Channel != null)
                        {
                            if (IndexOnSlice == parameter.Channel.Number)
                            {
                                return parameter;
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }*/

        /// <summary>
        /// Получить параметр, по его индексу в срезе данных
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        /// <returns>Параметр, который соответствует индексу в срезе данных</returns>
        public Parameter GetParameter(Guid Identifier)
        {
            try
            {
                if (Identifier != Guid.Empty)
                {
                    foreach (Parameter parameter in commutator.Parameters)
                    {
                        if (parameter != null)
                        {
                            if (parameter.Identifier == Identifier)
                            {
                                return parameter;
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        // ------------------------------ сохранение/загрузка ------------------------------

        /// <summary>
        /// имя конфигурационного файла
        /// </summary>
        protected const string sgt_cfg_file_name = "sgt.xml";

        /// <summary>
        /// Корневой узел конфигурации
        /// </summary>
        protected const string RootName = "sgt_configuration";

        /// <summary>
        /// корневой узел в котором сохраняется конфигурация БД
        /// </summary>
        protected const string DataBaseName = "data_base";

        /// <summary>
        /// узел котором сохраняется адрес сервера БД
        /// </summary>
        protected const string dataSourceName = "datasource";

        /// <summary>
        /// имя узла в котором сохраняется имя пользователя дл БД
        /// </summary>
        protected const string userIDName = "userid";

        /// <summary>
        /// имя узла в котором сохраняется пароль пользователя БД
        /// </summary>
        protected const string passwordName = "password";

        /// <summary>
        /// имя узла в котором сохраняется номер устройства которому отсылать команду тальблок
        /// </summary>
        protected const string d_numberName = "d_number";
        
        /// <summary>
        /// имя узла в котором сохраняется коэффициент тальблока
        /// </summary>
        protected const string k_talblockName = "k_talblock";

        /// <summary>
        /// геометрия главного окна
        /// </summary>
        public int X, Y, state, width, height;

        /// <summary>
        /// Сохранить конфигурацию приложения
        /// </summary>
        public void Save()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                XmlElement root = doc.CreateElement(RootName);

                doc.AppendChild(root);

                XmlNode d_numberNode = doc.CreateElement(d_numberName);
                XmlNode k_talblockNode = doc.CreateElement(k_talblockName);

                XmlNode X_Node = doc.CreateElement("X");
                XmlNode Y_Node = doc.CreateElement("Y");

                XmlNode width_Node = doc.CreateElement("width");
                XmlNode height_Node = doc.CreateElement("height");

                XmlNode statusNode = doc.CreateElement("status");

                XmlNode show_drillingNode = doc.CreateElement("show_drilling");
                XmlNode show_solutionNode = doc.CreateElement("show_solution");
                XmlNode show_spoNode = doc.CreateElement("show_spo");

                d_numberNode.InnerText = d_number.ToString();
                k_talblockNode.InnerText = k_talblock.ToString();

                X_Node.InnerText = X.ToString();
                Y_Node.InnerText = Y.ToString();

                width_Node.InnerText = width.ToString();
                height_Node.InnerText = height.ToString();

                statusNode.InnerText = state.ToString();

                show_drillingNode.InnerText = show_drilling.ToString();
                show_solutionNode.InnerText = show_solution.ToString();
                show_spoNode.InnerText = show_spo.ToString();

                root.AppendChild(d_numberNode);
                root.AppendChild(k_talblockNode);

                root.AppendChild(X_Node);
                root.AppendChild(Y_Node);

                root.AppendChild(width_Node);
                root.AppendChild(height_Node);

                root.AppendChild(statusNode);

                root.AppendChild(show_drillingNode);
                root.AppendChild(show_solutionNode);
                root.AppendChild(show_spoNode);

                commutator.Save(doc, root);
                technology.Save(doc, root);

                XmlElement panelsRoot = doc.CreateElement("Panels");
                root.AppendChild(panelsRoot);

                VPanel[] panels = Panels;
                if (panels != null)
                {
                    foreach (VPanel panel in panels)
                    {
                        if (panel != null)
                        {
                            XmlNode p_node = panel.Save(doc);
                            if (p_node != null)
                            {
                                panelsRoot.AppendChild(p_node);
                            }
                        }
                    }
                }

                VPanel[] optPanels = OptPanels;
                if (optPanels != null)
                {
                    foreach (VPanel panel in optPanels)
                    {
                        if (panel != null)
                        {
                            XmlNode p_node = panel.Save(doc);
                            if (p_node != null)
                            {
                                panelsRoot.AppendChild(p_node);
                            }
                        }
                    }
                }

                SaveDataBase(doc, root);
                doc.Save(string.Format("{0}\\{1}", Environment.CurrentDirectory, sgt_cfg_file_name));

                SaveWorks();
            }
            catch { }
        }

        /// <summary>
        /// Сохранить конфигурацию БД
        /// </summary>
        /// <param name="doc">Xml документ, в который осуществляется сохранение настроек БД</param>
        protected void SaveDataBase(XmlDocument doc, XmlNode root)
        {
            try
            {
                XmlNode db_root = doc.CreateElement(DataBaseName);

                XmlNode dataSourceNode = doc.CreateElement(dataSourceName);
                XmlNode userIDNode = doc.CreateElement(userIDName);

                XmlNode passwordNode = doc.CreateElement(passwordName);

                dataSourceNode.InnerText = db_manager.DataSource;
                userIDNode.InnerText = db_manager.UserID;

                passwordNode.InnerText = db_manager.Password;

                db_root.AppendChild(dataSourceNode);
                db_root.AppendChild(userIDNode);

                db_root.AppendChild(passwordNode);

                root.AppendChild(db_root);
            }
            catch { }
        }

        /// <summary>
        /// Загрузить конфигурацию приложения
        /// </summary>
        public void Load()
        {
            XmlDocument document = null;
            try
            {
                string path = Environment.CurrentDirectory;
                string totalPathCfg = string.Format("{0}\\{1}", path, sgt_cfg_file_name);

                if (System.IO.File.Exists(totalPathCfg))
                {
                    document = new XmlDocument();

                    document.Load(totalPathCfg);
                    XmlNode root = document.FirstChild;

                    if (root != null)
                    {
                        if (root.Name == RootName)
                        {
                            if (root.HasChildNodes)
                            {
                                foreach (XmlNode child in root.ChildNodes)
                                {
                                    switch (child.Name)
                                    {
                                        case d_numberName:

                                            try
                                            {
                                                d_number = int.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case k_talblockName:

                                            try
                                            {
                                                k_talblock = int.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case "X":

                                            try
                                            {
                                                X = int.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case "Y":

                                            try
                                            {
                                                Y = int.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case "status":

                                            try
                                            {
                                                state = int.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case "width":

                                            try
                                            {
                                                width = int.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case "height":

                                            try
                                            {
                                                height = int.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case "show_drilling":
                                            
                                            try
                                            {
                                                show_drilling = bool.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case "show_solution":

                                            try
                                            {
                                                show_solution = bool.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case "show_spo":

                                            try
                                            {
                                                show_spo = bool.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;


                                        case Commutator.RootName:

                                            commutator.Load(child);
                                            break;

                                        case Technology.RootName:

                                            technology.Load(child);
                                            break;

                                        case DataBaseName:

                                            LoadDataBase(child);
                                            break;

                                        case "Panels":

                                            LoadPanels(child);
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                LoadProjects();
            }
            catch { }
        }

        /// <summary>
        /// Загрузить насройки панелей
        /// </summary>
        /// <param name="root">Корневой узел настроек всех панелей</param>
        protected void LoadPanels(XmlNode root)
        {
            try
            {
                if (root != null)
                {
                    if (root.HasChildNodes)
                    {
                        foreach (XmlNode child in root.ChildNodes)
                        {
                            switch (child.Name)
                            {
                                case "DrillingPanel":

                                    DrillingPanel.Load(child);
                                    break;

                                case "SolutionPanel":

                                    SolutionPanel.Load(child);
                                    break;

                                case "SpoPanel":

                                    SpoPanel.Load(child);
                                    break;

                                case "numericPanel":

                                    try
                                    {
                                        NumericPanel n_panel = new NumericPanel(this);
                                        n_panel.Load(child);

                                        panels.Add(n_panel);
                                    }
                                    catch { }
                                    break;

                                case "fullPanel":

                                    try
                                    {
                                        FullPanel fullPanel = new FullPanel();
                                        fullPanel.Load(child);

                                        panels.Add(fullPanel);
                                    }
                                    catch { }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Загрузить настройки БД
        /// </summary>
        /// <param name="root">Корневой узел настроек БД</param>
        protected void LoadDataBase(XmlNode root)
        {
            try
            {
                if (root != null && root.Name == DataBaseName)
                {
                    if (root.HasChildNodes)
                    {
                        foreach (XmlNode child in root.ChildNodes)
                        {
                            switch (child.Name)
                            {
                                case dataSourceName:

                                    try
                                    {
                                        db_manager.DataSource = child.InnerText;
                                    }
                                    catch { }
                                    break;

                                case userIDName:

                                    try
                                    {
                                        db_manager.UserID = child.InnerText;
                                    }
                                    catch { }
                                    break;

                                case passwordName:

                                    try
                                    {
                                        db_manager.Password = child.InnerText;
                                    }
                                    catch { }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        // ---------------------------------------------------------------------------------
    }

    public enum GuiMode
    {
        /// <summary>
        /// Режим настройки
        /// </summary>
        Techolog,

        /// <summary>
        /// Режим пользователя
        /// </summary>
        User,

        /// <summary>
        /// По умолчанию. Режим настройки
        /// </summary>
        Default
    }
}