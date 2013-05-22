using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Ходы насоса 1 Аналоговый
    /// </summary>
    public class P0008 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0008(Guid p_identifier)
            : base(p_identifier, "P0008", "Ходы насоса 1 Аналоговый")
        {
        }
    }
}