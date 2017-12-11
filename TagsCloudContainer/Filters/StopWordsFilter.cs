using System.Collections.Generic;


namespace TagsCloudContainer.Filters
{
	//TODO RV(atolstov): правильно называть их стоп-словами
    public class StopWordsFilter : ParametrizedFilter
    {
        public StopWordsFilter(ISet<string> stopWords) : base(s => !stopWords.Contains(s))
        {
        }
    }
}
