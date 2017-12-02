using System;
using System.Drawing;


namespace TagsCloudContainer.CloudDrawers.Stylers
{
    public class Style : IDisposable
    {
        public Pen Pen { get; }
        public Brush Brush { get; }
        public Font Font { get; }

        public Style(Pen pen, Brush brush, Font font)
        {
            Pen = pen;
            Brush = brush;
            Font = font;
        }

        public void Dispose()
        {
            Pen?.Dispose();
            Brush?.Dispose();
            Font?.Dispose();
        }
    }
}
