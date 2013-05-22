using System;
using System.Threading;
using System.Collections.Generic;

namespace NumericTable
{
    /// <summary>
    /// Реализует панель цифрового табло
    /// </summary>
    public partial class Panel
    {
        public PanelItem[] Items
        {
            get
            {
                if (i_slim.TryEnterReadLock(500))
                {
                    try
                    {
                        return items.ToArray();
                    }
                    finally
                    {
                        i_slim.ExitReadLock();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Добавить параметр для отображения
        /// </summary>
        /// <param name="item">Добавляемый параметр для отображения</param>
        public void InsertItem(PanelItem item)
        {
            if (i_slim.TryEnterWriteLock(300))
            {
                try
                {
                    items.Add(item);
                }
                finally
                {
                    i_slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Удалить отображаемый параметр
        /// </summary>
        /// <param name="item">Удаляемый параметр</param>
        public void RemoveItem(PanelItem item)
        {
            if (i_slim.TryEnterWriteLock(300))
            {
                try
                {
                    items.Remove(item);
                }
                finally
                {
                    i_slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Очистить список отображаемых элементов
        /// </summary>
        public void ClearItems()
        {
            if (i_slim.TryEnterWriteLock(300))
            {
                try
                {
                    items.Clear();
                }
                finally
                {
                    i_slim.ExitWriteLock();
                }
            }
        }
    }
}