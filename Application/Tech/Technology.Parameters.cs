using System;
using System.Threading;

namespace SGT
{
    /// <summary>
    /// Реализует технологию СГТ
    /// </summary>
    public partial class Technology
    {
        protected P0001 p1;         // Вес на крюке Датчик
        protected P0002 p2;         // Крутящий момент ротора Аналоговый

        protected P0003 p3;         // Поток на выходе Датчик
        protected P0004 p4;         // Давление Датчик

        protected P0005 p5;         // Высота талевого блока Датчик

        protected P0006 p6;         // Газ Д1 (CH4) Датчик
        protected P06_1 p6_1;       // Газ Д2 (CH4) Датчик
        protected P06_2 p6_2;       // Газ Д3 (CH4) Датчик
        protected P06_3 p6_3;       // Газ Д4 (CH4) Датчик
        protected P06_4 p6_4;       // Газ Д5 (H2S) Датчик
        protected P06_5 p6_5;       // Газ Д6 (H2S) Датчик
        protected P06_6 p6_6;       // Газ Д7 (H2S) Датчик
        protected P06_7 p6_7;       // Газ Д8 (H2S) Датчик
        protected P06_8 p6_8;       // Газ Д9 (CH4) Датчик
        protected P06_9 p6_9;       // Газ Д10 (H2S) Датчик

        protected P0007 p7;         // Уровень ДУ1 Датчик
        protected P07_1 p7_1;       // Уровень ДУ2 Датчик
        protected P07_2 p7_2;       // Уровень ДУ3 Датчик
        protected P07_3 p7_3;       // Уровень ДУ4 Датчик
        protected P07_4 p7_4;       // Уровень ДУ5 Датчик
        protected P07_5 p7_5;       // Уровень ДУ6 Датчик
        protected P07_6 p7_6;       // Уровень ДУ7 Датчик
        protected P07_7 p7_7;       // Уровень ДУ8 Датчик
        protected P07_8 p7_8;       // Уровень ДУ9 Датчик
        protected P07_9 p7_9;       // Уровень ДУ10 Датчик
        protected P7_10 p7_10;      // Уровень ДУ11 Датчик
        protected P7_11 p7_11;      // Уровень ДУ12 Датчик
        protected P7_12 p7_12;      // Уровень ДУ13 Датчик
        protected P7_13 p7_13;      // Уровень ДУ14 Датчик

        protected P0008 p8;         // Ходы насоса 1 Аналоговый
        protected P08_1 p8_1;       // Ходы насоса 2 Аналоговый

        protected P0009 p9;         // Объём ДУ1 Датчик
        protected P09_1 p9_1;       // Объём ДУ2 Датчик
        protected P09_2 p9_2;       // Объём ДУ3 Датчик
        protected P09_3 p9_3;       // Объём ДУ4 Датчик
        protected P09_4 p9_4;       // Объём ДУ5 Датчик
        protected P09_5 p9_5;       // Объём ДУ6 Датчик
        protected P09_6 p9_6;       // Объём ДУ7 Датчик
        protected P09_7 p9_7;       // Объём ДУ8 Датчик
        protected P09_8 p9_8;       // Объём ДУ9 Датчик
        protected P09_9 p9_9;       // Объём ДУ10 Датчик
        protected P9_10 p9_10;      // Объём ДУ11 Датчик
        protected P9_11 p9_11;      // Объём ДУ12 Датчик
        protected P9_12 p9_12;      // Объём ДУ13 Датчик
        protected P9_13 p9_13;      // Объём ДУ14 Датчик

        protected P0010 p10;        // Расход на входе Датчик

        protected P0011 p11;        // Ходы насоса 1 АСУ
        protected P11_1 p11_1;      // Ходы насоса 2 АСУ

        protected P0012 p12;        // Клинья АСУ
        protected P0013 p13;        // Вес на крюке Аналоговый

        protected P0014 p14;        // Диаметр поршня 1 АСУ
        protected P14_1 p14_1;      // Диаметр поршня 1 АСУ

        protected P0015 p15;        // Обороты ротора Аналоговый
        protected P0016 p16;        // Крутящий момент ротора АСУ

        protected P0017 p17;        // Обороты ротора АСУ
        protected P0018 p18;        // Обороты СВП Аналоговый

