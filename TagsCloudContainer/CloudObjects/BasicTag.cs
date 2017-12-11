using System.Drawing;
using TagsCloudContainer.Stylers;


namespace TagsCloudContainer.CloudObjects
{
    public class BasicTag : ITag
    {
        public string Value { get; }
        public Style Style { get; }
        public Point Location { get; }

        public BasicTag(string value, Style style, Point location)
        {
            Value = value;
            Style = style;
            Location = location;
        }
    }
}
