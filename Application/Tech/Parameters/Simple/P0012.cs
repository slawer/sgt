using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Клинья АСУ
    /// </summary>
    public class P0012 : TParameter
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0012(Guid p_identifier)
            : base(p_identifier, "P0012", "Клинья АСУ")
        {
        }
    }
}