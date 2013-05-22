using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Обороты СВП Аналоговый
    /// </summary>
    public class P0018 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0018(Guid p_identifier)
            : base(p_identifier, "P0018", "Обороты СВП Аналоговый")
        {
        }
    }
}