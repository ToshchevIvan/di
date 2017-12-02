using System;
using System.Collections.Generic;


namespace TagsCloudContainer.ValuesSources
{
    public interface IValuesSource<out TValue> : IDisposable
    {
        IEnumerable<TValue> GetCloudObjectsValues();
    }
}
