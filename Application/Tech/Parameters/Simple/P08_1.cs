using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Ходы насоса 2 Аналоговый
    /// </summary>
    public class P08_1 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P08_1(Guid p_identificator)
            : base(p_identificator, "P08_1", "Ходы насоса 2 Аналоговый")
        {
        }
    }
}