using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;


namespace TagsCloudConsole.Options
{
    public class CloudOptions
    {
        public string InputFile { get; }
        public string OutputFile { get; }
        public InputFormat InputFormat { get; }
        public ImageFormat OutputFormat { get; }
        public Point Center { get; }
        public Size CanvasSize { get; }
        public IEnumerable<Color> ColorPalette { get; }
        public float FontEmSize { get; }
        public FontFamily FontFamily { get; }
        public int MaxCount { get; }
        public int MaxWeight { get; }
        public string[] StopWords { get; }

        public CloudOptions(string inputFile, string outputFile, InputFormat inputFormat, ImageFormat outputFormat, 
            Point center, Size canvasSize, IEnumerable<Color> colorPalette, float fontEmSize, FontFamily fontFamily, 
            int maxCount, int maxWeight, string[] stopWords)
        {
            InputFile = inputFile;
            OutputFile = outputFile;
            InputFormat = inputFormat;
            OutputFormat = outputFormat;
            Center = center;
            CanvasSize = canvasSize;
            ColorPalette = colorPalette;
            FontEmSize = fontEmSize;
            FontFamily = fontFamily;
            MaxCount = maxCount;
            MaxWeight = maxWeight;
            StopWords = stopWords;
        }
    }
}
