using System;

namespace SGT
{
    /// <summary>
    /// Реализует технологический параметр
    /// </summary>
    public partial class TParameter
    {
        /// <summary>
        /// Определяет состояние Вес на крюке по весу.
        /// </summary>
        /// <param name="v1">Вес на крюке в текущий момент</param>
        /// <param name="locking_weight_hook">Блокировочное значение веса на крюке</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        public TProcResult WeightHookForWeight(P0102 v1, float locking_weight_hook)
        {
            if (!float.IsNaN(v1.Value))
            {
                if (v1.Value > locking_weight_hook)
                {
                    return TProcResult.True;
                }
                else
                    return TProcResult.False;
            }

            return TProcResult.Default;
        }

        /// <summary>
        /// Определяет состояние Вес на крюке по клиньям 
        /// </summary>
        /// <param name="v1">Положение клиньев в текущий момент</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        public TProcResult WeightHookForWedges(P0012 v1)
        {
            if (!float.IsNaN(v1.Value))
            {
                int _kl = (int)Math.Round(v1.Value);
                if (_kl == 0)
                {
                    return TProcResult.True;
                }
                else
                    return TProcResult.False;
            }

            return TProcResult.Default;
        }

        /// <summary>
        /// Определяет состояние Вес на крюке по весу или клиньям
        /// </summary>
        /// <param name="v1">Вес на крюке в текущий момент</param>
        /// <param name="v2">Положение клиньев в текущий момент</param>
        /// <param name="locking_weight_hook">Блокировочное значение веса на крюке. Глобальный параметр для всей технологии, по умолчанию == 0</param>
        /// <param name="r_weight">Определять состояние по весу или клиньям</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        public TProcResult WeightHookForWeightOrWedges(P0102 v1, P0012 v2, float locking_weight_hook, 
            TechnologicalRegimeWeightHook r_weight)
        {
            switch (r_weight)
            {
                case TechnologicalRegimeWeightHook.Wedges:

                    return WeightHookForWedges(v2);

                case TechnologicalRegimeWeightHook.Weight:

                    return WeightHookForWeight(v1, locking_weight_hook);

                case TechnologicalRegimeWeightHook.WeightOrWedges:

                    if (WeightHookForWeight(v1, locking_weight_hook) != TProcResult.True)
                    {
                        return WeightHookForWedges(v2);
                    }
                    else
                        return TProcResult.True;

                default:
                    break;
            }

            return TProcResult.Default;
        }

        /// <summary>
        /// Определяет состояние "Проработка" по вращению ротора
        /// </summary>
        /// <param name="v1">Обороты ротора в текущий момент</param>
        /// <param name="locking_value_rotor_speed">Блокировочное значение оборотов ротора</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        public TProcResult StudyRotationRotor(P0110 v1, float locking_value_rotor_speed)
        {
            if (!float.IsNaN(v1.Value))
            {
                if (v1.Value > locking_value_rotor_speed)
                {
                    return TProcResult.True;
                }
                else
                    return TProcResult.False;
            }

            return TProcResult.Default;
        }

        /// <summary>
        /// Определяет состояние "Проработка" по скорости тальблока 
        /// </summary>
        /// <param name="v1">Скорость тальблока в текущий момент</param>
        /// <param name="locking_speed_talbloka">Блокировочное значение скорости тальблока</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        public TProcResult StudySpeedTalblok(P0103 v1, float locking_speed_talbloka)
        {
            if (!float.IsNaN(v1.Value))
            {
                if (Math.Abs(v1.Value) > locking_speed_talbloka)
                {
                    return TProcResult.True;
                }
                else
                    return TProcResult.False;
            }

            return TProcResult.Default;
        }

        /// <summary>
        /// Определяет состояние "Проработка" по параметрам Обороты ротора и Скорость тальблока
        /// </summary>
        /// <param name="v1">Обороты ротора в текущий момент</param>
        /// <param name="v2">Скорость тальблока в текущий момент</param>
        /// <param name="locking_value_rotor_speed">Блокировочное значение оборотов ротора.</param>
        /// <param name="locking_speed_talbloka">Блокировочное значение скорости тальблока.</param>
        /// <param name="r_study">Определяет метод расчета проработка</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        public TProcResult StudySpeedTalblokAndRotationRotor(P0110 v1, P0103 v2, float locking_value_rotor_speed,
            float locking_speed_talbloka, TechnologicalRegimStudy r_study)
        {
            switch (r_study)
            {
                case TechnologicalRegimStudy.RotorSpeed:

                    return StudyRotationRotor(v1, locking_value_rotor_speed);

                case TechnologicalRegimStudy.SpeedTalblok:

                    return StudySpeedTalblok(v2, locking_speed_talbloka);

                case TechnologicalRegimStudy.RotorSpeenOrSpeedTalblok:

                    if (StudyRotationRotor(v1, locking_value_rotor_speed) != TProcResult.True)
                    {
                        return StudySpeedTalblok(v2, locking_speed_talbloka);
                    }
                    else
                        return TProcResult.True;

                default:
                    break;
            }

            return TProcResult.Default;
        }

        /// <summary>
        /// Определят состояние "Бурение" по давлению в скважине
        /// </summary>
        /// <param name="v1">Давление в текущий момент</param>
        /// <param name="locking_pressure">Блокировочное значение давления</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        public TProcResult StateDrillingPressureInWell(P0004 v1, float locking_pressure)
        {
            if (!float.IsNaN(v1.Value))
            {
                if (v1.Value > locking_pressure)
                {
                    return TProcResult.True;
                }
                else
                    return TProcResult.False;
            }

            return TProcResult.Default;
        }

        /// <summary>
        /// Определят состояние "Бурение" по нагрузке на долото
        /// </summary>
        /// <param name="v1">Нагрузка на долото в текущий момент</param>
        /// <param name="locking_value_load">Блокировочное значение нагрузки</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        public TProcResult StateDrillingLoadOnBit(P0201 v1, float locking_value_load)
        {
            if (!float.IsNaN(v1.Value))
            {
                if (v1.Value > locking_value_load)
                {
                    return TProcResult.True;
                }
                else
                    return TProcResult.False;
            }

            return TProcResult.Default;
        }

        /// <summary>
        /// Определяет состояние "Бурение" по параметрам давление и Нагрузка на долото
        /// </summary>
        /// <param name="v1">Давление в текущий момент</param>
        /// <param name="v2">Нагрузка на долото в текущий момент</param>
        /// <param name="locking_pressure">Блокировочное значение давления.</param>
        /// <param name="locking_value_load">Блокировочное значение нагрузки.</param>
        /// <param name="r_drilling">Определяет метод расчета режима бурения</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        public TProcResult StateDrillingPressureAndLoadBit(P0004 v1, P0201 v2, float locking_pressure,
            float locking_value_load, TechnologicalRegimDrilling r_drilling)
        {
            switch (r_drilling)
            {
                case TechnologicalRegimDrilling.Pressure:

                    return StateDrillingPressureInWell(v1, locking_pressure);

                case TechnologicalRegimDrilling.PressureAndLoad:

                    TProcResult result =  StateDrillingPressureInWell(v1, locking_pressure);
                    if (result == TProcResult.True)
                    {
                        return StateDrillingLoadOnBit(v2, locking_value_load);
                    }
                    else
                        return result;

                default:
                    break;
            }

            return TProcResult.Default;
        }
    }
}