using System;
using System.Drawing;
using System.Threading;

namespace NumericTable
{
    /// <summary>
    /// Реализует ограничивающий прямоугольник.
    /// Потокобезопаный тип данных.
    /// </summary>
    public class BoundingRectangle
    {
        protected Rectangle rect;                           // прямоугольник
        protected ReaderWriterLockSlim slim;                // синхронизатор

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public BoundingRectangle()
        {
            rect = new Rectangle();
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="location">System.Drawing.Point, представляющий левый верхний угол прямоугольной области.</param>
        /// <param name="size">System.Drawing.Size, представляющий ширину и высоту прямоугольной области.</param>
        public BoundingRectangle(Point location, Size size)
        {
            rect = new Rectangle(location, size);
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="x"> Координата по оси X верхнего левого угла прямоугольника.</param>
        /// <param name="y">Координата по оси Y верхнего левого угла прямоугольника.</param>
        /// <param name="width">Ширина прямоугольника.</param>
        /// <param name="height">Высота прямоугольника.</param>
        public BoundingRectangle(int x, int y, int width, int height)
        {
            rect = new Rectangle(x, y, width, height);
            slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        /// <summary>
        /// Возвращает координату по оси Y, являющуюся суммой значений свойств BoundingRectangle.Y
        /// и BoundingRectangle.Height данной структуры BoundingRectangle.
        /// </summary>
        public int Bottom 
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return rect.Bottom;
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
        /// Возвращает или задает высоту в структуре BoundingRectangle.
        /// </summary>
        public int Height 
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return rect.Height;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        rect.Height = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }
        
        /// <summary>
        /// Проверяет, все ли числовые свойства этого прямоугольника System.Drawing.Rectangle
        /// имеют нулевые значения.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return rect.IsEmpty;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Возвращает координату по оси X левого края структуры System.Drawing.Rectangle. 
        /// </summary>
        public int Left 
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return rect.Left;
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
        /// Возвращает или задает координаты левого верхнего угла структуры BoundingRectangle.
        /// </summary>
        public Point Location
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return rect.Location;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return default(Point);
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        rect.Location = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }
        
        /// <summary>
        /// Возвращает координату по оси X, являющуюся суммой значений свойств BoundingRectangle.X
        /// и BoundingRectangle.Width данной структуры System.Drawing.Rectangle.
        /// </summary>
        public int Right
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return rect.Right;
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
        /// Возвращает или задает размер этого прямоугольника BoundingRectangle.
        /// </summary>
        public Size Size
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {

                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return default(Size);
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        rect.Size = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает координату по оси Y верхнего края структуры BoundingRectangle.
        /// </summary>
        public int Top
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return rect.Top;
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
        /// Возвращает или задает ширину структуры BoundingRectangle.
        /// </summary>
        public int Width
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return rect.Width;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        rect.Width = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает или задает координату по оси X левого верхнего угла структуры
        /// BoundingRectangle.
        /// </summary>
        public int X
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return rect.X;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        rect.X = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает или задает координату по оси Y левого верхнего угла структуры
        /// BoundingRectangle.
        /// </summary>
        public int Y 
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return rect.Y;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return -1;
            }

            set
            {
                if (slim.TryEnterWriteLock(500))
                {
                    try
                    {
                        rect.Y = value;
                    }
                    finally
                    {
                        slim.ExitWriteLock();
                    }
                }
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                if (slim.TryEnterReadLock(300))
                {
                    try
                    {
                        return rect;
                    }
                    finally
                    {
                        slim.ExitReadLock();
                    }
                }

                return default(Rectangle);
            }
        }
    }
}