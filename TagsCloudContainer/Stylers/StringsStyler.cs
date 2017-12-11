using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudObjects;


namespace TagsCloudContainer.Stylers
{
    public class StringsStyler : IStyler
    {
        private readonly Font baseFont;
        private readonly float fontScaleFactor;
        private readonly IEnumerator<Color> colorPalette;
        private readonly Graphics graphics;

        public StringsStyler(Font baseFont, float fontScaleFactor, IEnumerable<Color> colorPalette)
        {
            this.baseFont = baseFont;
            this.fontScaleFactor = fontScaleFactor;
            this.colorPalette = CyclePalette(colorPalette).GetEnumerator();
            graphics = Graphics.FromImage(new Bitmap(1, 1));
        }

        public IDictionary<string, Style> GetStyles(IDictionary<string, int> weightedStrings)
        {
            var styles = new Dictionary<string, Style>();
            foreach (var item in weightedStrings)
            {
                var font = GetFont(item.Value);
                styles[item.Key] = new Style(
                    GetBrush(), font, GetSizeOf(item.Key, font)
                );
            }
            return styles;
        }

        private Font GetFont(int weight)
        {
            return new Font(
                baseFont.FontFamily,
                baseFont.Size + fontScaleFactor * weight,
                baseFont.Style
            );
        }

        private Brush GetBrush()
        {
            colorPalette.MoveNext();
            return new SolidBrush(colorPalette.Current);
        }

        private Size GetSizeOf(string value, Font font)
        {
            return graphics.MeasureString(value, font)
                .ToSize();
        }

        private static IEnumerable<Color> CyclePalette(IEnumerable<Color> palette)
        {
            var paletteArray = palette.ToArray();
            while (true)
                foreach (var color in paletteArray)
                    yield return color;
        }
    }
}
