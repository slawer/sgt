using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ6 Датчик
    /// </summary>
    public class P07_5 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P07_5(Guid p_identifier)
            : base(p_identifier, "P07_5", "Уровень ДУ6 Датчик")
        {
        }
    }
}