        protected P0101 p101;       // Крутящий момент ротора
        protected P0102 p102;       // Вес на крюке

        protected P0103 p103;       // Скорость тальблока
        protected P0104 p104;       // Объем бурового раствора в емкостях (суммарный)

        protected P0105 p105;       // Изменение расхода на выходе
        protected P0106 p106;       // Изменение суммарного объема

        protected P0107 p107;       // Сигнал тревоги Газы 20%
        protected P0108 p108;       // Сигнал тревоги Газы 50%

        protected P0109 p109;       // Расход на входе по Ходам Насоса с Аналогового сигнала
        protected P0110 p110;       // Обороты ротора

        protected P0112 p112;       // Расход на входе Ходам Насоса АСУ
        protected P0113 p113;       // Расход на входе по Ходам Насоса

        protected P0114 p114;       // Расход на входе
        protected P0116 p116;       // Ходы Насоса 1

        protected P0117 p117;       // Ходы Насоса 2
        protected P0118 p118;       // Сумма ходов насоса

        protected P0200 p200;       // Вес колонны
        protected P0201 p201;       // Нагрузка на долото

        protected P0202 p202;       // Длина инструмента
        protected P0203 p203;       // Количество опущенных свеч

        protected P0204 p204;       // Положение инструмента
        protected P0205 p205;       // Глубина забоя

        protected P0206 p206;       // Состояние процесса бурения
        protected P0207 p207;       // Подача

        protected P0208 p208;       // Мех. скорость проходки в единицах м/час
        protected P0209 p209;       // Время бурения 1м проходки

        protected P0210 p210;       // Скорость СПО
        protected P0211 p211;       // Над забоем в метрах

        protected P0212 p212;       // время циркуляции
        protected P0213 p213;       // Время бурения
        
        // -------------------------

        /// <summary>
        /// Вес на крюке Датчик
        /// </summary>
        public P0001 P0001
        {
            get { return p1; }
        }
        
        /// <summary>
        /// Крутящий момент ротора Аналоговый
        /// </summary>
        public P0002 P0002
        {
            get { return p2; }
        }

        /// <summary>
        /// Поток на выходе Датчик
        /// </summary>
        public P0003 P0003
        {
            get { return p3; }
        }
        
        /// <summary>
        /// Давление Датчик
        /// </summary>
        public P0004 P0004
        {
            get { return p4; }
        }

        /// <summary>
        /// Высота талевого блока Датчик
        /// </summary>
        public P0005 P0005
        {
            get { return p5; }
        }

        /// <summary>
        /// Газ Д1 (CH4) Датчик
        /// </summary>
        public P0006 P0006
        {
            get { return p6; }
        }
        
        /// <summary>
        /// Газ Д2 (CH4) Датчик
        /// </summary>
        public P06_1 P06_1
        {
            get { return p6_1; }
        }
        
        /// <summary>
        /// Газ Д3 (CH4) Датчик
        /// </summary>
        public P06_2 P06_2
        {
            get { return p6_2; }
        }
        
        /// <summary>
        /// Газ Д4 (CH4) Датчик
        /// </summary>
        public P06_3 P06_3
        {
            get { return p6_3; }
        }
        
        /// <summary>
        /// Газ Д5 (H2S) Датчик
        /// </summary>
        public P06_4 P06_4
        {
            get { return p6_4; }
        }
        
        /// <summary>
        /// Газ Д6 (H2S) Датчик
        /// </summary>
        public P06_5 P06_5
        {
            get { return p6_5; }
        }
        
        /// <summary>
        /// Газ Д7 (H2S) Датчик
        /// </summary>
        public P06_6 P06_6
        {
            get { return p6_6; }
        }
        
        /// <summary>
        /// Газ Д8 (H2S) Датчик
        /// </summary>
        public P06_7 P06_7
        {
            get { return p6_7; }
        }
        
        /// <summary>
        /// Газ Д9 (CH4) Датчик
        /// </summary>
        public P06_8 P06_8
        {
            get { return p6_8; }
        }

        /// <summary>
        /// Газ Д10 (H2S) Датчик
        /// </summary>        
        public P06_9 P06_9
        {
            get { return p6_9; }
        }

        /// <summary>
        /// Уровень ДУ1 Датчик
        /// </summary>        
        public P0007 P0007
        {
            get { return p7; }
        }

