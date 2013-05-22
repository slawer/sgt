using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Газ Д3 (CH4) Датчик
    /// </summary>
    public class P06_2 : TParameter
    {
        protected float lower;              // нижняя граница
        protected float upper;              // верхняя граница

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P06_2(Guid p_identifier)
            : base(p_identifier, "P06_2", "Газ Д3 Датчик")
        {
            lower = 20;
            upper = 50;
        }

        /// <summary>
        /// Определяет нижнюю границу уровня тревоги для параметра Газ Д3
        /// </summary>
        public float Lower
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return lower;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        lower = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет верхнюю границу уровня тревоги для параметра Газ Д3
        /// </summary>
        public float Upper
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return upper;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        upper = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        // --------------------------- сохранение параметра ---------------------------

        /// <summary>
        /// Имя узла в котором сохраняется нижняя граница
        /// </summary>
        private const string LowerName = "Lower";

        /// <summary>
        /// Имя узла в котором сохраняется верхняя граница
        /// </summary>
        private const string UpperName = "Upper";

        /// <summary>
        /// Сохранить параметр в Xml узел
        /// </summary>
        /// <param name="document">XML документ, куда будет добавлен данный XmlNode</param>
        /// <returns>Узел в котором сохранен параметр</returns>
        public override XmlNode Save(XmlDocument document)
        {
            XmlNode root = base.Save(document);
            if (root != null)
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        XmlNode LowerNode = document.CreateElement(LowerName);
                        XmlNode UpperNode = document.CreateElement(UpperName);

                        LowerNode.InnerText = lower.ToString();
                        UpperNode.InnerText = upper.ToString();

                        root.AppendChild(LowerNode);
                        root.AppendChild(UpperNode);

                        return root;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Загрузить параметр из Xml узла
        /// </summary>
        /// <param name="Node">Xml узел в котором сохранен параметр</param>        
        public override void Load(XmlNode Node)
        {
            base.Load(Node);
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (Node != null && Node.HasChildNodes)
                    {
                        if (Node.Name == RootName)
                        {
                            foreach (XmlNode Child in Node.ChildNodes)
                            {
                                switch (Child.Name)
                                {
                                    case LowerName:

                                        try
                                        {
                                            lower = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case UpperName:

                                        try
                                        {
                                            upper = float.Parse(Child.InnerText);
                                        }
                                        catch { }
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

        // ----------------------------------------------------------------------------
    }
}