using System;
using System.Xml;
using System.Threading;
using System.Collections.Generic;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Расход на входе Ходам Насоса АСУ
    /// </summary>
    public class P0112 : TParameter
    {
        protected List<IdealFlowPair> pairs;            // пары значений расход/диаметр
        protected ReaderWriterLockSlim p_slim;          // синхронизатор списка пар значений расход/диаметр

        protected float _pump_moves = 125;              // ходы насоса, по умолчанию 125 об/мин

        protected float scale_factor_1;                 // коэффициент пересчета для насоса 1
        protected float scale_factor_2;                 // коэффициент пересчета для насоса 2

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0112(Guid p_identifier)
            : base(p_identifier, "P0112", "Расход на входе Ходам Насоса АСУ")
        {
            simple = false;

            pairs = new List<IdealFlowPair>();
            p_slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Возвращяет ходы насоса об/мин
        /// </summary>
        public float PumpMovies
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return _pump_moves;
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
                        _pump_moves = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет коэффициент пересчета для первого насоса
        /// </summary>
        public float ScaleFactorPump1
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return scale_factor_1;
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
                        scale_factor_1 = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет коэффициент пересчета для первого насоса
        /// </summary>
        public float ScaleFactorPump2
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return scale_factor_2;
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
                        scale_factor_2 = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет список пар значений расход/диаметр
        /// </summary>
        public IdealFlowPair[] Pairs
        {
            get
            {
                if (p_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return pairs.ToArray();
                    }
                    finally
                    {
                        p_slim.ExitReadLock();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Добавить с конец списка пару значений расход/диаметр
        /// </summary>
        /// <param name="pair">добавляемая пара значений расход/диаметр</param>
        public void InsertPair(IdealFlowPair pair)
        {
            if (p_slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (pair != null)
                    {
                        pairs.Add(pair);
                    }
                }
                finally
                {
                    p_slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Удалить из списка пару значений расход/диаметр
        /// </summary>
        /// <param name="pair">Удаляемая пара значений расход/диаметр</param>
        public void RemovePair(IdealFlowPair pair)
        {
            if (p_slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (pair != null)
                    {
                        pairs.Remove(pair);
                    }
                }
                finally
                {
                    p_slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Очистить список пар значений расход/диаметр
        /// </summary>
        public void Clear()
        {
            if (p_slim.TryEnterWriteLock(300))
            {
                try
                {
                    pairs.Clear();
                }
                finally
                {
                    p_slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Вычисление расхода жидкости в зависимости от диаметра и ходов насоса
        /// </summary>
        /// <param name="pump_movies">Ходы насоса</param>
        /// <param name="pump_diameter">Диаметр насоса</param>
        /// <returns>Расход жидкости</returns>
        protected float CalculateFlow(float pump_movies, float pump_diameter)
        {
            IdealFlowPair[] _pairs = Pairs;
            if (_pairs != null)
            {
                float flow = float.NaN;
                foreach (IdealFlowPair _pair in _pairs)
                {
                    int diam = (int)pump_diameter;
                    int ideal_diam = (int)_pair.Diameter;

                    if (ideal_diam == diam)
                    {
                        flow = _pair.Flow;
                        break;
                    }
                }

                if (float.IsNaN(flow))
                {
                    return float.NaN;
                }
                else
                {
                    return (flow * pump_movies) / PumpMovies;
                }
            }

            return float.NaN;
        }

        /// <summary>
        /// Вычисляет значение параметра Расход на входе по Ходам Насоса с Аналогового сигнала
        /// </summary>
        /// <param name="pump1">Ходы насоса 1</param>
        /// <param name="pump2">Ходы насоса 2</param>
        /// <param name="diameter1">Диаметр цилиндра насоса 1</param>
        /// <param name="diameter2">Диаметр цилиндра насоса 2</param>
        public void Calculate(P0011 pump1, P11_1 pump2, P0014 diameter1, P14_1 diameter2)
        {
            float v = float.NaN;

            if (!float.IsNaN(pump1.Value))
            {
                if (!float.IsNaN(diameter1.Value))
                {
                    v = CalculateFlow(pump1.Value, diameter1.Value) * scale_factor_1;
                }
            }

            if (!float.IsNaN(pump2.Value))
            {   
                if (!float.IsNaN(diameter2.Value))
                {
                    float v2 = CalculateFlow(pump2.Value, diameter2.Value) * scale_factor_2;
                    if (!float.IsNaN(v2))
                    {
                        if (float.IsNaN(v)) v = 0;
                        v = v + v2;
                    }
                }
            }

            _value = v;
        }

        // --------------------------- сохранение параметра ---------------------------

        /// <summary>
        /// Имя узла в котором сохраняется ходы насоса, по умолчанию 125 об/мин
        /// </summary>
        private const string PumpMovesName = "_pump_moves";

        /// <summary>
        /// Имя узла в котором сохраняется коэффициент пересчета для насоса 1
        /// </summary>
        private const string scale_factor_1_name = "scale_factor_1";

        /// <summary>
        /// Имя узла в котором сохраняется коэффициент пересчета для насоса 2
        /// </summary>
        private const string scale_factor_2_name = "scale_factor_2";

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
                if (slim.TryEnterReadLock(100) && p_slim.TryEnterReadLock(100))
                {
                    try
                    {
                        XmlNode PumpMoviesNode = document.CreateElement(PumpMovesName);

                        XmlNode scale_factor_1_node = document.CreateElement(scale_factor_1_name);
                        XmlNode scale_factor_2_node = document.CreateElement(scale_factor_2_name);
                        
                        PumpMoviesNode.InnerText = _pump_moves.ToString();

                        scale_factor_1_node.InnerText = scale_factor_1.ToString();
                        scale_factor_2_node.InnerText = scale_factor_2.ToString();

                        root.AppendChild(PumpMoviesNode);

                        root.AppendChild(scale_factor_1_node);
                        root.AppendChild(scale_factor_2_node);

                        if (pairs != null)
                        {
                            foreach (IdealFlowPair pair in pairs)
                            {
                                XmlNode ideal_pair = pair.Save(document);
                                if (ideal_pair != null)
                                {
                                    root.AppendChild(ideal_pair);
                                }
                            }
                        }

                        return root;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                        p_slim.ExitReadLock();
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
            if (slim.TryEnterWriteLock(500) && p_slim.TryEnterWriteLock(500))
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
                                    case PumpMovesName:

                                        try
                                        {
                                            _pump_moves = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case scale_factor_1_name:

                                        try
                                        {
                                            scale_factor_1 = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case scale_factor_2_name:

                                        try
                                        {
                                            scale_factor_2 = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case IdealFlowPair.RootName:

                                        try
                                        {
                                            IdealFlowPair pair = new IdealFlowPair();
                                            pair.Load(Child);

                                            pairs.Add(pair);
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
                    p_slim.ExitWriteLock();
                }
            }
        }

        // ----------------------------------------------------------------------------

        /// <summary>
        /// Реализует хранение пары значений Диаметр/Расход
        /// </summary>
        public class IdealFlowPair
        {
            protected float flow;                       // расход жидкости
            protected float diameter;                   // диаметр расходомера

            protected ReaderWriterLockSlim slim;        // синхронизатор

            /// <summary>
            /// инициализирует новый экземпляр класса
            /// </summary>
            public IdealFlowPair()
            {
                flow = float.NaN;
                diameter = float.NaN;

                slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            }

            /// <summary>
            /// Инициализирует новый экземпляр класса
            /// </summary>
            /// <param name="_flow">Расход жидкости</param>
            /// <param name="_diameter">Диаметр разходомера</param>
            public IdealFlowPair(float _flow, float _diameter)
            {
                flow = _flow;
                diameter = _diameter;

                slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            }

            /// <summary>
            /// Определяет расход жидкости
            /// </summary>
            public float Flow
            {
                get
                {
                    if (slim.TryEnterReadLock(100))
                    {
                        try
                        {
                            return flow;
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
                            flow = value;
                        }
                        finally
                        {
                            slim.ExitWriteLock();
                        }
                    }
                }
            }

            /// <summary>
            /// Определяет диаметр расходомера
            /// </summary>
            public float Diameter
            {
                get
                {
                    if (slim.TryEnterReadLock(100))
                    {
                        try
                        {
                            return diameter;
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
                            diameter = value;
                        }
                        finally
                        {
                            slim.ExitWriteLock();
                        }
                    }
                }
            }

            // ------------------------- сохранение -------------------------

            /// <summary>
            /// Корневой узел для пары расход/диаметр
            /// </summary>
            public const string RootName = "IdealFlowPair";

            /// <summary>
            /// Имя узла в котором сохраняется расход
            /// </summary>
            protected const string FlowName = "flow";

            /// <summary>
            /// Имя узла в котором сохраняется диаметр
            /// </summary>
            protected const string DiameterName = "diameter";

            /// <summary>
            /// Сохранить пару расход/диаметр
            /// </summary>
            /// <param name="document">Документ в который осуществляется сохранение</param>
            /// <returns>Xml узел в котором сохранена пара расход/давление</returns>
            public XmlNode Save(XmlDocument document)
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        if (document != null)
                        {
                            XmlNode RootNode = document.CreateElement(RootName);

                            XmlNode FlowNode = document.CreateElement(FlowName);
                            XmlNode DiameterNode = document.CreateElement(DiameterName);

                            FlowNode.InnerText = flow.ToString();
                            DiameterNode.InnerText = diameter.ToString();

                            RootNode.AppendChild(FlowNode);
                            RootNode.AppendChild(DiameterNode);

                            return RootNode;
                        }
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return null;
            }

            /// <summary>
            /// Загрузить пару расход/давление
            /// </summary>
            /// <param name="Node">Узел в котором содержаться данные для пары расход/давление</param>
            public void Load(XmlNode Node)
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        if (Node != null && Node.Name == RootName)
                        {
                            if (Node.HasChildNodes)
                            {
                                foreach (XmlNode Child in Node.ChildNodes)
                                {
                                    switch (Child.Name)
                                    {
                                        case FlowName:

                                            try
                                            {
                                                flow = float.Parse(Child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case DiameterName:

                                            try
                                            {
                                                diameter = float.Parse(Child.InnerText);
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
        }
    }
}