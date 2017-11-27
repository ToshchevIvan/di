using System.Collections.Generic;


namespace TagsCloudContainer.WordsPreprocessors
{
    public class BoringWordsPreprocessor : IWordsPreprocessor
    {
        private readonly ISet<string> wordsToFilter;

        public BoringWordsPreprocessor(ISet<string> wordsToFilter)
        {
            this.wordsToFilter = wordsToFilter;
        }

        public bool ProcessWord(string word, out string result)
        {
            result = word;
            return !wordsToFilter.Contains(word);
        }
    }
}
