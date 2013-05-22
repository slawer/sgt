using System;

namespace SGT
{
    /// <summary>
    /// Реализует технологию СГТ
    /// </summary>
    public partial class Technology
    {
        /// <summary>
        /// Маска команды АСУ Нагрузка
        /// </summary>
        protected readonly int Маска_АСУ_Нагрузка = 1;

        /// <summary>
        /// Маска команды АСУ Поток
        /// </summary>
        protected readonly int Маска_АСУ_Поток = 2;

        /// <summary>
        /// Маска команды АСУ Тальблок
        /// </summary>
        protected readonly int Маска_АСУ_Тальблок = 4;

        /// <summary>
        /// Маска команды АСУ Подача
        /// </summary>
        protected readonly int Маска_АСУ_Подача = 8;

        /// <summary>
        /// Маска команды АСУ Объем
        /// </summary>
        protected readonly int Маска_АСУ_Объем = 16;

        /// <summary>
        /// Маска команды Пульта Бурильщика Нагрузка
        /// </summary>
        protected readonly int Маска_ПультаБурильщика_Нагрузка = 1;

        /// <summary>
        /// Маска команды Пульта Бурильщика Тальблок
        /// </summary>
        protected readonly int Маска_ПультаБурильщика_Тальблок = 2;

        /// <summary>
        /// Маска команды Пульта Бурильщика Подача
        /// </summary>
        protected readonly int Маска_ПультаБурильщика_Подача = 3;

        /// <summary>
        /// Маска команды Пульта Бурильщика Объем
        /// </summary>
        protected readonly int Маска_ПультаБурильщика_Объем = 4;

        /// <summary>
        /// Маска команды Пульта Бурильщика Поток
        /// </summary>
        protected readonly int Маска_ПультаБурильщика_Поток = 5;

        protected Guid id_driller_console;                  // идентификатор команды пульта бурильщика АСУ
        protected Guid id_driller_console_weight_column;    // идентификатор комманды Пульта Бурильщика Ореол (Через БКСД)

        protected int driller_console;                      // номер параметра в списке , который содержит команду пульта бурильщика АСУ
        protected int driller_console_weight_column;        // номер параметра в списке , который содержит команду пульта бурильщика Ореол

        /// <summary>
        /// Выполнить инициализацию параметров перед началом обработки поступивших данных
        /// </summary>
        protected void InitializeTechData(float[] slice)
        {
            ParseCommands(slice);
        }

        /// <summary>
        /// Разбор команд, поступивших из АСУ и пультов управления буровой
        /// </summary>
        protected void ParseCommands(float[] slice)
        {
            // Разбор команд, поступивших с пульта управления

            try
            {
                SgtApplication _app = SgtApplication.CreateInstance();
                if (_app != null)
                {
                    Parameter drillerParam = _app.GetParameter(id_driller_console);
                    Parameter drweParam = _app.GetParameter(id_driller_console_weight_column);

                    if (drillerParam != null && drillerParam.Channel != null)
                    {
                        driller_console = drillerParam.Channel.Number;
                    }
                    else
                        driller_console = -1;

                    if (drweParam != null && drweParam.Channel != null)
                    {
                        driller_console_weight_column = drweParam.Channel.Number;
                    }
                    else
                        driller_console_weight_column = -1;

                }

                if (driller_console_weight_column > -1 && driller_console_weight_column < slice.Length)
                {
                    // Разбор команд, поступивших с Пульта бурильщика Ореол
                    float _v = slice[driller_console_weight_column];
                    if (!float.IsNaN(_v))
                    {
                        int _cmd = ((int)Math.Round(_v) & 7); // Вырезаем первых 3 бита!

                        if (_cmd == Маска_ПультаБурильщика_Нагрузка) // Реализация команды Нагрузка
                        {
                            P0200.ResetStartingPoint();
                        }
                        else
                        if (_cmd == Маска_ПультаБурильщика_Поток) // Реализация команды Поток
                        {
                            P0105.StartingPoint = float.NaN;
                        }
                        else
                        if (_cmd == Маска_ПультаБурильщика_Тальблок) // Реализация команды Тальблок
                        {
                            _app.DoTalblock(0);
                        }
                        else
                        if (_cmd == Маска_ПультаБурильщика_Подача) // Реализация команды Подача
                        {
                            P0207.ResetStartingPoint();
                        }
                        else
                        if (_cmd == Маска_АСУ_Объем) // Реализация команды Обьем
                        {
                            P0106.StartingPoint = float.NaN;
                        }
                    }
                }

                if (driller_console > -1 && driller_console < slice.Length)
                {
                    // Разбор команд, поступивших с Пульта Бурильщика АСУ
                    float _v = slice[driller_console];
                    if (!float.IsNaN(_v))
                    {
                        int _cmd = (int)Math.Round(_v);

                        if ((_cmd & Маска_АСУ_Нагрузка) != 0) // Реализация команды Нагрузка
                        {
                            P0200.ResetStartingPoint();
                        }
                        else
                        if ((_cmd & Маска_АСУ_Поток) != 0) // Реализация команды Поток
                        {
                            P0105.StartingPoint = float.NaN;
                        }
                        else
                        if ((_cmd & Маска_АСУ_Тальблок) != 0) // Реализация команды Тальблок
                        {
                            _app.DoTalblock(0);
                        }
                        else
                        if ((_cmd & Маска_АСУ_Подача) != 0) // Реализация команды Подача
                        {
                            P0207.ResetStartingPoint();
                        }
                        else
                        if ((_cmd & Маска_АСУ_Объем) != 0) // Реализация команды Обьем
                        {
                            P0106.StartingPoint = float.NaN;
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