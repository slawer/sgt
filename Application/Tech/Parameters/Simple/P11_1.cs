using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Ходы насоса 2 АСУ
    /// </summary>
    public class P11_1 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P11_1(Guid p_identifier)
            : base(p_identifier, "P11_1", "Ходы насоса 2 АСУ")
        {
        }
    }
}