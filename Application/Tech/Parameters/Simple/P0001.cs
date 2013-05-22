using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Вес на крюке Датчик
    /// </summary>
    public class P0001 : TParameter
    {

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор технологического параметра</param>
        public P0001(Guid p_identifier)
            : base(p_identifier, "P0001", "Вес на крюке Датчик")
        {
        }
    }
}