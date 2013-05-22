using System;

namespace SGT
{
    /// <summary>
    /// Реализует параметр Состояние процесса бурения
    /// </summary>
    public class P0206 :TParameter
    {
        // ========================================================================================
        // Константы, определяющие состояни технологического процесса

        /// <summary>
        /// Значение не определно
        /// </summary>
        public static int Техпроцесс_Default = 0;

        /// <summary>
        /// Константа Над забоем / ПЗР / ПЗР
        /// </summary>
        public static int НадЗабоем_ПЗР_ПЗР = 0 + 0 + 1;

        /// <summary>
        /// Константа Над забоем / СПО / СПО
        /// </summary>
        public static int НадЗабоем_СПО_СПО = 0 + 64 + 2;

        /// <summary>
        /// Константа Над забоем / СПО / Промывка
        /// </summary>
        public static int НадЗабоем_СПО_Промывка = 0 + 64 + 3;

        /// <summary>
        /// Константа Над забоем / СПО / Проработка
        /// </summary>
        public static int НадЗабоем_СПО_Проработка = 0 + 64 + 4;

        /// <summary>
        /// Константа Над забоем / Бурение / Промывка
        /// </summary>
        public static int НадЗабоем_Бурение_Промывка = 0 + 128 + 3;

        /// <summary>
        /// Константа Над забоем / Бурение / Проработка
        /// </summary>
        public static int НадЗабоем_Бурение_Проработка = 0 + 128 + 4;

        /// <summary>
        /// Константа Над забоем / Бурение / Наращивание
        /// </summary>
        public static int НадЗабоем_Бурение_Наращивание = 0 + 128 + 5;

        /// <summary>
        /// Константа Над забоем / Бурение / Бурение
        /// </summary>
        public static int НадЗабоем_Бурение_Бурение = 0 + 128 + 6;

        // ...

        /// <summary>
        /// Константа Пустой крюк  / ПЗР / ПЗР
        /// </summary>
        public static int ПустойКрюк_ПЗР_ПЗР = 1024 + 0 + 1;

        /// <summary>
        /// Константа Пустой крюк / СПО / СПО
        /// </summary>
        public static int ПустойКрюк_СПО_СПО = 1024 + 64 + 2;

        /// <summary>
        /// Константа Над Пустой крюк / Бурение / Наращивание
        /// </summary>
        public static int ПустойКрюк_Бурение_Наращивание = 1024 + 128 + 5;

        // ...

        // ========================================================================================

        private string tech_stage = string.Empty;
        private string tech_regime = string.Empty;

        private string tech_hook = string.Empty;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="p_identifier">Идентификатор параметра</param>
        public P0206(Guid p_identifier)
            : base(p_identifier, "P0206", "Состояние процесса бурения")
        {
            simple = false;
            _value = Техпроцесс_Default;
        }

        // --------------- технологический этап и режим работы ---------------


        /// <summary>
        /// Возвращяет текстовое описание текущего технологического этапа работы
        /// </summary>
        public string Stage
        {
            get
            {
                return tech_stage;
            }
        }

        /// <summary>
        /// Возвращяет текстовое описание текущего технологического режима работы
        /// </summary>
        public string Regime
        {
            get
            {
                return tech_regime;
            }
        }

        /// <summary>
        /// Возвращяет текстовое описание крюка
        /// </summary>
        public string Hook
        {
            get
            {
                return tech_hook;
            }
        }

