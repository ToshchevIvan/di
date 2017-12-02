using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer.ValuesPreprocessors
{
    public class ToLowerStringsPreprocessor : IValuesPreprocessor<string>
    {
        public bool Process(string value, out string newValue)
        {
            newValue = value?.ToLowerInvariant();
            return true;
        }

        public IEnumerable<string> ProcessAll(IEnumerable<string> values)
        {
            return values.Select(val =>
            {
                Process(val, out var res);
                return res;
            });
        }
    }
}
