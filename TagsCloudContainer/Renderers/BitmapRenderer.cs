using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.CloudObjects;


namespace TagsCloudContainer.Renderers
{
    public class BitmapRenderer : IRenderer<Bitmap>
    {
        private readonly Bitmap canvas;
        private readonly Graphics graphics;

        public BitmapRenderer(Bitmap canvas)
        {
            this.canvas = canvas;
            graphics = Graphics.FromImage(canvas);
        }

        public Bitmap Render(IEnumerable<ITag> tags)
        {
            foreach (var tag in tags)
                graphics.DrawString(tag.Value, tag.Style.Font,
                    tag.Style.Brush, tag.Location);
            return canvas;
        }

        public void Dispose()
        {
            graphics?.Dispose();
        }
    }
}
