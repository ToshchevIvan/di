using System;
using System.Collections.Generic;


namespace TagsCloudContainer.Statisticians
{
    public class StringCountStatistic : IStatistician
    {
        private readonly int maxWeight;

        public StringCountStatistic(int maxWeight = int.MaxValue)
        {
            this.maxWeight = maxWeight;
        }

        public Result<IDictionary<string, int>> GetStatistic(IEnumerable<string> values)
        {
            var stats = new Dictionary<string, int>();
            foreach (var value in values)
            {
                stats.TryGetValue(value, out var weight);
                if (weight < maxWeight)
                    weight += 1;
                stats[value] = weight;
            }

            return stats;
        }
    }
}
