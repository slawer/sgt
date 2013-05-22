using System;
using System.Xml;
using System.Threading;

namespace SGT
{
    /// <summary>
    /// Реализует сеанс работы
    /// </summary>
    public class Session
    {
        protected int number = -1;                      // номер сеанса
        protected string description;                   // описание сеанса
        
        protected string data_base;                     // база данных в которую осуществляется запись данных
        protected bool actived = false;                 // рейс активен или нет

        protected DateTime time;                        // текущая дата и время начала рейса

        protected ReaderWriterLockSlim slim = null;     // синхронизатор

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Session()
        {
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            number = -1;
            time = DateTime.Now;

            data_base = string.Empty;
            description = string.Empty;
        }

        /// <summary>
        /// Номер сеанса
        /// </summary>
        public int Number
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return number;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        number = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Описание сеанса
        /// </summary>
        public string Description
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return description;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        description = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Рейс активен или нет
        /// </summary>
        public bool IsActived
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return actived;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        actived = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет название базы данных в которую осуществляется запись
        /// </summary>
        public string DataBase
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return data_base;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        data_base = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Дата и Время начала рейса
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                return time;
            }
        }

        // ------------- сохраняемся/загружаемся -------------

        public const string SessionRootName = "Session";

        /// <summary>
        /// Узел в котором сохраняется номер сеанса
        /// </summary>
        protected const string numberName = "number";

        /// <summary>
        /// Узел в котором сохраняется описание сеанса
        /// </summary>
        protected const string descriptionName = "description";

        /// <summary>
        /// Узел в котором сохраняется рейс активен или нет
        /// </summary>
        protected const string activedName = "actived";

        /// <summary>
        /// Узел в котором сохраняется имя БД в которую осуществляется запись данных
        /// </summary>
        protected const string DataBaseName = "data_base";

        /// <summary>
        /// время начала сенса
        /// </summary>
        protected const string DateTimeName = "date_time";

        /// <summary>
        /// Сохранить рейс
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранение рейса</param>
        /// <returns>Сохраненный рейс</returns>
        public XmlNode Save(XmlDocument doc)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    XmlNode root = doc.CreateElement(SessionRootName);

                    XmlNode numberNode = doc.CreateElement(numberName);
                    XmlNode descriptionNode = doc.CreateElement(descriptionName);

                    XmlNode activedNode = doc.CreateElement(activedName);
                    XmlNode dataBaseNode = doc.CreateElement(DataBaseName);
                    
                    XmlNode dateTimeNode = doc.CreateElement(DateTimeName);
                    
                    numberNode.InnerText = number.ToString();
                    descriptionNode.InnerText = description.ToString();

                    activedNode.InnerText = actived.ToString();
                    dataBaseNode.InnerText = data_base;

                    dateTimeNode.InnerText = time.ToString();

                    root.AppendChild(numberNode);
                    root.AppendChild(descriptionNode);
                    root.AppendChild(activedNode);
                    root.AppendChild(dataBaseNode);
                    root.AppendChild(dateTimeNode);

                    return root;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }

            return null;
        }

        /// <summary>
        /// Загружаем рейс
        /// </summary>
        /// <param name="root">Корневой узел в котором находятся данные рейса</param>
        public void Load(XmlNode root)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (root != null && root.Name == SessionRootName)
                    {
                        if (root.HasChildNodes)
                        {
                            foreach (XmlNode child in root.ChildNodes)
                            {
                                if (child != null)
                                {
                                    switch (child.Name)
                                    {

                                        case numberName:

                                            try
                                            {
                                                number = int.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case descriptionName:

                                            try
                                            {
                                                description = child.InnerText;
                                            }
                                            catch { }
                                            break;

                                        case activedName:

                                            try
                                            {
                                                actived = bool.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case DataBaseName:

                                            try
                                            {
                                                data_base = child.InnerText;
                                            }
                                            catch { }
                                            break;

                                        case DateTimeName:

                                            try
                                            {
                                                time = DateTime.Parse(child.InnerText);
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
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // ---------------------------------------------------
    }
}