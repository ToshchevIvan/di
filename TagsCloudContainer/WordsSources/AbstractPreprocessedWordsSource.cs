using System.Collections.Generic;
using TagsCloudContainer.WordsPreprocessors;


namespace TagsCloudContainer.WordsSources
{
    public abstract class AbstractPreprocessedWordsSource : IWordsSource
    {
        public IEnumerable<IWordsPreprocessor> Preprocessors { get; }
        
        protected AbstractPreprocessedWordsSource(params IWordsPreprocessor[] preprocessors)
        {
            Preprocessors = preprocessors;
        }

        public abstract IEnumerable<string> GetWords();
    }
}
