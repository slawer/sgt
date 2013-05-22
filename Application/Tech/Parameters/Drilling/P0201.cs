using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Нагрузка на долото
    /// </summary>
    public class P0201 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0201(Guid p_identifier)
            : base(p_identifier, "P0201", "Нагрузка на долото")
        {
            simple = false;
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Нагрузка на долото
        /// </summary>
        /// <param name="v1">Давление в текущий момент</param>
        /// <param name="v2">Клинья в текущий момент</param>
        /// <param name="v3">Вес на крюке</param>
        /// <param name="v4">Вес колонны</param>
        /// <param name="locking_weight_hook">Блокировочное значение веса на крюке</param>
        /// <param name="locking_pressure">Блокировочное значение давления</param>
        /// <param name="r_weight">Возможные методы расчета состояния Вес на крюке</param>
        public void Calculate(P0004 v1, P0012 v2, P0102 v3, P0200 v4, float locking_weight_hook,
            float locking_pressure, TechnologicalRegimeWeightHook r_weight)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (float.IsNaN(v3.Value) || float.IsNaN(v4.Value) || float.IsNaN(v1.Value))
                    {
                        _value = float.NaN;
                    }
                    else
                    {
                        _value = 0;
                        if (WeightHookForWeightOrWedges(v3, v2, locking_weight_hook, r_weight) == TProcResult.True)
                        {
                            if (v1.Value > locking_pressure) // Pr>БЗ
                            {
                                _value = v4.Value - v3.Value;
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