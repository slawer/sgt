using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Вес на крюке Аналоговый
    /// </summary>
    public class P0013 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0013(Guid p_identifier)
            : base(p_identifier, "P0013", "Вес на крюке Аналоговый")
        {
        }
    }
}