using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.CloudObjects;


namespace TagsCloudContainer.CloudObjectsContainers
{
    public interface ICloudObjectContainer<TValue>
    {
        int Count { get; }
        void AddValue(TValue value);
        void LimitCount(int maxCount);
        void LimitWeight(int maxWeight);
        IEnumerable<ICloudObject<TValue>> GetAllCloudObjects();
        IEnumerable<ICloudObject<TValue>> GetOrderedCloudObjects<TKey>();
        IEnumerable<ICloudObject<TValue>> GetOrderedCloudObjects<TKey>(
            Func<ICloudObject<TValue>, TKey> keySelector);
        IEnumerable<ICloudObject<TValue>> GetOrderedCloudObjects<TKey>(
            Func<ICloudObject<TValue>, TKey> keySelector, IComparer<TKey> comparer);
    }
}
