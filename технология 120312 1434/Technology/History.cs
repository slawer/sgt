using System;

namespace Technology
{
    /// <summary>
    /// реализует историю значений параметров технологии
    /// </summary>
    public class History
    {
        /// <summary>
        /// Метод возвращающий значение параметра в указанное время
        /// </summary>
        /// <param name="parameter">Номер параметра</param>
        /// <param name="time">Время параметра</param>
        /// <returns>Значение параметра в указанное время. 
        /// Если нету параметра в списке или нету значения в указанное время возвращяется float.NaN</returns>
        public static float GetParameterValue(int parameter, DateTime time)
        {
            return float.NaN;
        }
    }
}