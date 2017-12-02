using System.Collections.Generic;


namespace TagsCloudContainer.ValuesPreprocessors
{
    public interface IValuesPreprocessor<TValue>
    {
        bool Process(TValue value, out TValue newValue);
        IEnumerable<TValue> ProcessAll(IEnumerable<TValue> values);
    }
}