        /// <summary>
        /// Уровень ДУ2 Датчик
        /// </summary>        
        public P07_1 P07_1
        {
            get { return p7_1; }
        }

        /// <summary>
        /// Уровень ДУ3 Датчик
        /// </summary>        
        public P07_2 P07_2
        {
            get { return p7_2; }
        }

        /// <summary>
        /// Уровень ДУ4 Датчик
        /// </summary>        
        public P07_3 P07_3
        {
            get { return p7_3; }
        }

        /// <summary>
        /// Уровень ДУ5 Датчик
        /// </summary>        
        public P07_4 P07_4
        {
            get { return p7_4; }
        }

        /// <summary>
        /// Уровень ДУ6 Датчик
        /// </summary>        
        public P07_5 P07_5
        {
            get { return p7_5; }
        }

        /// <summary>
        /// Уровень ДУ7 Датчик
        /// </summary>        
        public P07_6 P07_6
        {
            get { return p7_6; }
        }

        /// <summary>
        /// Уровень ДУ8 Датчик
        /// </summary>        
        public P07_7 P07_7
        {
            get { return p7_7; }
        }

        /// <summary>
        /// Уровень ДУ9 Датчик
        /// </summary>        
        public P07_8 P07_8
        {
            get { return p7_8; }
        }

        /// <summary>
        /// Уровень ДУ10 Датчик
        /// </summary>        
        public P07_9 P07_9
        {
            get { return p7_9; }
        }

        /// <summary>
        /// Уровень ДУ11 Датчик
        /// </summary>        
        public P7_10 P7_10
        {
            get { return p7_10; }
        }

        /// <summary>
        /// Уровень ДУ12 Датчик
        /// </summary>        
        public P7_11 P7_11
        {
            get { return p7_11; }
        }

        /// <summary>
        /// Уровень ДУ13 Датчик
        /// </summary>        
        public P7_12 P7_12
        {
            get { return p7_12; }
        }

        /// <summary>
        /// Уровень ДУ14 Датчик
        /// </summary>        
        public P7_13 P7_13
        {
            get { return p7_13; }
        }

        /// <summary>
        /// Ходы насоса 1 Аналоговый
        /// </summary>
        public P0008 P0008
        {
            get { return p8; }
        }

        /// <summary>
        /// Ходы насоса 2 Аналоговый
        /// </summary>        
        public P08_1 P08_1
        {
            get { return p8_1; }
        }

        /// <summary>
        /// Объём ДУ1 Датчик
        /// </summary>        
        public P0009 P0009
        {
            get { return p9; }
        }

        /// <summary>
        /// Объём ДУ2 Датчик
        /// </summary>        
        public P09_1 P09_1
        {
            get { return p9_1; }
        }

        /// <summary>
        /// Объём ДУ3 Датчик
        /// </summary>        
        public P09_2 P09_2
        {
            get { return p9_2; }
        }

        /// <summary>
        /// Объём ДУ4 Датчик
        /// </summary>        
        public P09_3 P09_3
        {
            get { return p9_3; }
        }

        /// <summary>
        /// Объём ДУ5 Датчик
        /// </summary>        
        public P09_4 P09_4
        {
            get { return p9_4; }
        }

        /// <summary>
        /// Объём ДУ6 Датчик
        /// </summary>        
        public P09_5 P09_5
        {
            get { return p9_5; }
        }

        /// <summary>
        /// Объём ДУ7 Датчик
        /// </summary>        
        public P09_6 P09_6
        {
            get { return p9_6; }
        }

        /// <summary>
        /// Объём ДУ8 Датчик
        /// </summary>        
        public P09_7 P09_7
        {
            get { return p9_7; }
        }

        /// <summary>
        /// Объём ДУ9 Датчик
        /// </summary>        
        public P09_8 P09_8
        {
            get { return p9_8; }
        }

        /// <summary>
        /// Объём ДУ10 Датчик
        /// </summary>        
        public P09_9 P09_9
        {
            get { return p9_9; }
        }

        /// <summary>
        /// Объём ДУ11 Датчик
        /// </summary>        
        public P9_10 P9_10
        {
            get { return p9_10; }
        }

