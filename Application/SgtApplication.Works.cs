using System;
using System.IO;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует основное приложение
    /// </summary>
    public partial class SgtApplication
    {
        /// <summary>
        /// Сохранить задания
        /// </summary>
        protected void SaveWorks()
        {
            try
            {
                string projectFolderPath = string.Format("{0}\\{1}", Environment.CurrentDirectory, "works");
                DirectoryInfo info = new DirectoryInfo(projectFolderPath);

                if (info.Exists)
                {
                    SaveProjectList(info);
                }
                else
                {
                    // ---- нету папаки с проектами ----

                    info.Create();          // создаем папку с проектами
                    SaveProjectListFull(info);
                }

                CheckProjectsFolders(info);
            }
            catch { }
        }

        /// <summary>
        /// Сохранить проекты в корневом каталоге. только файл каталог , папки проетов не трогать
        /// </summary>
        /// <param name="d_info"></param>
        protected void SaveProjectList(DirectoryInfo d_info)
        {
            XmlDocument document = null;
            try
            {
                document = new XmlDocument();
                XmlNode root = document.CreateElement("works");

                foreach (Work work in works)
                {
                    XmlNode p_node = work.Save(document);
                    if (p_node != null)
                    {
                        root.AppendChild(p_node);
                    }
                }

                document.AppendChild(root);
                document.Save(string.Format("{0}\\{1}", d_info.FullName, "works.xml"));
            }
            catch { }
        }

        /// <summary>
        /// Сохарнить проекты в корневом каталоге и создавать для каждого пректа папку проекта
        /// </summary>
        /// <param name="d_info"></param>
        protected void SaveProjectListFull(DirectoryInfo d_info)
        {
            SaveProjectList(d_info);
            foreach (Work work in works)
            {
                if (work != null)
                {
                    if (work.Identifier != null)
                    {
                        string folder = work.Identifier.ToString();
                        try
                        {
                            d_info.CreateSubdirectory(folder);
                        }
                        catch { }
                    }
                }
            }
        }

        /// <summary>
        /// Загрузить проекты
        /// </summary>
        protected void LoadProjects()
        {
            XmlDocument document = null;
            try
            {
                string projectFolderPath = string.Format("{0}\\{1}", Environment.CurrentDirectory, "works");
                DirectoryInfo info = new DirectoryInfo(projectFolderPath);

                if (info.Exists)
                {
                    if (File.Exists(string.Format("{0}\\{1}", info.FullName, "works.xml")))
                    {
                        document = new XmlDocument();
                        document.Load(string.Format("{0}\\{1}", info.FullName, "works.xml"));

                        XmlNode root = document.FirstChild;
                        if (root != null && root.Name == "works")
                        {
                            if (root.HasChildNodes)
                            {
                                foreach (XmlNode child in root.ChildNodes)
                                {
                                    switch (child.Name)
                                    {
                                        case Work.WorkNodeName:

                                            try
                                            {
                                                Work work = new Work();
                                                work.Load(child);

                                                works.Add(work);
                                            }
                                            catch { }
                                            break;


                                        default:
                                            break;
                                    }
                                }
                            }

                            CheckProjectsFolders(info);
                        }
                    }
                }
                else
                {
                }
            }
            catch { }
        }

        /// <summary>
        /// проверить папки для проектов
        /// </summary>
        /// <param name="d_info"></param>
        protected void CheckProjectsFolders(DirectoryInfo d_info)
        {
            try
            {
                if (d_info != null)
                {
                    if (works.Count > 0)
                    {
                        string[] folders = Directory.GetDirectories(d_info.FullName);
                        if (folders != null)
                        {
                            foreach (Work work in works)
                            {
                                bool find = false;
                                string f_work = work.Identifier.ToString();

                                foreach (string folder in folders)
                                {                                    
                                    if (f_work == folder)
                                    {
                                        find = true;
                                        break;
                                    }
                                }

                                if (find == false)
                                {
                                    d_info.CreateSubdirectory(f_work);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        // -------------------------------------------------------------------------------

        /// <summary>
        /// Начать новую работу
        /// </summary>
        /// <param name="work">Работа которую необходимо начать</param>
        public void StartNewWork(Work work)
        {
            try
            {
                if (work != null)
                {
                    StopCurrentWork();
                    work.IsActived = true;

                    if (work.StartingDepth >= 0)
                    {
                        technology.P0205.Calculate(work.StartingDepth);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Завершить текущую работу
        /// </summary>
        public void StopCurrentWork()
        {
            try
            {
                Work work = _app.CurrentWork;
                if (work != null)
                {
                    if (db_manager.State == DataBase.DBState.Saving)
                    {
                        db_manager.TurnOffSave();
                    }

                    if (db_manager.State == DataBase.DBState.Loaded)
                    {
                        db_manager.CloseDB();
                    }

                    foreach (Work c_work in works)
                    {
                        if (c_work != null)
                        {
                            c_work.IsActived = false;
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Создать и запустить новый рейс
        /// </summary>
        /// <param name="session">рейс , который необходимо начать</param>
        public void CreateNewSession(Session session)
        {
            try
            {
                Work work = _app.CurrentWork;
                if (work != null)
                {
                    work.ResetSessionsState();

                    session.IsActived = true;
                    work.InsertSession(session);

                    if (db_manager.State == DataBase.DBState.Saving)
                    {
                        db_manager.TurnOffSave();
                    }

                    if (db_manager.State == DataBase.DBState.Loaded)
                    {
                        db_manager.CloseDB();
                    }

                    session.DataBase = string.Format("db_{0:00}_{1:00}_{2:00}_{3:00}_{4:00}_{5:00}",
                                            session.DateTime.Year,
                                            session.DateTime.Month,
                                            session.DateTime.Day,
                                            session.DateTime.Hour,
                                            session.DateTime.Minute,
                                            session.DateTime.Second);

                    if (session.DataBase != string.Empty)
                    {
                        db_manager.CreateBD(session.DataBase);
                        db_manager.LoadDB(session.DataBase);
                    }

                    if (db_manager.State == DataBase.DBState.Loaded)
                    {
                        db_manager.TurnOnSave();
                    }
                }
            }
            catch { }
        }
    }
}