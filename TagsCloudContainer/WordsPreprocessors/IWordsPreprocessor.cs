namespace TagsCloudContainer.WordsPreprocessors
{
    public interface IWordsPreprocessor
    {
        bool ProcessWord(string word, out string result);
    }
}