        /// <summary>
        /// Объём ДУ12 Датчик
        /// </summary>        
        public P9_11 P9_11
        {
            get { return p9_11; }
        }

        /// <summary>
        /// Объём ДУ13 Датчик
        /// </summary>        
        public P9_12 P9_12
        {
            get { return p9_12; }
        }

        /// <summary>
        /// Объём ДУ14 Датчик
        /// </summary>        
        public P9_13 P9_13
        {
            get { return p9_13; }
        }

        /// <summary>
        /// Расход на входе Датчик
        /// </summary>
        public P0010 P0010
        {
            get { return p10; }
        }

        /// <summary>
        /// Ходы насоса 1 АСУ
        /// </summary>
        public P0011 P0011
        {
            get { return p11; }
        }

        /// <summary>
        /// Ходы насоса 2 АСУ
        /// </summary>        
        public P11_1 P11_1
        {
            get { return p11_1; }
        }

        /// <summary>
        /// Клинья АСУ
        /// </summary>
        public P0012 P0012
        {
            get { return p12; }
        }

        /// <summary>
        /// Вес на крюке Аналоговый
        /// </summary>        
        public P0013 P0013
        {
            get { return p13; }
        }

        /// <summary>
        /// Диаметр поршня 1 АСУ
        /// </summary>        
        public P0014 P0014
        {
            get { return p14; }
        }

        /// <summary>
        /// Диаметр поршня 1 АСУ
        /// </summary>        
        public P14_1 P14_1
        {
            get { return p14_1; }
        }

        /// <summary>
        /// Обороты ротора Аналоговый
        /// </summary>
        public P0015 P0015
        {
            get { return p15; }
        }

        /// <summary>
        /// Крутящий момент ротора АСУ
        /// </summary>        
        public P0016 P0016
        {
            get { return p16; }
        }

        /// <summary>
        /// Обороты ротора АСУ
        /// </summary>
        public P0017 P0017
        {
            get { return p17; }
        }

        /// <summary>
        /// Обороты СВП Аналоговый
        /// </summary>
        public P0018 P0018
        {
            get { return p18; }
        }

        /// <summary>
        /// Крутящий момент ротора
        /// </summary>
        public P0101 P0101
        {
            get { return p101; }
        }
        
        /// <summary>
        /// Вес на крюке
        /// </summary>
        public P0102 P0102
        {
            get
            {
                return p102;
            }
        }

        /// <summary>
        /// Скорость тальблока
        /// </summary>
        public P0103 P0103
        {
            get
            {
                return p103;
            }
        }

        /// <summary>
        /// Объем бурового раствора в емкостях (суммарный)
        /// </summary>
        public P0104 P0104
        {
            get
            {
                return p104;
            }
        }

        /// <summary>
        /// Изменение расхода на выходе
        /// </summary>
        public P0105 P0105
        {
            get
            {
                return p105;
            }
        }

        /// <summary>
        /// Изменение суммарного объема
        /// </summary>
        public P0106 P0106
        {
            get
            {
                return p106;
            }
        }

        /// <summary>
        /// Сигнал тревоги Газы 20%
        /// </summary>
        public P0107 P0107
        {
            get
            {
                return p107;
            }
        }

        /// <summary>
        /// Сигнал тревоги Газы 50%
        /// </summary>
        public P0108 P0108
        {
            get
            {
                return p108;
            }
        }

        /// <summary>
        /// Расход на входе по Ходам Насоса с Аналогового сигнала
        /// </summary>
        public P0109 P0109
        {
            get
            {
                return p109;
            }
        }

        /// <summary>
        /// Обороты ротора
        /// </summary>
        public P0110 P0110
        {
            get
            {
                return p110;
            }
        }

        /// <summary>
        /// Расход на входе Ходам Насоса АСУ
        /// </summary>
        public P0112 P0112
        {
            get
            {
                return p112;
            }
        }

        /// <summary>
        /// Расход на входе по Ходам Насоса
        /// </summary>
        public P0113 P0113
        {
            get
            {
                return p113;
            }
        }

        /// <summary>
        /// Расход на входе
        /// </summary>
        public P0114 P0114
        {
            get
            {
                return p114;
            }
        }

        /// <summary>
        /// Ходы Насоса 1
        /// </summary>
        public P0116 P0116
        {
            get
            {
                return p116;
            }
        }

