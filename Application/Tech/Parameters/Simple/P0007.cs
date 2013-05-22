using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Уровень ДУ1 Датчик
    /// </summary>
    public class P0007 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0007(Guid p_identifier)
            : base(p_identifier, "P0007", "Уровень ДУ1 Датчик")
        {
        }
    }
}