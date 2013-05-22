using System;
using System.Threading;
using System.Collections;

namespace DataBase
{
    /// <summary>
    /// Реализует список загруженных параметров БД.
    /// Потокобезопасный, импользуется для синхронизации объек System.Mutex.
    /// </summary>
    public class DataBaseParameters : ICollection, IEnumerable
    {
        // ---- данные класса ----

        private int count = 0;                          // количество добавленных элементов в коллекцию

        private DataBaseParameter[] parameters;         // параметры конвеера
        private ReaderWriterLockSlim slim = null;       // синхронизует доступ к коллекции

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="count">Количество параметров</param>
        public DataBaseParameters(int count)
        {            
            parameters = new DataBaseParameter[count];
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Возвращает перечислитель, который осуществляет перебор элементов коллекции.
        /// </summary>
        /// <returns>
        /// Объект System.Collections.IEnumerator, который может использоваться для перебора
        /// элементов коллекции.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DB_PEnumerator(parameters);
        }

        /// <summary>
        /// Возвращает число элементов, содержащихся в коллекции.
        /// </summary>
        public int Count
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return count;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return -1;
            }
        }

        /// <summary>
        /// Значение true, если доступ к классу System.Collections.ICollection является
        /// синхронизированным (потокобезопасным), в противном случае — false.
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Объект, который может использоваться для синхронизации доступа к коллекции.
        /// </summary>
        public object SyncRoot
        {
            get
            {
                return slim;
            }
        }

        /// <summary>
        /// Копирует элементы коллекции в массив System.Array,
        /// начиная с указанного индекса массива System.Array.
        /// </summary>
        /// <param name="array">
        /// Одномерный массив System.Array, в который копируются элементы из коллекции.
        /// Индексация в массиве System.Array должна начинаться с нуля.
        /// </param>
        /// <param name="index">
        /// Значение индекса в массиве array, с которого начинается копирование.
        /// </param>
        /// <exception cref="System.ArgumentNullException">Значение параметра array — null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Значение параметра index меньше нуля.</exception>
        /// <exception cref="System.ArgumentException">
        /// Массив array является многомерным.-или- Значение индекса массива index больше
        /// или равно длине массива array.-или- Количество элементов в исходной коллекции
        /// превышает размер доступного места, начиная с индекса массива index и до конца массива назначения array.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        //  Тип исходной коллекции System.Collections.ICollection нельзя автоматически
        //  привести к типу массива назначения array.
        /// </exception>
        public void CopyTo(Array array, int index)
        {
            if (slim.TryEnterReadLock(300))
            {
                try
                {
                    try
                    {
                        parameters.CopyTo(array, index);
                    }
                    catch (ArgumentNullException)
                    {
                        throw new ArgumentNullException();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    catch (ArgumentException)
                    {
                        throw new ArgumentException();
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        throw new ArrayTypeMismatchException();
                    }
                    catch (RankException)
                    {
                        throw new RankException();
                    }
                    catch (InvalidCastException)
                    {
                        throw new InvalidCastException();
                    }
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }
            else
                throw new TimeoutException();
        }

        // ---- расширяющие методы ----

        /// <summary>
        /// Добавить параметр в конец списка
        /// </summary>
        /// <param name="parameter">Добавляемый параметр</param>
        public void Insert(DataBaseParameter parameter)
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (count < parameters.Length)
                    {
                        for (int index = 0; index < parameters.Length; index++)
                        {
                            if (parameters[index] == null)
                            {
                                count++;
                                parameters[index] = parameter;

                                break;
                            }
                        }
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
            else
                throw new TimeoutException();
        }

        /// <summary>
        /// Удалить параметр из списка
        /// </summary>
        /// <param name="parameter">Удаляемый параметр</param>
        public void Remove(DataBaseParameter parameter)
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (count > 0)
                    {
                        for (int index = 0; index < parameters.Length; index++)
                        {
                            if (parameters[index] == parameter)
                            {
                                count = count - 1;
                                parameters[index] = null;

                                CorrectArray();
                                break;
                            }
                        }
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Удалить параметр из списка
        /// </summary>
        /// <param name="Identifier">Идентификатор удаляемого параметра</param>
        public void Remove(Guid Identifier)
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (count > 0)
                    {
                        for (int index = 0; index < parameters.Length; index++)
                        {
                            if (parameters[index].Identifier == Identifier)
                            {
                                count = count - 1;
                                parameters[index] = null;

                                CorrectArray();
                                break;
                            }
                        }
                    }
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Удалить параметр с указанным индексом
        /// </summary>
        /// <param name="index">Индекс удаляемого параметра</param>
        public void RemoveAt(int index)
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (index > -1 && index < parameters.Length)
                    {
                        if (count > 0)
                        {
                            parameters[index] = null;
                        }
                    }
                    else
                        throw new IndexOutOfRangeException();
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Очистить список
        /// </summary>
        public void Clear()
        {
            if (slim.TryEnterWriteLock(500))
            {
                try
                {
                    for (int index = 0; index < parameters.Length; index++)
                    {
                        parameters[index] = null;
                    }
                    count = 0;

                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
        }

        // ---- методы поддержки ----

        /// <summary>
        /// Выполняет корректировку массива
        /// </summary>
        private void CorrectArray()
        {
            for (int i = 0; i < parameters.Length - 2; i++)
            {
                for (int j = i; j < parameters.Length - 2; j++)
                {
                    if (parameters[i] == null)
                    {
                        parameters[i] = parameters[i + 1];
                        parameters[i + 1] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Получить параметр по его идентификатору
        /// </summary>
        /// <param name="Identifier">Идентификатор параметра</param>
        /// <returns>Параметр с указанным идентификатором</returns>
        public DataBaseParameter GetParameter(Guid Identifier)
        {
            if (slim.TryEnterReadLock(300))
            {
                try
                {
                    for (int index = 0; index < parameters.Length; index++)
                    {
                        if (parameters[index] != null)
                        {
                            if (parameters[index].Identifier == Identifier)
                            {
                                return parameters[index];
                            }
                        }
                        else
                            break;
                    }
                }
                finally
                {
                    slim.ExitReadLock();
                }
            }

            return null;
        }

        /// <summary>
        /// Инициализировать мьютекс
        /// </summary>
        internal void InitMutex()
        {
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }
    }

    /// <summary>
    /// Реализует перечислитель параметров БД
    /// </summary>
    internal class DB_PEnumerator : IEnumerator
    {
        // ---- данные класса ----

        private int position = -1;          // позиция в массиве при переборе коллекции
        private DataBaseParameter[] parameters;     // массив параметров

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="list">Список параметров для которого создается перечислитель</param>
        internal DB_PEnumerator(DataBaseParameter[] list)
        {
            parameters = list;
        }

        /// <summary>
        ///  Перемещает перечислитель к следующему элементу коллекции 
        /// </summary>
        /// <returns>
        /// Значение true, если перечислитель был успешно перемещен к следующему элементу;
        /// значение false, если перечислитель достиг конца коллекции.
        /// </returns>
        public bool MoveNext()
        {
            position++;
            if (parameters[position] != null) return true;
            else
                return false;
        }

        /// <summary>
        /// Устанавливает перечислитель в его начальное положение, перед первым элементом
        /// коллекции.
        /// </summary>
        public void Reset()
        {
            position = -1;
        }

        /// <summary>
        /// Получает текущий элемент в коллекции.
        /// </summary>
        public object Current
        {
            get
            {
                try
                {
                    return parameters[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}