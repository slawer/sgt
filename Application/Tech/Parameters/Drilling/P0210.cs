using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Скорость СПО
    /// Скорость определена для состояний, отличных от бурения
    /// </summary>
    public class P0210 : TParameter
    {
        /// <summary>
        /// Константа Над забоем / Бурение / Бурение
        /// </summary>
        public static int НадЗабоем_Бурение_Бурение = 0 + 128 + 6;

        /// <summary>
        /// Константа Пустой крюк  / ПЗР / ПЗР
        /// </summary>
        public static int ПустойКрюк_ПЗР_ПЗР = 1024 + 0 + 1;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0210(Guid p_identifier)
            : base(p_identifier, "P0210", "Скорость СПО")
        {
            simple = false;
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Скорость СПО
        /// </summary>
        /// <param name="v1">Скорость тальблока в текущий момент</param>
        /// <param name="v2">Состояние процесса бурения в текущий момент</param>
        public void Calculate(P0103 v1, P0206 v2)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (!float.IsNaN(v2.Value))
                    {
                        int _state = (int)Math.Round(v2.Value);
                        if (_state == НадЗабоем_Бурение_Бурение)
                        {
                            _value = 0;
                        }
                        else
                        {
                            if (_state < ПустойКрюк_ПЗР_ПЗР) // Надеюсь, значения констант полностью сохранены и можно пользоваться этим неравенством :-)
                            {
                                _value = v1.Value;
                            }
                            else
                                _value = 0;
                        }
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
    }
}