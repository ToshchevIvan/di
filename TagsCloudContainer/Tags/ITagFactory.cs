using System.Drawing;
using TagsCloudContainer.Stylers;


namespace TagsCloudContainer.Tags
{
    public interface ITagFactory
    {
        ITag Create(string value, Style style, Point location);
    }
}
