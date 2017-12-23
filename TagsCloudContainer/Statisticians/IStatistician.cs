using System.Collections.Generic;


namespace TagsCloudContainer.Statisticians
{
    public interface IStatistician
    {
        IDictionary<string, int> GetStatistic(IEnumerable<string> values);
    }
}