        /// <summary>
        /// Ходы Насоса 2
        /// </summary>
        public P0117 P0117
        {
            get
            {
                return p117;
            }
        }

        /// <summary>
        /// Сумма ходов насоса
        /// </summary>
        public P0118 P0118
        {
            get
            {
                return p118;
            }
        }

        /// <summary>
        /// Вес колонны
        /// </summary>
        public P0200 P0200
        {
            get
            {
                return p200;
            }
        }

        /// <summary>
        /// Нагрузка на долото
        /// </summary>
        public P0201 P0201
        {
            get
            {
                return p201;
            }
        }

        /// <summary>
        /// Длина инструмента
        /// </summary>
        public P0202 P0202
        {
            get
            {
                return p202;
            }
        }

        /// <summary>
        /// Количество опущенных свеч
        /// </summary>
        public P0203 P0203
        {
            get
            {
                return p203;
            }
        }

        /// <summary>
        /// Положение инструмента
        /// </summary>
        public P0204 P0204
        {
            get
            {
                return p204;
            }
        }

        /// <summary>
        /// Глубина забоя
        /// </summary>
        public P0205 P0205
        {
            get
            {
                return p205;
            }
        }

        /// <summary>
        /// Состояние процесса бурения
        /// </summary>
        public P0206 P0206
        {
            get
            {
                return p206;
            }
        }

        /// <summary>
        /// Подача
        /// </summary>
        public P0207 P0207
        {
            get
            {
                return p207;
            }
        }

        /// <summary>
        /// Мех. скорость проходки в единицах м/час
        /// </summary>
        public P0208 P0208
        {
            get
            {
                return p208;
            }
        }

        /// <summary>
        /// Время бурения 1м проходки
        /// </summary>
        public P0209 P0209
        {
            get
            {
                return p209;
            }
        }

        /// <summary>
        /// Скорость СПО
        /// </summary>
        public P0210 P0210
        {
            get
            {
                return p210;
            }
        }

        /// <summary>
        /// Над забоем в метрах
        /// </summary>
        public P0211 P0211
        {
            get
            {
                return p211;
            }
        }

        /// <summary>
        /// время циркуляции
        /// </summary>
        public P0212 P0212
        {
            get
            {
                return p212;
            }
        }

        /// <summary>
        /// Время бурения
        /// </summary>
        public P0213 P0213
        {
            get
            {
                return p213;
            }
        }

        /// <summary>
        /// Список технологических параметров
        /// </summary>
        public TParameter[] Parameters
        {
            get
            {
                TParameter[] parameters = { p1, p2, p3, p4, p5, p6, p6_1, p6_2, p6_3, p6_4, p6_5, p6_6, 
                                              p6_7, p6_8, p6_9, p7, p7_1, p7_2, p7_3, p7_4, p7_5, p7_6, p7_7, 
                                              p7_8, p7_9, p7_10, p7_11, p7_12, p7_13, p8, p8_1, p9, p9_1, p9_2, 
                                              p9_3, p9_4, p9_5, p9_6, p9_7, p9_8, p9_9, p9_10, p9_11, p9_12, p9_13, 
                                              p10, p11, p11_1, p12, p13, p14, p14_1, p15, p16, p17,p18, p101, p102, p103,
                                              p104, p105, p106, p107, p108, p109, p110, p112, p113, p114, p116, p117, p118,
                                              p200, p201, p202, p203, p204, p205, p206, p207, p208, p209, p210, p211, p212, 
                                              p213 };
                return parameters;
            }
        }

        /// <summary>
        /// Актуализировать ссылки на параметры
        /// </summary>
        public void ActualizedParameters()
        {
            try
            {
                SgtApplication _app = SgtApplication.CreateInstance();
                if (_app != null)
                {
                    TParameter[] t_parames = Parameters;
                    if (t_parames != null)
                    {
                        foreach (TParameter t_param in t_parames)
                        {
                            if (t_param != null)
                            {
                                Parameter p_param = _app.GetParameter(t_param.Identifier);
                                if (p_param != null && p_param.Channel != null)
                                {
                                    t_param.PNumber = p_param.Channel.Number;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteToLog(this, new ErrorArgs(ex.Message, ErrorType.NotFatal));
            }
        }
    }
}