using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer.ValuesPreprocessors
{
	//TODO RV(atolstov): правильно называть их стоп-словами
    public class BoringWordsStringsPreprocessor : IValuesPreprocessor<string>
    {
        private readonly ISet<string> wordsToExclude;

        public BoringWordsStringsPreprocessor(ISet<string> wordsToExclude)
        {
            this.wordsToExclude = wordsToExclude;
        }

		//TODO RV(atolstov): почему бы не возвращать null, если слово не нужно включать? Или функциональность стоит разделить на Normalizer и Filter?
		public bool Process(string value, out string newValue)
        {
            newValue = value;
            return !wordsToExclude.Contains(value);
        }

	    //TODO RV(atolstov): Почему бы не вынести в extension-метод?
		public IEnumerable<string> ProcessAll(IEnumerable<string> values)
        {
            return values.Where(v => Process(v, out v));
        }
    }
}
