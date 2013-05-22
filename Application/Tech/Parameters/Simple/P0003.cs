using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Поток на выходе Датчик
    /// </summary>
    public class P0003 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0003(Guid p_identifier)
            : base(p_identifier, "P0003", "Поток на выходе Датчик")
        {
        }
    }
}