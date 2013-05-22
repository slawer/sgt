using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Ходы насоса 1 АСУ
    /// </summary>
    public class P0011 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0011(Guid p_identifier)
            : base(p_identifier, "P0011", "Ходы насоса 1 АСУ")
        {
        }
    }
}