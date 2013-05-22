using System;
using System.Xml;
using System.Threading;
using System.Collections.Generic;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Количество опущенных свеч
    /// </summary>
    public class P0203 : TParameter
    {
        protected float length_instrument;          // предыдущее значение длинны инструмента

        protected float size_candle;                // типовой размер свечи
        
        protected float lower_size_candle;          // нижний размер свечи
        protected float upper_size_candle;          // верхний размер свечи

        // -------------------------------------------------------------------

        protected float deviation;                  // типовое отклонение

        protected List<tubeInfo> tubes;             // информация о трубках
        protected CalculateCandleAlgorithm alg;     // алгоритм вычисления количества опущенных свеч

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0203(Guid p_identifier)
            : base(p_identifier, "P0203", "Количество опущенных свеч")
        {
            simple = false;

            _value = 0;
            size_candle = 19;

            lower_size_candle = 17;
            upper_size_candle = 21;

            tubes = new List<tubeInfo>();
            for (int i = 0; i < 600; i++)
            {
                tubes.Add(new tubeInfo());
            }

            deviation = 1.0f;
            alg = CalculateCandleAlgorithm.LengthOfCandles;
        }

        /// <summary>
        /// Определяет типовой размер свечи
        /// </summary>
        public float SizeCandle
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return size_candle;
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
                        size_candle = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет нижний размер свечи
        /// </summary>
        public float LowerSizeCandle
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return lower_size_candle;
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
                        lower_size_candle = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет верхний размер свечи
        /// </summary>
        public float UpperSizeCandle
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return upper_size_candle;
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
                        upper_size_candle = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет типовое отклонение
        /// </summary>
        public float Deviation
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return deviation;
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
                        deviation = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет алгоритм вычисления количества опущенных свеч
        /// </summary>
        public CalculateCandleAlgorithm Algorithm
        {
            get
            {
                if (slim.TryEnterReadLock(100))
                {
                    try
                    {
                        return alg;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return CalculateCandleAlgorithm.Default;
            }

            set
            {
                if (slim.TryEnterWriteLock(300))
                {
                    try
                    {
                        alg = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет свечи
        /// </summary>
        public List<tubeInfo> Tubes
        {
            get
            {
                return tubes;
            }
        }

        /// <summary>
        /// Вычисляет текущее значение Количество опущенных свеч
        /// </summary>
        /// <param name="v1">Длина инструмента</param>
        /// <param name="currentTime">Текущее технологическое время</param>
        /// <param name="size_layout_bottom_column">Размер компоновки низа колонны</param>
        /// <param name="size_layout_top_column">Размер компоновки верха колонны</param>
        public void Calculate(P0202 v1, DateTime currentTime, float size_layout_bottom_column, float size_layout_top_column)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (float.IsNaN(_value)) _value = 0;
                    switch (v1.ModeProccess)
                    {
                        case P0202.TModeProcess.mpBase:

                            if (alg == CalculateCandleAlgorithm.LenghtOfCandleCount)
                            {
                                calculate_candle(v1, size_layout_bottom_column, size_layout_top_column);
                                _value = calculate_candle_count(v1, size_layout_bottom_column, size_layout_top_column);
                            }
                            else
                                _value = calculate_candle(v1, size_layout_bottom_column, size_layout_top_column);

                            break;

                        case P0202.TModeProcess.mpSetUser:

                            //length_instrument = v1.Value;
                            break;

                        case P0202.TModeProcess.mpCMDzaboi:

                            //_value = calculate_candle(v1, size_layout_bottom_column);
                            break;

                        case P0202.TModeProcess.mpCMDmodifyDepth:

                            //_value = calculate_candle(v1, size_layout_bottom_column);
                            break;

                        default:
                            break;
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Скорректировать значение длинны инструмента
        /// </summary>
        /// <param name="l_value">Значение параметра длинна инструмента</param>
        public void CorrectLenghtInstrument(float l_value)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    length_instrument = l_value;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // ------------ вспомогательные функции ------------

        /// <summary>
        /// Внутренняя процедура расчёта количества свечей, имеет побочный эффект
        /// </summary>
        /// <param name="v1">Длина инструмента</param>
        /// <param name="size_layout_bottom_column">Размер компоновки низа колонны</param>
        /// <param name="size_layout_top_column">Размер компоновки верха колонны</param>
        /// <returns></returns>
        private float calculate_candle(P0202 v1, float size_layout_bottom_column, float size_layout_top_column)
        {
            float dlina = v1.Value;
            float result = _value;

            if (!float.IsNaN(dlina))
            {
                if (dlina <= (size_layout_bottom_column + size_layout_top_column))
                {
                    result = 0;
                    length_instrument = 0;
                }
                else
                {
                    float old = length_instrument;
                    dlina = dlina - (size_layout_bottom_column + size_layout_top_column);

                    float dd = Math.Abs(dlina - old);
                    if (dd > lower_size_candle)
                    {
                        if (old < dlina)
                        {
                            // инструмент вырос
                            while (dd > upper_size_candle)
                            {
                                dd = dd - size_candle;

                                length_instrument = length_instrument + size_candle;
                                result = result + 1;
                            }

                            if (dd >= lower_size_candle)
                            {
                                length_instrument = dlina;
                                result = result + 1;
                            }
                        }
                        else
                        {
                            // инструмент стал меньше
                            while (dd > upper_size_candle)
                            {
                                dd = dd - size_candle;

                                length_instrument = length_instrument - size_candle;
                                result = result - 1;
                            }

                            if (dd >= lower_size_candle)
                            {
                                length_instrument = dlina;
                                result = result - 1;
                            }

                            if (length_instrument <= 0)
                            {
                                result = 0;
                                length_instrument = 0;
                            }

                            if (result < 0)
                            {
                                result = 0;                                
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Внутренняя процедура расчёта количества свечей, побочных эффектов не имеет =)
        /// </summary>
        /// <param name="size_layout_bottom_column">Размер компоновки низа колонны</param>
        /// <param name="size_layout_top_column">Размер компоновки верха колонны</param>
        /// <returns></returns>
        protected float calculate_candle_count(P0202 v1, float size_layout_bottom_column, float size_layout_top_column)
        {
            if (calculateTotal())
            {
                try
                {
                    if (tubes != null)
                    {
                        if (!float.IsNaN(v1.Value) && !float.IsInfinity(v1.Value) &&
                            !float.IsNegativeInfinity(v1.Value) && !float.IsPositiveInfinity(v1.Value))
                        {
                            float dlina = v1.Value - (size_layout_bottom_column + size_layout_top_column);
                            if (dlina < 0) return 0.0f;

                            for (int index = 0; index < tubes.Count; index++)
                            {
                                if (tubes[index] != null && tubes[index].Lenght > 0)
                                {
                                    float a = Math.Abs(dlina - tubes[index].Total);
                                    if (a <= deviation)
                                    {
                                        return tubes[index].Number;
                                    }
                                    else
                                        if (dlina < tubes[index].Total)
                                        {
                                            if (index <= 0)
                                            {
                                                return 0.0f;
                                            }
                                            else
                                            {
                                                float d1 = dlina - tubes[index - 1].Total;
                                                float d2 = tubes[index].Total - dlina;

                                                if (d1 < d2)
                                                {
                                                    v1.setValue(tubes[index - 1].Total + (size_layout_bottom_column + size_layout_top_column));
                                                    return tubes[index - 1].Number;
                                                }
                                                else
                                                {
                                                    v1.setValue(tubes[index].Total + (size_layout_bottom_column + size_layout_top_column));
                                                    return tubes[index].Number;
                                                }
                                            }
                                        }
                                }
                                else
                                    break;
                            }

                            int lastIndex = -1;
                            for (int index = 0; index < tubes.Count; index++)
                            {
                                if (tubes[index].Lenght <= 0)
                                {
                                    lastIndex = index - 1;
                                    break;
                                }
                            }

                            if (lastIndex > -1 && lastIndex < tubes.Count)
                            {
                                return tubes[lastIndex].Number + 1;
                            }
                            else
                                return 0.0f;

                            //return (tubes[tubes.Count - 1].Number + 1);
                        }
                    }
                }
                catch { }
            }
            return float.NaN;
        }

        /// <summary>
        /// Вычислить нарастающий итог для свечей
        /// </summary>
        protected bool calculateTotal()
        {
            try
            {
                float __total = 0.0f;
                for (int index = 0; index < tubes.Count; index++)
                {
                    if (index == 0)
                    {
                        __total = tubes[index].Lenght;
                        tubes[index].Total = tubes[index].Lenght;
                    }
                    else
                    {
                        if (tubes[index].Lenght > 0)
                        {
                            __total += tubes[index].Lenght;
                            tubes[index].Total = __total;
                        }
                        else
                            tubes[index].Total = 0.0f;
                    }
                }

                return true;
            }
            catch { }
            return false;
        }

        // --------------------------- сохранение параметра ---------------------------

        /// <summary>
        /// Имя узла в котором сохраняется значение длинны инструмента
        /// </summary>
        protected const string LengthInstrumentName = "length_instrument";

        /// <summary>
        /// Имя узла в котором сохраняется значение типовой размер свечи
        /// </summary>
        protected const string SizeCandleName = "size_candle";

        /// <summary>
        /// Имя узла в котором сохраняется значение нижний размер свечи
        /// </summary>
        protected const string LowerSizeCandleName = "lower_size_candle";
        
        /// <summary>
        /// Имя узла в котором сохраняется значение верхний размер свечи
        /// </summary>
        protected const string UpperSizeCandleName = "upper_size_candle";

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
                        XmlNode LengthInstrumentNode = document.CreateElement(LengthInstrumentName);
                        XmlNode SizeCandleNode = document.CreateElement(SizeCandleName);

                        XmlNode LowerSizeCandleNode = document.CreateElement(LowerSizeCandleName);
                        XmlNode UpperSizeCandleNode = document.CreateElement(UpperSizeCandleName);

                        XmlNode algNode = document.CreateElement("alg");
                        XmlNode deviationNode = document.CreateElement("deviation");

                        deviationNode.InnerText = deviation.ToString();
                        LengthInstrumentNode.InnerText = length_instrument.ToString();
                        
                        SizeCandleNode.InnerText = size_candle.ToString();

                        LowerSizeCandleNode.InnerText = lower_size_candle.ToString();
                        UpperSizeCandleNode.InnerText = upper_size_candle.ToString();

                        algNode.InnerText = alg.ToString();

                        XmlNode tubesRoot = document.CreateElement("candles");
                        foreach (tubeInfo tube in tubes)
                        {
                            if (tube != null)
                            {
                                XmlNode node = tube.Save(document);
                                if (node != null)
                                {
                                    tubesRoot.AppendChild(node);
                                }
                            }
                        }

                        root.AppendChild(LengthInstrumentNode);
                        root.AppendChild(deviationNode);

                        root.AppendChild(SizeCandleNode);                        
                        root.AppendChild(LowerSizeCandleNode);

                        root.AppendChild(UpperSizeCandleNode);
                        root.AppendChild(algNode);
                        
                        root.AppendChild(tubesRoot);
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
                                    case LengthInstrumentName:

                                        try
                                        {
                                            length_instrument = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case SizeCandleName:

                                        try
                                        {
                                            size_candle = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case LowerSizeCandleName:

                                        try
                                        {
                                            lower_size_candle = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case UpperSizeCandleName:

                                        try
                                        {
                                            upper_size_candle = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "deviation":

                                        try
                                        {
                                            deviation = float.Parse(Child.InnerText);
                                        }
                                        catch { }
                                        break;

                                    case "candles":

                                        try
                                        {
                                            tubes.Clear();
                                            LoadCandles(Child);
                                        }
                                        catch { }
                                        break;

                                    case "alg":

                                        try
                                        {
                                            alg = (CalculateCandleAlgorithm)Enum.Parse(typeof(CalculateCandleAlgorithm), Child.InnerText);
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
        /// Загрузить информацию о свечах
        /// </summary>
        /// <param name="root"></param>
        protected void LoadCandles(XmlNode root)
        {
            if (root != null && root.Name == "candles")
            {
                if (root.HasChildNodes)
                {
                    foreach (XmlNode child in root.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case tubeInfo.rootName:

                                try
                                {
                                    tubeInfo info = new tubeInfo();
                                    info.Load(child);

                                    tubes.Add(info);
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

        // ----------------------------------------------------------------------------
    }

    /// <summary>
    /// Варианты вычисления колечаства свечей
    /// </summary>
    public enum CalculateCandleAlgorithm
    {
        /// <summary>
        /// Вычислять по длинне свечи и количеству трубок
        /// </summary>
        LenghtOfCandleCount,

        /// <summary>
        /// Вычислять по длинне свечей
        /// </summary>
        LengthOfCandles,

        /// <summary>
        /// Алгоритм вычисления не определен
        /// </summary>
        Default
    }

    /// <summary>
    /// Реализует хранение данных о трубке
    /// </summary>
    public class tubeInfo
    {
        protected float lenhgt;                 // длинна трубки
        protected int number;                   // номер свечи

        protected string comment;               // комментарий
        protected float total;                  // нарастающий итог

        protected ReaderWriterLockSlim sync;    // синхронизатор

        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        public tubeInfo()
        {
            lenhgt = 0.0f;
            number = 0;

            comment = string.Empty;
            total = 0.0f;

            sync = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Определяет длинну трубки
        /// </summary>
        public float Lenght
        {
            get
            {
                if (sync.TryEnterReadLock(100))
                {
                    try
                    {
                        return lenhgt;
                    }
                    finally
                    {
                        sync.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (sync.TryEnterWriteLock(300))
                {
                    try
                    {
                        lenhgt = value;
                    }
                    finally
                    {
                        sync.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет номер свечи
        /// </summary>
        public int Number
        {
            get
            {
                if (sync.TryEnterReadLock(100))
                {
                    try
                    {
                        return number;
                    }
                    finally
                    {
                        sync.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (sync.TryEnterWriteLock(300))
                {
                    try
                    {
                        number = value;
                    }
                    finally
                    {
                        sync.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет комментарий
        /// </summary>
        public string Comment
        {
            get
            {
                if (sync.TryEnterReadLock(100))
                {
                    try
                    {
                        return comment;
                    }
                    finally
                    {
                        sync.ExitReadLock();
                    }
                }

                return string.Empty;
            }

            set
            {
                if (sync.TryEnterWriteLock(300))
                {
                    try
                    {
                        comment = value;
                    }
                    finally
                    {
                        sync.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Опреедляет нарастающий итог
        /// </summary>
        public float Total
        {
            get
            {
                if (sync.TryEnterReadLock(100))
                {
                    try
                    {
                        return total;
                    }
                    finally
                    {
                        sync.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (sync.TryEnterWriteLock(300))
                {
                    try
                    {
                        total = value;
                    }
                    finally
                    {
                        sync.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// имя узла в которой сохраняется объек
        /// </summary>
        public const string rootName = "tubeInfo";

        /// <summary>
        /// Сохранить объект
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранение объекта</param>
        /// <returns>Сохранений объект</returns>
        public XmlNode Save(XmlDocument doc)
        {
            try
            {
                if (doc != null)
                {
                    if (sync.TryEnterReadLock(100))
                    {
                        try
                        {
                            XmlNode root = doc.CreateElement(rootName);

                            XmlNode lenhgtNode = doc.CreateElement("lenhgt");
                            XmlNode numberNode = doc.CreateElement("number");

                            XmlNode commentNode = doc.CreateElement("comment");
                            XmlNode totalNode = doc.CreateElement("total");

                            lenhgtNode.InnerText = lenhgt.ToString();
                            numberNode.InnerText = number.ToString();

                            commentNode.InnerText = comment;
                            totalNode.InnerText = total.ToString();

                            root.AppendChild(lenhgtNode);
                            root.AppendChild(numberNode);

                            root.AppendChild(commentNode);
                            root.AppendChild(totalNode);

                            return root;
                        }
                        finally
                        {
                            sync.ExitReadLock();
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Загрузить объект
        /// </summary>
        /// <param name="root">узел в котором содержится объект</param>
        public void Load(XmlNode root)
        {
            if (sync.TryEnterWriteLock(300))
            {
                try
                {
                    if (root != null)
                    {
                        if (root.Name == rootName)
                        {
                            if (root.HasChildNodes)
                            {
                                foreach (XmlNode child in root.ChildNodes)
                                {
                                    switch (child.Name)
                                    {
                                        case "lenhgt":

                                            try
                                            {
                                                lenhgt = float.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case "number":

                                            try
                                            {
                                                number = int.Parse(child.InnerText);
                                            }
                                            catch { }
                                            break;

                                        case "comment":

                                            try
                                            {
                                                comment = child.InnerText;
                                            }
                                            catch { }
                                            break;

                                        case "total":

                                            try
                                            {
                                                total = float.Parse(child.InnerText);
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
                    sync.ExitWriteLock();
                }
            }
        }
    }
}