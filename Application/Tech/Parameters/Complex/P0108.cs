using System;
using System.Xml;
using System.Threading;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Сигнал тревоги Газы 50%
    /// </summary>
    public class P0108 : TParameter
    {
        protected Boolean gas_sensor_1;         // датчик газа 1
        protected Boolean gas_sensor_2;         // датчик газа 2

        protected Boolean gas_sensor_3;         // датчик газа 3
        protected Boolean gas_sensor_4;         // датчик газа 4

        protected Boolean gas_sensor_5;         // датчик газа 5
        protected Boolean gas_sensor_6;         // датчик газа 6

        protected Boolean gas_sensor_7;         // датчик газа 7
        protected Boolean gas_sensor_8;         // датчик газа 8

        protected Boolean gas_sensor_9;         // датчик газа 9
        protected Boolean gas_sensor_10;        // датчик газа 10

        protected ReaderWriterLockSlim s_gas;   // синхронизатор датчиков газа

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0108(Guid p_identifier)
            : base(p_identifier, "P0108", "Сигнал тревоги Газы 50%")
        {
            simple = false;

            gas_sensor_1 = true;
            gas_sensor_2 = true;

            gas_sensor_3 = true;
            gas_sensor_4 = true;

            gas_sensor_5 = true;
            gas_sensor_6 = false;

            gas_sensor_7 = false;
            gas_sensor_8 = false;

            gas_sensor_9 = false;
            gas_sensor_10 = false;

            s_gas = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _value = 0;
        }

        /// <summary>
        /// Определяет вхождение датчика газа 1 в систему слежения за уровнем
        /// </summary>
        public Boolean Gas_Sensor_1
        {
            get
            {
                if (s_gas.TryEnterReadLock(100))
                {
                    try
                    {
                        return gas_sensor_1;
                    }
                    finally
                    {
                        s_gas.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_gas.TryEnterWriteLock(300))
                {
                    try
                    {
                        gas_sensor_1 = value;
                    }
                    finally
                    {
                        s_gas.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет вхождение датчика газа 2 в систему слежения за уровнем
        /// </summary>
        public Boolean Gas_Sensor_2
        {
            get
            {
                if (s_gas.TryEnterReadLock(100))
                {
                    try
                    {
                        return gas_sensor_2;
                    }
                    finally
                    {
                        s_gas.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_gas.TryEnterWriteLock(300))
                {
                    try
                    {
                        gas_sensor_2 = value;
                    }
                    finally
                    {
                        s_gas.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет вхождение датчика газа 3 в систему слежения за уровнем
        /// </summary>
        public Boolean Gas_Sensor_3
        {
            get
            {
                if (s_gas.TryEnterReadLock(100))
                {
                    try
                    {
                        return gas_sensor_3;
                    }
                    finally
                    {
                        s_gas.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_gas.TryEnterWriteLock(300))
                {
                    try
                    {
                        gas_sensor_3 = value;
                    }
                    finally
                    {
                        s_gas.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет вхождение датчика газа 4 в систему слежения за уровнем
        /// </summary>
        public Boolean Gas_Sensor_4
        {
            get
            {
                if (s_gas.TryEnterReadLock(100))
                {
                    try
                    {
                        return gas_sensor_4;
                    }
                    finally
                    {
                        s_gas.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_gas.TryEnterWriteLock(300))
                {
                    try
                    {
                        gas_sensor_4 = value;
                    }
                    finally
                    {
                        s_gas.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет вхождение датчика газа 5 в систему слежения за уровнем
        /// </summary>
        public Boolean Gas_Sensor_5
        {
            get
            {
                if (s_gas.TryEnterReadLock(100))
                {
                    try
                    {
                        return gas_sensor_5;
                    }
                    finally
                    {
                        s_gas.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_gas.TryEnterWriteLock(300))
                {
                    try
                    {
                        gas_sensor_5 = value;
                    }
                    finally
                    {
                        s_gas.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет вхождение датчика газа 6 в систему слежения за уровнем
        /// </summary>
        public Boolean Gas_Sensor_6
        {
            get
            {
                if (s_gas.TryEnterReadLock(100))
                {
                    try
                    {
                        return gas_sensor_6;
                    }
                    finally
                    {
                        s_gas.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_gas.TryEnterWriteLock(300))
                {
                    try
                    {
                        gas_sensor_6 = value;
                    }
                    finally
                    {
                        s_gas.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет вхождение датчика газа 7 в систему слежения за уровнем
        /// </summary>
        public Boolean Gas_Sensor_7
        {
            get
            {
                if (s_gas.TryEnterReadLock(100))
                {
                    try
                    {
                        return gas_sensor_7;
                    }
                    finally
                    {
                        s_gas.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_gas.TryEnterWriteLock(300))
                {
                    try
                    {
                        gas_sensor_7 = value;
                    }
                    finally
                    {
                        s_gas.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет вхождение датчика газа 8 в систему слежения за уровнем
        /// </summary>
        public Boolean Gas_Sensor_8
        {
            get
            {
                if (s_gas.TryEnterReadLock(100))
                {
                    try
                    {
                        return gas_sensor_8;
                    }
                    finally
                    {
                        s_gas.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_gas.TryEnterWriteLock(300))
                {
                    try
                    {
                        gas_sensor_8 = value;
                    }
                    finally
                    {
                        s_gas.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет вхождение датчика газа 9 в систему слежения за уровнем
        /// </summary>
        public Boolean Gas_Sensor_9
        {
            get
            {
                if (s_gas.TryEnterReadLock(100))
                {
                    try
                    {
                        return gas_sensor_9;
                    }
                    finally
                    {
                        s_gas.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_gas.TryEnterWriteLock(300))
                {
                    try
                    {
                        gas_sensor_9 = value;
                    }
                    finally
                    {
                        s_gas.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Определяет вхождение датчика газа 9 в систему слежения за уровнем
        /// </summary>
        public Boolean Gas_Sensor_10
        {
            get
            {
                if (s_gas.TryEnterReadLock(100))
                {
                    try
                    {
                        return gas_sensor_10;
                    }
                    finally
                    {
                        s_gas.ExitReadLock();
                    }
                }

                return false;
            }

            set
            {
                if (s_gas.TryEnterWriteLock(300))
                {
                    try
                    {
                        gas_sensor_10 = value;
                    }
                    finally
                    {
                        s_gas.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращяет список датчиков газа
        /// </summary>
        protected Boolean[] Gases
        {
            get
            {
                if (s_gas.TryEnterReadLock(100))
                {
                    try
                    {
                        Boolean[] gases = { gas_sensor_1, gas_sensor_2, gas_sensor_3, gas_sensor_4, gas_sensor_5, 
                                              gas_sensor_6, gas_sensor_7, gas_sensor_8, gas_sensor_9, gas_sensor_10, };

                        return gases;
                    }
                    finally
                    {
                        s_gas.ExitReadLock();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Сигнал тревоги Газы 20%
        /// </summary>
        /// <param name="v1">Сигнал датчика Газы 1</param>
        /// <param name="v2">Сигнал датчика Газы 2</param>
        /// <param name="v3">Сигнал датчика Газы 3</param>
        /// <param name="v4">Сигнал датчика Газы 4</param>
        /// <param name="v5">Сигнал датчика Газы 5</param>
        /// <param name="v6">Сигнал датчика Газы 6</param>
        /// <param name="v7">Сигнал датчика Газы 7</param>
        /// <param name="v8">Сигнал датчика Газы 8</param>
        /// <param name="v9">Сигнал датчика Газы 9</param>
        /// <param name="v10">Сигнал датчика Газы 10</param>
        public void Calculate(P0006 v1, P06_1 v2, P06_2 v3, P06_3 v4, P06_4 v5, P06_5 v6,
                                P06_6 v7, P06_7 v8, P06_8 v9, P06_9 v10)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    _value = 0;
                    Boolean[] gases = Gases;                    

                    float[] vals = { v1.Value, v2.Value, v3.Value, v4.Value, v5.Value, 
                                       v6.Value, v7.Value, v8.Value, v9.Value, v10.Value, };

                    float[] uppers = { v1.Upper, v2.Upper, v3.Upper, v4.Upper, v5.Upper, 
                                         v6.Upper, v7.Upper, v8.Upper, v9.Upper, v10.Upper, };

                    if (gases != null && vals != null && uppers != null)
                    {
                        if (gases.Length == vals.Length && gases.Length == uppers.Length)
                        {
                            for (int index = 0; index < gases.Length; index++)
                            {
                                if (gases[index])
                                {
                                    if (!float.IsNaN(vals[index]))
                                    {
                                        if (vals[index] > uppers[index])
                                        {
                                            _value = 1;
                                            break;
                                        }
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

        // --------------------------- сохранение параметра ---------------------------

        /// <summary>
        /// Имя узла в котором сохраняется период усреднения 
        /// </summary>
        protected const string BaseGasName = "gas_";

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
                        Boolean[] gases = Gases;
                        if (gases != null)
                        {
                            int gas_number = 1;
                            foreach (Boolean gas in gases)
                            {
                                XmlNode GasNode = document.CreateElement(string.Format("{0}{1}", BaseGasName, gas_number++));

                                GasNode.InnerText = gas.ToString();
                                root.AppendChild(GasNode);
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
                            Boolean[] gases = Gases;

                            if (gases != null)
                            {
                                string[] gas_names = new string[gases.Length];
                                for (int index = 0; index < gas_names.Length; index++)
                                {
                                    gas_names[index] = string.Format("{0}{1}", BaseGasName, index + 1);
                                }

                                foreach (XmlNode Child in Node.ChildNodes)
                                {
                                    int gas_number = GetGasNumberFromName(Child.InnerText, gas_names);
                                    switch (gas_number)
                                    {
                                        case 1: try { Gas_Sensor_1 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 2: try { Gas_Sensor_2 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 3: try { Gas_Sensor_3 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 4: try { Gas_Sensor_4 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 5: try { Gas_Sensor_5 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 6: try { Gas_Sensor_6 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 7: try { Gas_Sensor_7 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 8: try { Gas_Sensor_8 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 9: try { Gas_Sensor_9 = bool.Parse(Child.InnerText); }
                                            catch { } break;

                                        case 10: try { Gas_Sensor_10 = bool.Parse(Child.InnerText); }
                                            catch { } break;
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
        protected int GetGasNumberFromName(string GasName, string[] GasNames)
        {
            if (GasNames != null)
            {
                for (int index = 0; index < GasNames.Length; index++)
                {
                    if (GasName == GasNames[index])
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