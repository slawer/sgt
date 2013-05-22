using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Подача
    /// </summary>
    public class P0207 : TParameter
    {
        protected float starting_point;             // глубина забоя в момент команды Подача

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0207(Guid p_identifier)
            : base(p_identifier, "P0207", "Подача")
        {
            simple = false;
            starting_point = float.NaN;
        }

        /// <summary>
        /// Глубина забоя в момент команды Подача 
        /// </summary>
        public float StartingPoint
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return starting_point;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return float.NaN;
            }
        }

        /// <summary>
        /// Сбросить глубину забоя в момент команды Подача
        /// </summary>
        public void ResetStartingPoint()
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    starting_point = float.NaN;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Сбросить глубину забоя в момент команды Подача
        /// <param name="newValue">Новое значение точки отсчета</param>
        /// </summary>
        public void ResetStartingPoint(float newValue)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    starting_point = newValue;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Подача
        /// </summary>
        /// <param name="v1">Глубина забоя в текущий момент</param>
        public void Calculate(P0205 v1)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (!float.IsNaN(v1.Value))
                    {
                        if (float.IsNaN(starting_point))
                        {
                            starting_point = v1.Value;
                        }

                        float vNew = 0;
                        if (v1.Value >= starting_point)
                        {
                            vNew = v1.Value - starting_point;
                        }
                        else
                            starting_point = v1.Value;

                        _value = vNew;
                    }
                    else
                        _value = float.NaN;

                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // --------------------------- сохранение параметра ---------------------------

        /// <summary>
        /// Имя узла в котором сохраняется источник значения для параметра
        /// </summary>
        private const string StartingPointName = "starting_point";

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
                        XmlNode StartingPointNode = document.CreateElement(StartingPointName);

                        StartingPointNode.InnerText = starting_point.ToString();
                        root.AppendChild(StartingPointNode);

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
                                    case StartingPointName:

                                        try
                                        {
                                            starting_point = float.Parse(Child.InnerText);
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