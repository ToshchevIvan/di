using System;
using System.Collections.Generic;


namespace TagsCloudContainer.Statisticians
{
    public class StringCountStatistic : IStatistician
    {
        private int maxWeight = int.MaxValue;

        public StringCountStatistic()
        {
        }

        public StringCountStatistic(int maxWeight)
        {
            this.maxWeight = maxWeight;
        }

        public IDictionary<string, int> GetStatistic(IEnumerable<string> values)
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
