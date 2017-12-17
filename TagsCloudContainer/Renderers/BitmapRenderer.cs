using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using TagsCloudContainer.Tags;


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

        public Result<Bitmap> Render(IEnumerable<ITag> tags)
        {
            foreach (var tag in tags)
            {
                if (!IsInBounds(tag))
                    return Result.Fail<Bitmap>("Tag doesn't fit onto canvas");
                graphics.DrawString(tag.Value, tag.Style.Font,
                    tag.Style.Brush, tag.Location);
            }
                
            return Result.Ok(canvas);
        }

        private bool IsInBounds(ITag tag)
        {
            var origin = new Point(0, 0);
            var rightBottomCorner = new Point(canvas.Width, canvas.Height);
            var tagCorner = tag.Location + tag.Style.Size;
            return origin.LessThan(tag.Location) && tag.Location.LessThan(rightBottomCorner) &&
                   tagCorner.LessThan(rightBottomCorner);
        }

        public void Dispose()
        {
            graphics?.Dispose();
        }
    }

    internal static class PointExtensions
    {
        internal static bool LessThan(this Point a, Point b) =>
            a.X < b.X && a.Y < b.Y;
    }
}
