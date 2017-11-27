using System.Drawing;


namespace TagsCloudContainer.RectangleLayouters
{
    public interface IRectangleLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
