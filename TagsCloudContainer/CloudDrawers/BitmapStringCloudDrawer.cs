using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.CloudDrawers.Stylers;
using TagsCloudContainer.CloudObjects;


namespace TagsCloudContainer.CloudDrawers
{
    public class BitmapStringCloudDrawer : ICloudDrawer<string>
    {
        public Image Canvas { get; }
        private readonly ICloudObjectsStyler<string> styler;
        private readonly Graphics graphics;

        public BitmapStringCloudDrawer(Image canvas, ICloudObjectsStyler<string> styler)
        {
            this.styler = styler;
            Canvas = canvas;
            graphics = Graphics.FromImage(canvas);
        }

        public void DrawAt(Point location, ICloudObject<string> cloudObject)
        {
            var styles = styler.GetStylesFor(cloudObject);
            graphics.DrawString(cloudObject.Value, styles.Font, styles.Brush, location);
        }

        public Size GetSizeOf(ICloudObject<string> cloudObject)
        {
            // MeasureString крайне неточно измеряет размеры
            // Отсюда большие дыры в облаке...
            return graphics.MeasureString(
                cloudObject.Value, styler.GetFontFor(cloudObject)
            ).ToSize();
        }

        public void Dispose()
        {
            graphics?.Dispose();
        }
    }
}
