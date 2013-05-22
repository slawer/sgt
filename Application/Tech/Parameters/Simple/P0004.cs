using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Давление Датчик
    /// </summary>
    public class P0004 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0004(Guid p_identifier)
            : base(p_identifier, "P0004", "Давление Датчик")
        {
        }
    }
}