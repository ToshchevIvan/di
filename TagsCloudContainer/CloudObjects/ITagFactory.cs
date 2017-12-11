using System.Drawing;
using TagsCloudContainer.Stylers;


namespace TagsCloudContainer.CloudObjects
{
    public interface ITagFactory
    {
        ITag Create(string value, Style style, Point location);
    }
}
