using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Обороты ротора Аналоговый
    /// </summary>
    public class P0015 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0015(Guid p_identifier)
            : base(p_identifier, "P0015", "Обороты ротора Аналоговый")
        {
        }
    }
}