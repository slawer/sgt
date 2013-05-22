using System;
using System.Xml;
using System.Threading;
using System.Collections.Generic;

namespace SGT
{
    /// <summary>
    /// Реализует задание
    /// </summary>
    public class Work
    {
        protected string field;         // месторождение
        protected string bush;          // куст

        protected string well;          // скважина
        protected string customer;      // заказчик

        protected string performer;     // исполнитель
        protected string comment;       // текстовое описание задания на работу

        protected DateTime startTime;   // дата начала работ
        protected int starting_depth;   // стартовая глубина

        protected bool actived;         // активна работа или нет

        protected List<Session> sessions;       // сеансы работы

        protected Guid guid;                    // идентификатор задания
        protected ReaderWriterLockSlim slim;    // синхронизатор

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Work()
        {
            guid = Guid.NewGuid();
            sessions = new List<Session>();

            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            field = string.Empty;
            bush = string.Empty;

            well = string.Empty;
            customer = string.Empty;

            performer = string.Empty;
            comment = string.Empty;

            startTime = DateTime.Now;
            starting_depth = 0;

            actived = false;
        }

        /// <summary>
        /// Месторождение
        /// </summary>
        public string Field
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return field;
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
                        field = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Куст
        /// </summary>
        public string Bush
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return bush;
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
                        bush = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Скважина
        /// </summary>
        public string Well
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return well;
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
                        well = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Заказчик
        /// </summary>
        public string Customer
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return customer;
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
                        customer = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public string Performer
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return performer;
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
                        performer = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Текстовое описание задания на работу
        /// </summary>
        public string Description
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return comment;
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
                        comment = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Дата начала работ
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return startTime;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return DateTime.MinValue;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        startTime = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Стартовая глубина
        /// </summary>
        public int StartingDepth
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return starting_depth;
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
                        starting_depth = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Идентификатор задания
        /// </summary>
        public Guid Identifier
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return guid;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return Guid.Empty;
            }
        }

        /// <summary>
        /// Определяет активна работа или нет
        /// </summary>
        public Boolean IsActived
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

        // ------------------ работа с рейсами ------------------

        /// <summary>
        /// Добавить рейс
        /// </summary>
        /// <param name="session">Добавляемый рейс</param>
        public void InsertSession(Session session)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (session != null)
                    {
                        sessions.Add(session);
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Удалить рейс
        /// </summary>
        /// <param name="session">Удаляемый рейс</param>
        public void DeleteSession(Session session)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (session != null)
                    {
                        sessions.Remove(session);
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Возвращяет список рейсов
        /// </summary>
        public Session[] Sessions
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return sessions.ToArray();
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Текущий рейс
        /// </summary>
        public Session Current
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        foreach (Session session in sessions)
                        {
                            if (session != null)
                            {
                                if (session.IsActived)
                                {
                                    return session;
                                }
                            }
                        }
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Сбросить состояние всех рейсов
        /// </summary>
        public void ResetSessionsState()
        {
            if (slim.TryEnterWriteLock(100))
            {
                try
                {
                    foreach (Session session in sessions)
                    {
                        if (session != null)
                        {
                            session.IsActived = false;
                        }
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // ------------------------------------------------------

        /// <summary>
        /// Имя узла в котором сохраняется задание
        /// </summary>
        public const string WorkNodeName = "Work";

        /// <summary>
        /// месторождение
        /// </summary>
        protected const string fieldName = "field";
        
        /// <summary>
        /// куст
        /// </summary>
        protected const string bushName = "bush";

        /// <summary>
        /// скважина
        /// </summary>
        protected const string wellName = "well";
        /// <summary>
        /// заказчик
        /// </summary>
        protected const string customerName = "customer";

        /// <summary>
        /// исполнитель
        /// </summary>
        protected const string performerName = "performer";
        
        /// <summary>
        /// текстовое описание задания на работу
        /// </summary>
        protected const string commentName = "comment";

        /// <summary>
        /// дата начала работ
        /// </summary>
        protected const string startTimeName = "startTime";

        /// <summary>
        /// стартовая глубина
        /// </summary>
        protected const string starting_depthName = "starting_depth";

        /// <summary>
        /// идентификатор работы
        /// </summary>
        protected const string guidName = "guid";

        /// <summary>
        /// рейсы
        /// </summary>
        protected const string SessionsRootName = "Sessions";

        /// <summary>
        /// активна работа или нет
        /// </summary>
        protected const string IsActivedName = "actived";

        /// <summary>
        /// Сохранить задание
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранение</param>
        /// <returns>Сохраненое задание</returns>
        public XmlNode Save(XmlDocument doc)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (doc != null)
                    {
                        XmlNode root = doc.CreateElement(WorkNodeName);

                        XmlNode fieldNode = doc.CreateElement(fieldName);
                        XmlNode bushNode = doc.CreateElement(bushName);

                        XmlNode wellNode = doc.CreateElement(wellName);
                        XmlNode customerNode = doc.CreateElement(customerName);

                        XmlNode performerNode = doc.CreateElement(performerName);
                        XmlNode commentNode = doc.CreateElement(commentName);

                        XmlNode startTimeNode = doc.CreateElement(startTimeName);
                        XmlNode starting_depthNode = doc.CreateElement(starting_depthName);

                        XmlNode guidNode = doc.CreateElement(guidName);
                        XmlNode activedNode = doc.CreateElement(IsActivedName);

                        fieldNode.InnerText = field.ToString();
                        bushNode.InnerText = bush.ToString();

                        wellNode.InnerText = well.ToString();
                        customerNode.InnerText = customer.ToString();

                        performerNode.InnerText = performer.ToString();
                        commentNode.InnerText = comment.ToString();

                        startTimeNode.InnerText = startTime.ToString();
                        starting_depthNode.InnerText = starting_depth.ToString();

                        guidNode.InnerText = guid.ToString();
                        activedNode.InnerText = actived.ToString();

                        XmlNode s_root = doc.CreateElement(SessionsRootName);
                        if (sessions != null && sessions.Count > 0)
                        {
                            foreach (Session session in sessions)
                            {
                                if (session != null)
                                {
                                    XmlNode sessionNode = session.Save(doc);
                                    if (sessionNode != null)
                                    {
                                        s_root.AppendChild(sessionNode);
                                    }
                                }
                            }
                        }

                        root.AppendChild(fieldNode);
                        root.AppendChild(bushNode);
                        root.AppendChild(wellNode);
                        root.AppendChild(customerNode);
                        root.AppendChild(performerNode);
                        root.AppendChild(commentNode);
                        root.AppendChild(startTimeNode);
                        root.AppendChild(starting_depthNode);
                        root.AppendChild(guidNode);
                        root.AppendChild(activedNode);
                        root.AppendChild(s_root);

                        return root;
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }

            return null;
        }

        /// <summary>
        /// Загрузить работу
        /// </summary>
        /// <param name="root">Узел в котором содержится работа</param>
        public void Load(XmlNode root)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (root != null && root.Name == WorkNodeName)
                    {
                        if (root.HasChildNodes)
                        {
                            foreach (XmlNode child in root.ChildNodes)
                            {
                                switch (child.Name)
                                {
                                    case fieldName:

                                        try
                                        {
                                            field = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case bushName:

                                        try
                                        {
                                            bush = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case wellName:

                                        try
                                        {
                                            well = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case customerName:

                                        break;

                                    case performerName:

                                        try
                                        {
                                            performer = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case commentName:

                                        try
                                        {
                                            comment = child.InnerText;
                                        }
                                        catch { }
                                        break;

                                    case startTimeName:

                                        try
                                        {
                                            startTime = DateTime.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case starting_depthName:

                                        try
                                        {
                                            starting_depth = int.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case guidName:

                                        try
                                        {
                                            guid = new Guid(child.InnerText);
                                        }
                                        catch 
                                        {
                                            guid = Guid.Empty;
                                        }
                                        break;

                                    case IsActivedName:

                                        try
                                        {
                                            actived = bool.Parse(child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case SessionsRootName:

                                        try
                                        {
                                            LoadSessions(child);
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
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Загрузить рейсы
        /// </summary>
        /// <param name="root">Корневой узел рейсов</param>
        protected void LoadSessions(XmlNode root)
        {
            foreach (XmlNode child in root.ChildNodes)
            {
                Session session = new Session();
                session.Load(child);

                sessions.Add(session);
            }
        }
    }
}