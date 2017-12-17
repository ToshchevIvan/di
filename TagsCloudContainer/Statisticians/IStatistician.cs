using System.Collections.Generic;


namespace TagsCloudContainer.Statisticians
{
    public interface IStatistician
    {
        Result<IDictionary<string, int>> GetStatistic(IEnumerable<string> values);
    }
}
