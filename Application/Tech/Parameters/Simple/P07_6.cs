using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ7 Датчик
    /// </summary>
    public class P07_6 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P07_6(Guid p_identifier)
            : base(p_identifier, "P07_6", "Уровень ДУ7 Датчик")
        {
        }
    }
}