using System.Drawing;
using TagsCloudContainer.CloudDrawers;
using TagsCloudContainer.RectanglesLayouters;


namespace RectangleCloudlayouterTests
{
    public static class ExampleLayoutGenerator
    {
        private static void GenerateLayoutAndSaveToFile(string filePath, Point center, Size canvasSize, int rectCount)
        {
            var layouter = new SpiralRectanglesLayouter(center);
            var placedRectangles = RandomEntitiesFabric.GetRandomLayout(layouter, rectCount);
            using (var painter = new BitmapRectangleCloudDrawer(canvasSize))
            {
                painter.PaintRectangles(placedRectangles)
                    .SaveToFile(filePath);
            }
        }
        
        public static void Main()
        {
            GenerateLayoutAndSaveToFile("example1.bmp", new Point(1500, 1500), new Size(3000, 3000), 200);
            GenerateLayoutAndSaveToFile("example2.bmp", new Point(1000, 1000), new Size(2000, 2000), 100);
            GenerateLayoutAndSaveToFile("example3.bmp", new Point(600, 600), new Size(1200, 1200), 30);
        }
    }
}
