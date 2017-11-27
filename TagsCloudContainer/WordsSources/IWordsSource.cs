using System.Collections.Generic;


namespace TagsCloudContainer.WordsSources
{
    public interface IWordsSource
    {
        IEnumerable<string> GetWords();
    }
}
