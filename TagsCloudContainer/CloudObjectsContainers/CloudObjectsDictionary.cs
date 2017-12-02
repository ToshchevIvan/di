using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.CloudObjects;


namespace TagsCloudContainer.CloudObjectsContainers
{
    public class CloudObjectsDictionary<TValue> : ICloudObjectContainer<TValue> 
    {
        public int Count => items.Count;
        private readonly IDictionary<TValue, ICloudObject<TValue>> items;
        private readonly ICloudObjectFabric<TValue> fabric;
        private int maxCount = int.MaxValue;        
        private int maxWeight = int.MaxValue;        

        public CloudObjectsDictionary(IDictionary<TValue, ICloudObject<TValue>> items, ICloudObjectFabric<TValue> fabric, 
            int maxCount = int.MaxValue, int maxWeight = int.MaxValue)
        {
            this.items = items;
            this.fabric = fabric;
            LimitCount(maxCount);
            LimitWeight(maxWeight);
        }
        
        public void AddValue(TValue value)
        {
            if (!items.TryGetValue(value, out var item))
                item = fabric.Create(value);
            item.WeightIncrement();
            items[value] = item;
        }

        public void LimitCount(int maxCount)
        {
            if (maxCount < 0)
                throw new ArgumentException();
            this.maxCount = maxCount;
        }
        
        public void LimitWeight(int maxWeight)
        {
            if (maxCount < 0)
                throw new ArgumentException();
            this.maxWeight = maxWeight;
        }

        public IEnumerable<ICloudObject<TValue>> GetAllCloudObjects()
        {
            return items.Values;
        }

        public IEnumerable<ICloudObject<TValue>> GetOrderedCloudObjects<TKey>()
        {
            return GetOrderedCloudObjects(x => x.Weight);
        }

        public IEnumerable<ICloudObject<TValue>> GetOrderedCloudObjects<TKey>(
            Func<ICloudObject<TValue>, TKey> keySelector)
        {
            return GetOrderedCloudObjects(keySelector, Comparer<TKey>.Default);
        }

        public IEnumerable<ICloudObject<TValue>> GetOrderedCloudObjects<TKey>(
            Func<ICloudObject<TValue>, TKey> keySelector, IComparer<TKey> comparer)
        {
            return GetAllCloudObjects()
                .OrderByDescending(keySelector, comparer)
                .SkipWhile(o => o.Weight > maxWeight)
                .Take(maxCount);
        }
    }
}
