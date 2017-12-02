using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudObjects;


namespace TagsCloudContainer.CloudDrawers.Stylers
{
    public class StringsStyler : ICloudObjectsStyler<string>
    {
        private readonly Font baseFont;
        private readonly float fontScaleFactor;
        private readonly IEnumerator<Color> colorPalette;

        public StringsStyler(Font baseFont, float fontScaleFactor, IEnumerable<Color> colorPalette)
        {
            this.baseFont = baseFont;
            this.fontScaleFactor = fontScaleFactor;
            this.colorPalette = CyclePalette(colorPalette).GetEnumerator();
        }

        public Style GetStylesFor(ICloudObject<string> cloudObject)
        {
            return new Style(
                GetPenFor(cloudObject),
                GetBrushFor(cloudObject),
                GetFontFor(cloudObject)
            );
        }

        public Font GetFontFor(ICloudObject<string> cloudObject)
        {
            return new Font(
                baseFont.FontFamily,
                baseFont.Size + fontScaleFactor * cloudObject.Weight,
                baseFont.Style
            );
        }

        public Pen GetPenFor(ICloudObject<string> cloudObject)
        {
            colorPalette.MoveNext();
            return new Pen(colorPalette.Current);
        }

        public Brush GetBrushFor(ICloudObject<string> cloudObject)
        {
            return GetPenFor(cloudObject).Brush;
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
