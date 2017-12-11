using System;
using System.Collections.Generic;
using System.Linq;
using NHunspell;


namespace TagsCloudContainer.Normalizers
{
    public class WordStemNormalizer : INormalizer
    {
        private readonly Hunspell hunspell;

        public WordStemNormalizer(Hunspell hunspell)
        {
            this.hunspell = hunspell;
        }
        
        public IEnumerable<string> Normalize(IEnumerable<string> values)
        {
            return values.Select(hunspell.Stem)
                .SelectMany(stems => stems);
        }
    }
}
