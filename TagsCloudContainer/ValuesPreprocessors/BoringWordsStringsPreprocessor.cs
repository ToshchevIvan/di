using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer.ValuesPreprocessors
{
    public class BoringWordsStringsPreprocessor : IValuesPreprocessor<string>
    {
        private readonly ISet<string> wordsToExclude;

        public BoringWordsStringsPreprocessor(ISet<string> wordsToExclude)
        {
            this.wordsToExclude = wordsToExclude;
        }
        
        public bool Process(string value, out string newValue)
        {
            newValue = value;
            return !wordsToExclude.Contains(value);
        }

        public IEnumerable<string> ProcessAll(IEnumerable<string> values)
        {
            return values.Where(v => Process(v, out v));
        }
    }
}
