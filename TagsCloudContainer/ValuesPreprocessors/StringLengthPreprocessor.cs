using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer.ValuesPreprocessors
{
    public class StringLengthPreprocessor : IValuesPreprocessor<string>
    {
        private readonly int threshold;

        public StringLengthPreprocessor(int threshold)
        {
            this.threshold = threshold;
        }
        
        public bool Process(string value, out string newValue)
        {
            newValue = value;
            return value?.Length >= threshold;
        }

        public IEnumerable<string> ProcessAll(IEnumerable<string> values)
        {
            return values.Where(v => Process(v, out v));
        }
    }
}
