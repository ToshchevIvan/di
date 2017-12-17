using System.Collections.Generic;


namespace TagsCloudContainer.Filters
{
    public interface IFilter
    {
        Result<IEnumerable<string>> Filter(IEnumerable<string> values);
    }
}
