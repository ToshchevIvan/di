using System.Collections.Generic;
using TagsCloudContainer.Stylers;
using TagsCloudContainer.Tags;


namespace TagsCloudContainer.Layouters
{
    public interface ILayouter
    {
        Result<IEnumerable<ITag>> GetLayout(IDictionary<string, Style> styledStrings);
    }
}
