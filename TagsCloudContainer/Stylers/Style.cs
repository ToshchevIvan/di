using System;
using System.Drawing;


namespace TagsCloudContainer.Stylers
{
    public class Style : IDisposable
    {
        public Brush Brush { get; }
        public Font Font { get; }
        public Size Size { get; }

        public Style(Brush brush, Font font, Size size)
        {
            Brush = brush;
            Font = font;
            Size = size;
        }

        public void Dispose()
        {
            Brush?.Dispose();
            Font?.Dispose();
        }
    }
}
