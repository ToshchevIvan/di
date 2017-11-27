namespace TagsCloudContainer.WordsPreprocessors
{
    public class ToLowerWordsPreprocessor : IWordsPreprocessor
    {
        public bool ProcessWord(string word, out string result)
        {
            result = word.ToLowerInvariant();
            return true;
        }
    }
}
