using System.Drawing;


namespace TagsCloudContainer.RectanglesLayouters
{
    public interface IRectanglesLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
