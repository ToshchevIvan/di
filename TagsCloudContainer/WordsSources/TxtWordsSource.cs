using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TagsCloudContainer.WordsPreprocessors;


namespace TagsCloudContainer.WordsSources
{
    public class TxtWordsSource : AbstractPreprocessedWordsSource, IDisposable
    {
        private readonly StreamReader reader;
        private bool isDisposed;

        public TxtWordsSource(string pathToFile, Encoding encoding, params IWordsPreprocessor[] preprocessors)
            : base(preprocessors)
        {
            if (pathToFile == null || !pathToFile.EndsWith(".txt"))
                throw new ArgumentException();
            reader = new StreamReader(pathToFile, encoding);
        }

        public override IEnumerable<string> GetWords()
        {
            CheckDisposed();
            while (!reader.EndOfStream)
            {
                var word = reader.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(word))
                    continue;
                if (Preprocessors.All(p => p.ProcessWord(word, out word)))
                    yield return word;
            }
        }

        private void CheckDisposed()
        {
            if (isDisposed)
                throw new InvalidOperationException("Object has been already disposed");
        }

        public void Dispose()
        {
            reader?.Dispose();
            isDisposed = true;
        }
    }
}