        /// <summary>
        /// Вычисляет текущее значение параметра Состояние процесса бурения
        /// </summary>
        /// <param name="v1">Давление в текущий момент</param>
        /// <param name="v2">Клинья в текущий момент</param>
        /// <param name="v3">Вес на крюке в текущий момент</param>
        /// <param name="v4">Скорость тальблока в текущий момент</param>
        /// <param name="v5">Обороты ротора в текущий момент</param>
        /// <param name="v6">Нагрузка на долото в текущий момент</param>
        /// <param name="v8">Положение инструмента в текущий момент</param>
        /// <param name="v9">Глубина забоя в текущий момент</param>
        /// <param name="currentTime">текущее технологическое время</param>
        /// <param name="locking_weight_hook">Блокировочное значение веса на крюке</param>
        /// <param name="interval_pzr">Интервал ПЗР</param>
        /// <param name="drilling_interval">Интервал бурения</param>
        /// <param name="size_bottom_hole_zone">Размер призабойной зоны</param>
        /// <param name="locking_pressure">Блокировочное значение давления</param>
        /// <param name="locking_value_rotor_speed">Блокировочное значение оборотов ротора</param>
        /// <param name="locking_value_load">Блокировочное значение нагрузки</param>
        /// <param name="locking_speed_talbloka">Блокировочное значение скорости тальблока</param>
        /// <param name="r_drilling">Метод расчета режима бурения</param>
        /// <param name="r_study">Метод расчета проработка</param>
        /// <param name="r_weight">Метод расчета технологического режима</param>
        public void Calculate(
            P0004 v1,
            P0012 v2,
            P0102 v3,
            P0103 v4,
            P0110 v5,
            P0201 v6,
            P0204 v8,
            P0205 v9,
            DateTime currentTime,
            float locking_weight_hook,
            float interval_pzr,
            float drilling_interval,
            float size_bottom_hole_zone,
            float locking_pressure,
            float locking_value_rotor_speed,
            float locking_value_load,
            float locking_speed_talbloka,
            TechnologicalRegimDrilling r_drilling,
            TechnologicalRegimStudy r_study,
            TechnologicalRegimeWeightHook r_weight)
        {
            if (slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (float.IsNaN(v8.Value))
                    {
                        _value = Техпроцесс_Default;

                        tech_stage = string.Empty;
                        tech_regime = string.Empty;

                        tech_hook = string.Empty;

                        return;
                    }
                    else
                        if (float.IsNaN(v8.Value))
                        {
                            _value = Техпроцесс_Default;

                            tech_stage = string.Empty;
                            tech_regime = string.Empty;

                            tech_hook = string.Empty;

                            return;
                        }
                        else
                            if (float.IsNaN(v9.Value))
                            {
                                _value = Техпроцесс_Default;

                                tech_stage = string.Empty;
                                tech_regime = string.Empty;

                                tech_hook = string.Empty;

                                return;
                            }

                    TProcResult result = WeightHookForWeightOrWedges(v3, v2, locking_weight_hook, r_weight);
                    switch (result)
                    {
                        case TProcResult.True:

                            _value = TrueBranch(v1, v2, v3, v4, v5, v6, v8, v9, currentTime,
                                locking_weight_hook, interval_pzr, drilling_interval, size_bottom_hole_zone,
                                locking_pressure, locking_value_rotor_speed, locking_value_load, locking_speed_talbloka,
                                r_drilling, r_study, r_weight);

                            break;

                        case TProcResult.False:

                            float tLp = v9.Value - drilling_interval;
                            float Lp = (interval_pzr >= tLp) ? tLp : interval_pzr;

                            // Пустой крюк
                            if (v8.Value > Lp) // Dd>Lp
                            {
                                if (v8.Value <= (v9.Value - drilling_interval)) // Dd<Cd-Id
                                {
                                    // СПО
                                    // СПО

                                    tech_stage = "СПО";
                                    tech_regime = "СПО";

                                    tech_hook = "Пустой крюк";
                                    _value = ПустойКрюк_СПО_СПО;
                                }
                                else
                                {
                                    // Бурение
                                    //Наращивание

                                    tech_stage = "Бурение";
                                    tech_regime = "Наращивание";

                                    tech_hook = "Пустой крюк";
                                    _value = ПустойКрюк_Бурение_Наращивание;
                                }
                            }
                            else
                            {
                                //ПЗР
                                //ПЗР
                                tech_stage = "ПЗР";
                                tech_regime = "ПЗР";

                                tech_hook = "Пустой крюк";
                                _value = ПустойКрюк_ПЗР_ПЗР;
                            }
                            break;

                        case TProcResult.Default:

                            tech_stage = string.Empty;
                            tech_regime = string.Empty;
                            
                            tech_hook = string.Empty;
                            _value = Техпроцесс_Default;

                            break;

                        default:
                            break;
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // -------------------- вспомогательные функции --------------------

        /// <summary>
        /// Вычисляет текущее значение параметра Состояние процесса бурения , если вес на крюке true
        /// </summary>
        /// <param name="v1">Давление в текущий момент</param>
        /// <param name="v2">Клинья в текущий момент</param>
        /// <param name="v3">Вес на крюке в текущий момент</param>
        /// <param name="v4">Скорость тальблока в текущий момент</param>
        /// <param name="v5">Обороты ротора в текущий момент</param>
        /// <param name="v6">Нагрузка на долото в текущий момент</param>
        /// <param name="v8">Положение инструмента в текущий момент</param>
        /// <param name="v9">Глубина забоя в текущий момент</param>
        /// <param name="currentTime">текущее технологическое время</param>
        /// <param name="locking_weight_hook">Блокировочное значение веса на крюке</param>
        /// <param name="interval_pzr">Интервал ПЗР</param>
        /// <param name="drilling_interval">Интервал бурения</param>
        /// <param name="size_bottom_hole_zone">Размер призабойной зоны</param>
        /// <param name="locking_pressure">Блокировочное значение давления</param>
        /// <param name="locking_value_rotor_speed">Блокировочное значение оборотов ротора</param>
        /// <param name="locking_value_load">Блокировочное значение нагрузки</param>
        /// <param name="locking_speed_talbloka">Блокировочное значение скорости тальблока</param>
        /// <param name="r_drilling">Метод расчета режима бурения</param>
        /// <param name="r_study">Метод расчета проработка</param>
        /// <param name="r_weight">Метод расчета технологического режима</param>
        private float TrueBranch(P0004 v1,
            P0012 v2,
            P0102 v3,
            P0103 v4,
            P0110 v5,
            P0201 v6,
            P0204 v8,
            P0205 v9,
            DateTime currentTime,
            float locking_weight_hook,
            float interval_pzr,
            float drilling_interval,
            float size_bottom_hole_zone,
            float locking_pressure,
            float locking_value_rotor_speed,
            float locking_value_load,
            float locking_speed_talbloka,
            TechnologicalRegimDrilling r_drilling,
            TechnologicalRegimStudy r_study,
            TechnologicalRegimeWeightHook r_weight)
        {
            float tLp = v9.Value - drilling_interval;
            float Lp = (interval_pzr >= tLp) ? tLp : interval_pzr;

            // Над забоем
            if (v8.Value > Lp) // Dd>Lp
            {
                if (!float.IsNaN(v1.Value))
                {
                    if (v8.Value <= (v9.Value - drilling_interval)) // Dd<Cd-Id
                    {// СПО
                        if (v1.Value > locking_pressure) // Pr>БЗ
                        {
                            switch (StudySpeedTalblokAndRotationRotor(v5, v4, locking_value_rotor_speed, locking_speed_talbloka, r_study))
                            {
                                case TProcResult.True:

                                    // Проработка

                                    tech_stage = "СПО";
                                    tech_regime = "Проработка";

                                    tech_hook = "Над забоем";
                                    return НадЗабоем_СПО_Проработка;

                                case TProcResult.False:

                                    // Промывка

                                    tech_stage = "СПО";
                                    tech_regime = "Промывка";

                                    tech_hook = "Над забоем";
                                    return НадЗабоем_СПО_Промывка;

                                default:

                                    tech_stage = string.Empty;
                                    tech_regime = string.Empty;

                                    tech_hook = string.Empty;
                                    return Техпроцесс_Default;
                            }
                        }
                        else
                        {
                            //СПО

                            tech_stage = "СПО";
                            tech_regime = "СПО";

                            tech_hook = "Над забоем";
                            return НадЗабоем_СПО_СПО;
                        }
                    }
                    else
                    {
                        // Бурение
                        if (v8.Value <= (v9.Value - size_bottom_hole_zone)) // В Pp?
                        {
                            // Не попали в призабойную зону
                            if (v1.Value > locking_pressure) // Pr>БЗ
                            {
                                switch (StudySpeedTalblokAndRotationRotor(v5, v4, locking_value_rotor_speed, locking_speed_talbloka, r_study))
                                {
                                    case TProcResult.True:

                                        // Проработка

                                        tech_stage = "Бурение";
                                        tech_regime = "Проработка";

                                        tech_hook = "Над забоем";
                                        return НадЗабоем_Бурение_Проработка;

                                    case TProcResult.False:

                                        // Промывка

                                        tech_stage = "Бурение";
                                        tech_regime = "Промывка";

                                        tech_hook = "Над забоем";
                                        return НадЗабоем_Бурение_Промывка;

                                    default:

                                        return Техпроцесс_Default;
                                }
                            }
                            else
                            {
                                //Наращивание

                                tech_stage = "Бурение";
                                tech_regime = "Наращивание";

                                tech_hook = "Над забоем";
                                return НадЗабоем_Бурение_Наращивание;
                            }
                        }
                        else
                        {
                            // Попали в призабойную зону
                            switch (StateDrillingPressureAndLoadBit(v1, v6, locking_pressure, locking_value_load, r_drilling)) // Pr>БЗ
                            {
                                case TProcResult.True:

                                    // Бурение

                                    tech_stage = "Бурение";
                                    tech_regime = "Бурение";

                                    tech_hook = "Над забоем";
                                    return НадЗабоем_Бурение_Бурение;

                                case TProcResult.False:

                                    //Наращивание

                                    tech_stage = "Бурение";
                                    tech_regime = "Наращивание";

                                    tech_hook = "Над забоем";
                                    return НадЗабоем_Бурение_Наращивание;

                                default:

                                    tech_stage = string.Empty;
                                    tech_regime = string.Empty;

                                    tech_hook = string.Empty;
                                    return Техпроцесс_Default;
                            }
                        }
                    }
                }
                else
                {
                    tech_stage = string.Empty;
                    tech_regime = string.Empty;

                    tech_hook = string.Empty;
                    return Техпроцесс_Default;
                }
            }
            else
            {
                //ПЗР
                tech_stage = "ПЗР";
                tech_regime = "ПЗР";

                tech_hook = "Над забоем";
                return НадЗабоем_ПЗР_ПЗР;
            }
        }

        // -----------------------------------------------------------------
    }
}