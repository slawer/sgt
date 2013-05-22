using System;

namespace SGT
{
    /// <summary>
    /// Определяет интерфейс вспомогательных технологических функция
    /// </summary>
    internal interface ITechParameter
    {
        /// <summary>
        /// Определяет состояние Вес на крюке по весу.
        /// </summary>
        /// <param name="v1">Вес на крюке в текущий момент</param>
        /// <param name="locking_weight_hook">Блокировочное значение веса на крюке</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        TProcResult WeightHookForWeight(P0102 v1, float locking_weight_hook);

        /// <summary>
        /// Определяет состояние Вес на крюке по клиньям 
        /// </summary>
        /// <param name="v1">Положение клиньев в текущий момент</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        TProcResult WeightHookForWedges(P0012 v1);

        /// <summary>
        /// Определяет состояние Вес на крюке по весу или клиньям
        /// </summary>
        /// <param name="v1">Вес на крюке в текущий момент</param>
        /// <param name="v2">Положение клиньев в текущий момент</param>
        /// <param name="locking_weight_hook">Блокировочное значение веса на крюке. Глобальный параметр для всей технологии, по умолчанию == 0</param>
        /// <param name="r_weight">Определять состояние по весу или клиньям</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        TProcResult WeightHookForWeightOrWedges(P0102 v1, P0012 v2, float locking_weight_hook, TechnologicalRegimeWeightHook r_weight);

        /// <summary>
        /// Определяет состояние "Проработка" по вращению ротора
        /// </summary>
        /// <param name="v1">Обороты ротора в текущий момент</param>
        /// <param name="locking_value_rotor_speed">Блокировочное значение оборотов ротора</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        TProcResult StudyRotationRotor(P0110 v1, float locking_value_rotor_speed);

        /// <summary>
        /// Определяет состояние "Проработка" по скорости тальблока 
        /// </summary>
        /// <param name="v1">Скорость тальблока в текущий момент</param>
        /// <param name="locking_speed_talbloka">Блокировочное значение скорости тальблока</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        TProcResult StudySpeedTalblok(P0103 v1, float locking_speed_talbloka);

        /// <summary>
        /// Определяет состояние "Проработка" по параметрам Обороты ротора и Скорость тальблока
        /// </summary>
        /// <param name="v1">Обороты ротора в текущий момент</param>
        /// <param name="v2">Скорость тальблока в текущий момент</param>
        /// <param name="locking_value_rotor_speed">Блокировочное значение оборотов ротора.</param>
        /// <param name="locking_speed_talbloka">Блокировочное значение скорости тальблока.</param>
        /// <param name="r_study">Определяет метод расчета проработка</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        TProcResult StudySpeedTalblokAndRotationRotor(P0110 v1, P0103 v2, float locking_value_rotor_speed,
            float locking_speed_talbloka, TechnologicalRegimStudy r_study);

        /// <summary>
        /// Определят состояние "Бурение" по давлению в скважине
        /// </summary>
        /// <param name="v1">Давление в текущий момент</param>
        /// <param name="locking_pressure">Блокировочное значение давления</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        TProcResult StateDrillingPressureInWell(P0004 v1, float locking_pressure);

        /// <summary>
        /// Определят состояние "Бурение" по нагрузке на долото
        /// </summary>
        /// <param name="v1">Нагрузка на долото в текущий момент</param>
        /// <param name="locking_value_load">Блокировочное значение нагрузки</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        TProcResult StateDrillingLoadOnBit(P0201 v1, float locking_value_load);

        /// <summary>
        /// Определяет состояние "Бурение" по параметрам давление и Нагрузка на долото
        /// </summary>
        /// <param name="v1">Давление в текущий момент</param>
        /// <param name="v2">Нагрузка на долото в текущий момент</param>
        /// <param name="locking_pressure">Блокировочное значение давления.</param>
        /// <param name="locking_value_load">Блокировочное значение нагрузки.</param>
        /// <param name="r_drilling">Определяет метод расчета режима бурения</param>
        /// <returns>Возвращает одно из трёх состояний: есть, нету, неизвестно</returns>
        TProcResult StateDrillingPressureAndLoadBit(P0004 v1, P0201 v2, float locking_pressure,
            float locking_value_load, TechnologicalRegimDrilling r_drilling);
    }        
}