using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ5 Датчик
    /// </summary>
    public class P07_4 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P07_4(Guid p_identifier)
            : base(p_identifier, "P07_4", "Уровень ДУ5 Датчик")
        {
        }
    }
}