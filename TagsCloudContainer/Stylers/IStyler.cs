using System.Collections.Generic;


namespace TagsCloudContainer.Stylers
{
    public interface IStyler
    {
        IDictionary<string, Style> GetStyles(IDictionary<string, int> weightedStrings);
    }
}
