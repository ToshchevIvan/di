using System;
using System.Collections.Generic;
using System.Linq;
using NHunspell;


namespace TagsCloudContainer.Normalizers
{
    public class WordStemNormalizer : INormalizer
    {
        private readonly string affFile;
        private readonly string dictFile;

        public WordStemNormalizer(string affFile, string dictFile)
        {
            this.affFile = affFile;
            this.dictFile = dictFile;
        }

        public Result<IEnumerable<string>> Normalize(IEnumerable<string> values)
        {
            return Result.Of(() => new Hunspell(affFile, dictFile))
                .RefineError("Hunspell failed to load dictionaries")
                .Then(hunspell => values.Select(hunspell.Stem)
                    .SelectMany(stems => stems));
        }
    }
}
