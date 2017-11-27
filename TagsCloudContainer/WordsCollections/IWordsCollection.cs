using System;
using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer.WordsCollections
{
    public interface IWordsCollection
    {
        void AddWord(string word);
        IEnumerable<WeightedWord> GetWords();
        IOrderedEnumerable<WeightedWord> GetOrderedWords();
        IOrderedEnumerable<WeightedWord> GetOrderedWords<TKey>(Func<WeightedWord, TKey> keySelector, bool descending = false);
    }
}
