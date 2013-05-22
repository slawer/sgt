using System;
using System.Xml;

namespace SGT
{
    /// <summary>
    /// Реализует технологию СГТ
    /// </summary>
    public partial class Technology
    {
        /// <summary>
        /// Корневой узел технологии
        /// </summary>
        public const string RootName = tech_root;

        /// <summary>
        /// корневой узел настроек технологии
        /// </summary>
        protected const string tech_root = "technology";

        /// <summary>
        /// узел в котором содержаться параметры
        /// </summary>
        protected const string parameters_root = "parameters";

        /// <summary>
        /// узел в котором содержится блокировочное значение веса на крюке
        /// </summary>
        protected const string locking_weight_hook_name = "locking_weight_hook";
        
        /// <summary>
        /// узел в котором содержится Интервал ПЗР
        /// </summary>        
        protected const string interval_pzr_name = "interval_pzr";

        /// <summary>
        /// узел в котором содержится интервал бурения
        /// </summary>
        protected const string drilling_interval_name = "drilling_interval";

        /// <summary>
        /// узел в котором содержится размер призабойной зоны
        /// </summary>
        protected const string size_bottom_hole_zone_name = "size_bottom_hole_zone";

        /// <summary>
        /// узел в котором содержится Блокировочное значение давления на входе
        /// </summary>
        protected const string locking_pressure_name = "locking_pressure";

        /// <summary>
        /// узел в котором содержится размер компоновки низа колонны
        /// </summary>
        protected const string size_layout_bottom_column_name = "size_layout_bottom_column";

        /// <summary>
        /// узел в котором содержится размер компоновки верха колонны
        /// </summary>
        protected const string size_layout_top_column_name = "size_layout_top_column";

        /// <summary>
        /// узел в котором содержится блокировочное значене оборотов ротора
        /// </summary>
        protected const string locking_value_rotor_speed_name = "locking_value_rotor_speed";

        /// <summary>
        /// узел в котором содержится блокировочное значение нагрузки
        /// </summary>
        protected const string locking_value_load_name = "locking_value_load";

        /// <summary>
        /// узел в котором содержится шаг усреднения механической скорости
        /// </summary>
        protected const string averaging_mechanical_speed_name = "averaging_mechanical_speed";

        /// <summary>
        /// узел в котором содержится шаг усреднения времени бурения 1 м
        /// </summary>
        protected const string averaging_time_drilling_name = "averaging_time_drilling";

        /// <summary>
        /// узел в котором содержится период усреднения для скорости тальблока
        /// </summary>
        protected const string averaging_period_rate_talbloka_name = "averaging_period_rate_talbloka";

        /// <summary>
        /// узел в котором содержится номер параметра в списке , который содержит команду пульта бурильщика
        /// </summary>
        protected const string driller_console_name = "driller_console";

        /// <summary>
        /// узел в котором содержится номер параметра в списке , который содержит команду пульта бурильщика на сброс веса колоны
        /// </summary>
        protected const string driller_console_weight_column_name = "driller_console_weight_column";

        /// <summary>
        /// узел в котором сожержится блокировочное значение скорости тальблока
        /// </summary>
        protected const string locking_speed_talbloka_name = "locking_speed_talbloka";

        /// <summary>
        /// Имя узла в котором содержится метод расчета режима бурения
        /// </summary>
        protected const string r_drilling_name = "r_drilling";
        
        /// <summary>
        /// Имя узла в котором содержится метод расчета проработка
        /// </summary>
        protected const string r_study_name = "r_study";

        /// <summary>
        /// Имя узла в котором содержится метод расчета технологического режима
        /// </summary>
        protected const string r_weight_name = "r_weight";

