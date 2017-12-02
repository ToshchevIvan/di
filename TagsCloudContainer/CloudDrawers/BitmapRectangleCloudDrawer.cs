using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudObjects;


namespace TagsCloudContainer.CloudDrawers
{
    // Класс ещё не адаптирован под новые интерфейсы, т.к. нужно будет переписать тесты
    // Но код конфигурирования выходит довольно объёмным. Как и куда его можно вынести?
    public class BitmapRectangleCloudDrawer : ICloudDrawer<Rectangle>
    {
        public Image Canvas => bitmap;
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private readonly Pen pen = new Pen(Color.ForestGreen, 2);
        private bool disposed;

        public BitmapRectangleCloudDrawer(Size canvasSize)
        {
            bitmap = new Bitmap(canvasSize.Width, canvasSize.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(Brushes.Bisque, 
                new Rectangle(0, 0, canvasSize.Width, canvasSize.Height));
        }

        public BitmapRectangleCloudDrawer PaintRectangles(IEnumerable<Rectangle> rectangles)
        {
            CheckDisposed();
            graphics.DrawRectangles(pen, rectangles.ToArray());
            
            return this;
        }

        public BitmapRectangleCloudDrawer SaveToFile(string filePath)
        {
            CheckDisposed();
            bitmap.Save(filePath);
            
            return this;
        }

        public void DrawAt(Point location, ICloudObject<Rectangle> cloudObject)
        {
            graphics.DrawRectangle(pen, cloudObject.Value);
        }

        public Size GetSizeOf(ICloudObject<Rectangle> cloudObject)
        {
            return cloudObject.Value.Size;
        }
        
        private void CheckDisposed() 
        {
            if(disposed) 
                throw new ObjectDisposedException("Object has been already disposed");
        }
        
        public void Dispose()
        {
            disposed = true;
            bitmap.Dispose();
            graphics.Dispose();
            pen.Dispose();
        }
    }
}
