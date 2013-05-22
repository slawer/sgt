using System;
using System.Xml;
using System.Threading;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Объем бурового раствора в емкостях (суммарный)
    /// </summary>
    public class P0104 : TParameter
    {
        protected Boolean tank_1;       // емкость 1
        protected Boolean tank_2;       // емкость 2

        protected Boolean tank_3;       // емкость 3
        protected Boolean tank_4;       // емкость 4

        protected Boolean tank_5;       // емкость 5
        protected Boolean tank_6;       // емкость 6

        protected Boolean tank_7;       // емкость 7
        protected Boolean tank_8;       // емкость 8

        protected Boolean tank_9;       // емкость 9
        protected Boolean tank_10;      // емкость 10

        protected Boolean tank_11;      // емкость 11
        protected Boolean tank_12;      // емкость 12

        protected Boolean tank_13;      // емкость 13
        protected Boolean tank_14;      // емкость 14

        protected ReaderWriterLockSlim s_tank;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0104(Guid p_identifier)
            : base(p_identifier, "P0104", "Объем бурового раствора в емкостях (суммарный)")
        {
            simple = false;

            tank_1 = true;
            tank_2 = true;
            tank_3 = true;
            tank_4 = true;
            tank_5 = true;
            tank_6 = true;
            tank_7 = true;

            tank_8 = false;
            tank_9 = false;
            tank_10 = false;
            tank_11 = false;
            tank_12 = false;
            tank_13 = false;
            tank_14 = false;

            s_tank = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Определяет включать первую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_1
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_1;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_1 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать вторую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_2
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_2;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_2 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать третью емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_3
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_3;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_3 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать четвертую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_4
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_4;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_4 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать пятую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_5
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_5;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_5 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать шестую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_6
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_6;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_6 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать седьмую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_7
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_7;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_7 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать восьмую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_8
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_8;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_8 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать девятую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_9
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_9;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_9 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать десятую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_10
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_10;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_10 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать одиннадцатую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_11
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_11;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_11 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать двенадцатую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_12
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_12;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_12 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать тринадцатую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_13
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_13;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_13 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет включать четырнадцатую емкость в общий объем или нет
        /// </summary>
        public Boolean Tank_14
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {
                        return tank_14;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_tank.TryEnterWriteLock(300))
                {
                    try
                    {
                        tank_14 = value;
                    }
                    finally
                    {
                        s_tank.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет массив емкостей
        /// </summary>
        protected Boolean[] Tanks
        {
            get
            {
                if (s_tank.TryEnterReadLock(100))
                {
                    try
                    {

                        Boolean[] tanks = { tank_1, tank_2, tank_3, tank_4, tank_5, tank_6, tank_7,
                                    tank_8, tank_9, tank_10, tank_11, tank_12, tank_13, tank_14, };

                        return tanks;
                    }
                    finally
                    {
                        s_tank.ExitReadLock();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Объем бурового раствора в емкостях (суммарный)
        /// </summary>
        /// <param name="vX">Объем бурового раствора в емкости Х</param>
        public void Calculate(P0009 v1, P09_1 v2, P09_2 v3, P09_3 v4, P09_4 v5, P09_5 v6, P09_6 v7,
                                      P09_7 v8, P09_8 v9, P09_9 v10, P9_10 v11, P9_11 v12, P9_12 v13, P9_13 v14)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    Boolean[] tanks = Tanks;
                    if (tanks != null)
                    {
                        float v = 0;

                        float[] vals = { v1.Value, v2.Value, v3.Value, v4.Value, v5.Value, v6.Value, v7.Value, 
                                           v8.Value, v9.Value, v10.Value, v11.Value, v12.Value, v13.Value, v14.Value };
                        if (vals != null)
                        {
                            if (vals.Length == tanks.Length)
                            {
                                for (int index = 0; index < tanks.Length; index++)
                                {
                                    if (tanks[index])
                                    {
                                        if (!float.IsNaN(vals[index]))
                                        {
                                            v += vals[index];
                                        }
                                        else
                                        {
                                            //v = vals[index];
                                            //return;
                                        }
                                    }
                                }
                            }

                            _value = v;
                        }
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // --------------------------- сохранение параметра ---------------------------

        /// <summary>
        /// Базовое имя узла в котором будут сохранены емкости
        /// </summary>
        protected const string BaseTankName = "tank_";

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
                        Boolean[] tanks = Tanks;
                        if (tanks != null)
                        {
                            int tank_number = 1;
                            foreach (Boolean tank in tanks)
                            {
                                XmlNode TankNode = document.CreateElement(string.Format("{0}{1}", BaseTankName, tank_number++));

                                TankNode.InnerText = tank.ToString();
                                root.AppendChild(TankNode);
                            }
                        }

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
                            Boolean[] tanks = Tanks;
                            String[] tank_names = null;

                            if (tanks != null)
                            {
                                tank_names = new string[tanks.Length];
                                for (int index = 0; index < tank_names.Length; index++)
                                {
                                    tank_names[index] = string.Format("{0}{1}", BaseTankName, index + 1);
                                }

                                foreach (XmlNode Child in Node.ChildNodes)
                                {
                                    int tank_number = GetTankNumberFromName(Child.Name, tank_names);
                                    switch (tank_number)
                                    {
                                        case 1: try { Tank_1 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 2: try { Tank_2 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 3: try { Tank_3 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 4: try { Tank_4 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 5: try { Tank_5 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 6: try { Tank_6 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 7: try { Tank_7 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 8: try { Tank_8 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 9: try { Tank_9 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 10: try { Tank_10 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 11: try { Tank_11 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 12: try { Tank_12 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 13: try { Tank_13 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 14: try { Tank_14 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        default: break;
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

        /// <summary>
        /// Получить номер емкости
        /// </summary>
        /// <param name="TankName">Ммя емкости</param>
        /// <param name="TankNames">Список имен емкостей</param>
        /// <returns>Номер емкости</returns>
        protected int GetTankNumberFromName(string TankName, string[] TankNames)
        {
            if (TankNames != null)
            {
                for (int index = 0; index < TankNames.Length; index++)
                {
                    if (TankName == TankNames[index])
                    {
                        return (index + 1);
                    }
                }
            }

            return -1;
        }

        // ----------------------------------------------------------------------------
    }
}