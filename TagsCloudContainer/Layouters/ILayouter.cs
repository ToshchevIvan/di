using System.Collections.Generic;
using TagsCloudContainer.CloudObjects;
using TagsCloudContainer.Stylers;


namespace TagsCloudContainer.Layouters
{
    public interface ILayouter
    {
        IEnumerable<ITag> GetLayout(IDictionary<string, Style> styledStrings);
    }
}
