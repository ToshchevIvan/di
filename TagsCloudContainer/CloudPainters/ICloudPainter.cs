using System;
using System.Drawing;


namespace TagsCloudContainer.CloudPainters
{
    public interface ICloudPainter : IDisposable
    {
        Bitmap Bitmap { get; }
        Graphics Graphics { get; }
        Color Color { get; }
        Font Font { get; }

        void PaintWord(string word, Font font);
        void SaveToFile(string filePath);
    }
}
