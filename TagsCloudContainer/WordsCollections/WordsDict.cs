using System;
using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer.WordsCollections
{
    public class WordsDict : IWordsCollection
    {
        private readonly IDictionary<string, int> dict;

        public WordsDict(IDictionary<string, int> dict)
        {
            this.dict = dict;
        }

        public void AddWord(string word)
        {
            dict.TryGetValue(word, out var weight);
            dict[word] = weight + 1;
        }

        public IEnumerable<WeightedWord> GetWords()
        {
            return dict.Select(kv => new WeightedWord(kv.Key, kv.Value));
        }

        public IOrderedEnumerable<WeightedWord> GetOrderedWords<TKey>(Func<WeightedWord, TKey> keySelector,
            bool descending = false)
        {
            if (descending)
                return GetWords().OrderByDescending(keySelector);
            return GetWords().OrderBy(keySelector);
        }

        public IOrderedEnumerable<WeightedWord> GetOrderedWords()
        {
            return GetOrderedWords(w => w.Weight);
        }
    }
}
