using System;
using System.Collections.Generic;

namespace DataBase
{
    /// <summary>
    /// Реализует параметр базы данных
    /// </summary>
    public class DataBaseParameter
    {
        private Guid guid;                                  // Глобальноуникальный идентификатор параметра (соответствует идентификатору параметра конвейера)
        private DateTime created;                           // время создания параметра в БД

        private int id = -1;                                // поле id параметра в главной таблице БД
        private int numbe_prm = -1;                         // поле numbe_prm  в главной таблице БД

        private string project;                             // поле project в главной таблице
        private string p_work;                              // поле p_work в главной таблице
        
        private string t_values = String.Empty;             // название таблицы в которой храняться значения параметра
        private string t_history = String.Empty;            // название таблицы в которой храниться описание параметра

        private List<DataBaseDescription> descriptions;     // записи содержащие описание параметра

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseParameter()
        {
            project = string.Empty;
            p_work = string.Empty;

            descriptions = new List<DataBaseDescription>();
        }

        /// <summary>
        /// инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="identifier">Используемый идентификатор параметра</param>
        public DataBaseParameter(Guid identifier)
            : this()
        {
            guid = identifier;
        }

        /// <summary>
        /// Определяет  название таблицы в которой храняться значения параметра
        /// </summary>
        public string tblValues
        {
            get { return t_values; }
            set { t_values = value; }
        }

        /// <summary>
        /// Определяет  название таблицы в которой храниться описание параметра
        /// </summary>
        public string tblHistory
        {
            get { return t_history; }
            set { t_history = value; }
        }

        /// <summary>
        /// Определяет название проекта к которому относится данный параметр
        /// </summary>
        public string Project
        {
            get { return project; }
            set { project = value; }
        }

        /// <summary>
        /// Определяет работу к которой относится данный параметр
        /// </summary>
        public string Work
        {
            get { return p_work; }
            set { p_work = value; }
        }

        /// <summary>
        /// Определяет поле id параметра в главной таблице БД.
        /// Не идентификатор параметра.
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Определяет поле numbe_prm  в главной таблице БД.
        /// Два параметра не должны иметь равные значения данного поля (необходимо для совместимости).
        /// </summary>
        public int Numbe_Prm
        {
            get { return numbe_prm; }
            set { numbe_prm = value; }
        }

        /// <summary>
        /// Определяет глобально уникальный идентификатор параметра
        /// (соответствует идентификатору параметра в программе).
        /// </summary>
        public Guid Identifier
        {
            get { return guid; }
            set
            {
                guid = new Guid(value.ToByteArray());
            }
        }

        /// <summary>
        /// Определяет время создания параметра в БД
        /// </summary>
        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }

        /// <summary>
        /// Определяет  записи содержащие описание параметра
        /// </summary>
        public List<DataBaseDescription> Descriptions
        {
            get { return descriptions; }
        }
    }

    /// <summary>
    /// Реализует описание параметра базы данных
    /// </summary>
    public class DataBaseDescription
    {
        private int id;                     // id строки таблицы
        private DateTime dt_create;         // время создания записи
        
        private int main_key;               // ссылка на таблицу t_main. t_main.id == main_key

        private int numbe_prm;              // номер параметра. соответствует номеру параметра в КРС (не используется, но должен иметь корректное значение)
        private string name_prm;            // текстовое описание параметра

        private string type_prm;            // текстовое описание единиц измерения параметра

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public DataBaseDescription()
        {
            name_prm = string.Empty;
            type_prm = string.Empty;
        }

        /// <summary>
        /// Определяет id строки
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Определяет время создания записи
        /// </summary>
        public DateTime dtCreate
        {
            get { return dt_create; }
            set { dt_create = value; }
        }

        /// <summary>
        /// Определяет ссылку на таблицу t_main. t_main.id == main_key
        /// </summary>
        public int MainKey
        {
            get { return main_key; }
            set { main_key = value; }
        }

        /// <summary>
        /// Определяет номер параметра. соответствует номеру параметра в КРС (не используется, но должен иметь корректное значение)
        /// </summary>
        public int NumberParameter
        {
            get { return numbe_prm; }
            set { numbe_prm = value; }
        }

        /// <summary>
        /// Определяет текстовое описание параметра
        /// </summary>
        public string NameParameter
        {
            get { return name_prm; }
            set { name_prm = value; }
        }

        /// <summary>
        /// Определяет текстовое описание единиц измерения параметра
        /// </summary>
        public string TypeParameter
        {
            get { return type_prm; }
            set { type_prm = value; }
        }
    }
}