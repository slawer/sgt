using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ10 Датчик
    /// </summary>
    public class P07_9 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P07_9(Guid p_identifier)
            : base(p_identifier, "P07_9", "Уровень ДУ10 Датчик")
        {
        }
    }
}