        /// <summary>
        /// Сохранить конфигурацию
        /// </summary>
        /// <param name="doc">файл в который выполняется сохранение</param>
        /// <param name="root">корневой узел в который осуществляется сохранение</param>
        public void Save(XmlDocument doc, XmlNode root)
        {
            if (c_slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (doc != null && root != null)
                    {
                        XmlNode t_root = doc.CreateElement(tech_root);

                        XmlNode locking_weight_hook_node = doc.CreateElement(locking_weight_hook_name);
                        XmlNode interval_pzr_node = doc.CreateElement(interval_pzr_name);

                        XmlNode drilling_interval_node = doc.CreateElement(drilling_interval_name);
                        XmlNode size_bottom_hole_zone_node = doc.CreateElement(size_bottom_hole_zone_name);

                        XmlNode locking_pressure_node = doc.CreateElement(locking_pressure_name);

                        XmlNode size_layout_top_column_node = doc.CreateElement(size_layout_top_column_name);
                        XmlNode size_layout_bottom_column_node = doc.CreateElement(size_layout_bottom_column_name);

                        XmlNode locking_value_rotor_speed_node = doc.CreateElement(locking_value_rotor_speed_name);

                        XmlNode locking_value_load_node = doc.CreateElement(locking_value_load_name);

                        XmlNode averaging_mechanical_speed_node = doc.CreateElement(averaging_mechanical_speed_name);
                        XmlNode averaging_time_drilling_node = doc.CreateElement(averaging_time_drilling_name);

                        XmlNode averaging_period_rate_talbloka_node = doc.CreateElement(averaging_period_rate_talbloka_name);

                        XmlNode driller_console_node = doc.CreateElement(driller_console_name);

                        XmlNode driller_console_weight_column_node = doc.CreateElement(driller_console_weight_column_name);
                        XmlNode locking_speed_talbloka_node = doc.CreateElement(locking_speed_talbloka_name);

                        XmlNode r_drilling_node = doc.CreateElement(r_drilling_name);
                        XmlNode r_study_node = doc.CreateElement(r_study_name);

                        XmlNode r_weight_node = doc.CreateElement(r_weight_name);

                        locking_weight_hook_node.InnerText = locking_weight_hook.ToString();
                        interval_pzr_node.InnerText = interval_pzr.ToString();

                        drilling_interval_node.InnerText = drilling_interval.ToString();
                        size_bottom_hole_zone_node.InnerText = size_bottom_hole_zone.ToString();

                        locking_pressure_node.InnerText = locking_pressure.ToString();

                        size_layout_top_column_node.InnerText = size_layout_top_column.ToString();
                        size_layout_bottom_column_node.InnerText = size_layout_bottom_column.ToString();

                        locking_value_rotor_speed_node.InnerText = locking_value_rotor_speed.ToString();
                        locking_value_load_node.InnerText = locking_value_load.ToString();

                        driller_console_node.InnerText = id_driller_console.ToString();

                        driller_console_weight_column_node.InnerText = id_driller_console_weight_column.ToString();
                        locking_speed_talbloka_node.InnerText = locking_speed_talbloka.ToString();

                        r_drilling_node.InnerText = r_drilling.ToString();
                        r_study_node.InnerText = r_study.ToString();

                        r_weight_node.InnerText = r_weight.ToString();
                        
                        // ---------------------

                        t_root.AppendChild(locking_weight_hook_node);
                        t_root.AppendChild(interval_pzr_node);

                        t_root.AppendChild(drilling_interval_node);
                        t_root.AppendChild(size_bottom_hole_zone_node);

                        t_root.AppendChild(locking_pressure_node);

                        t_root.AppendChild(size_layout_top_column_node);
                        t_root.AppendChild(size_layout_bottom_column_node);

                        t_root.AppendChild(locking_value_rotor_speed_node);

                        t_root.AppendChild(locking_value_load_node);

                        t_root.AppendChild(averaging_mechanical_speed_node);
                        t_root.AppendChild(averaging_time_drilling_node);

                        t_root.AppendChild(averaging_period_rate_talbloka_node);
                        t_root.AppendChild(driller_console_node);

                        t_root.AppendChild(driller_console_weight_column_node);
                        t_root.AppendChild(locking_speed_talbloka_node);

                        t_root.AppendChild(r_drilling_node);
                        t_root.AppendChild(r_study_node);

                        t_root.AppendChild(r_weight_node);

                        SaveParameters(doc, t_root);
                        root.AppendChild(t_root);
                    }
                }
                finally
                {
                    c_slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Сохранить параметры
        /// </summary>
        /// <param name="doc">файл в который выполняется сохранение</param>
        /// <param name="root">корневой узел в который осуществляется сохранение</param>
        protected void SaveParameters(XmlDocument doc, XmlNode root)
        {
            TParameter[] parameters = Parameters;
            if (parameters != null)
            {
                XmlNode p_root = doc.CreateElement(parameters_root);
                foreach (TParameter parameter in parameters)
                {
                    if (parameter != null)
                    {
                        XmlNode node = parameter.Save(doc);
                        if (node != null)
                        {
                            p_root.AppendChild(node);
                        }
                    }
                }

                root.AppendChild(p_root);
            }
        }

        /// <summary>
        /// Загрузить конфигурацию
        /// </summary>
        /// <param name="Root">Корневой узел, в котором сохранена конфигурация технологии</param>
        public void Load(XmlNode Root)
        {
            if (c_slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (Root != null && Root.HasChildNodes)
                    {
                        foreach (XmlNode Child in Root)
                        {
                            switch (Child.Name)
                            {
                                case parameters_root:

                                    try
                                    {
                                        LoadParameters(Child);
                                    }
                                    catch { }
                                    break;

                                case locking_weight_hook_name:

                                    try
                                    {
                                        locking_weight_hook = float.Parse(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case interval_pzr_name:

                                    try
                                    {
                                        interval_pzr = float.Parse(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case drilling_interval_name:

                                    try
                                    {
                                        drilling_interval = float.Parse(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case size_bottom_hole_zone_name:

                                    try
                                    {
                                        size_bottom_hole_zone = float.Parse(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case locking_pressure_name:

                                    try
                                    {
                                        locking_pressure = float.Parse(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case size_layout_bottom_column_name:

                                    try
                                    {
                                        size_layout_bottom_column = float.Parse(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case size_layout_top_column_name:

                                    try
                                    {
                                        size_layout_top_column = float.Parse(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case locking_value_rotor_speed_name:

                                    try
                                    {
                                        locking_value_rotor_speed = float.Parse(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case locking_value_load_name:

                                    try
                                    {
                                        locking_value_load = float.Parse(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case driller_console_name:

                                    try
                                    {
                                        id_driller_console = new Guid(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case driller_console_weight_column_name:

                                    try
                                    {
                                        id_driller_console_weight_column = new Guid(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case locking_speed_talbloka_name:

                                    try
                                    {
                                        locking_speed_talbloka = float.Parse(Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                case r_drilling_name:

                                    try
                                    {
                                        r_drilling = (TechnologicalRegimDrilling)Enum.Parse(typeof(TechnologicalRegimDrilling), Child.InnerText);
                                    }
                                    catch{ }
                                    break;

                                case r_study_name:

                                    try
                                    {
                                        r_study = (TechnologicalRegimStudy)Enum.Parse(typeof(TechnologicalRegimStudy), Child.InnerText);
                                    }
                                    catch{ }
                                    break;

                                case r_weight_name:

                                    try
                                    {
                                        r_weight = (TechnologicalRegimeWeightHook)Enum.Parse(typeof(TechnologicalRegimeWeightHook), Child.InnerText);
                                    }
                                    catch { }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
                finally
                {
                    c_slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Загрузить параметры
        /// </summary>
        /// <param name="p_root">Узел в котором сохранены параметры</param>
        protected void LoadParameters(XmlNode p_root)
        {
            if (p_root != null && p_root.HasChildNodes)
            {
                TParameter[] parameters = Parameters;
                if (parameters != null)
                {
                    foreach (XmlNode Child in p_root.ChildNodes)
                    {
                        if (Child.Name == TParameter.RootName)
                        {
                            XmlAttributeCollection attrs = Child.Attributes;
                            if (attrs != null && attrs.Count > 0)
                            {
                                LdrPrm(parameters, Child, attrs[0].Name);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Загрузить данные в технологический параметр
        /// </summary>
        /// <param name="prms">Список технологических параметров</param>
        /// <param name="node">Узел в котором содержаться данные</param>
        /// <param name="id">Идентификатор параметра</param>
        protected void LdrPrm(TParameter[] prms, XmlNode node, String id)
        {
            try
            {
                foreach (TParameter prm in prms)
                {
                    if (prm != null)
                    {
                        if (prm.UnigueClassName == id)
                        {
                            prm.Load(node);
                            break;
                        }
                    }
                }
            }
            catch { }
        }
    }
}