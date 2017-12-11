using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace TagsCloudContainer.StringsReaders
{
    public abstract class AbstractFileStringsReader : IStringsReader
    {
        protected readonly StreamReader Stream;
        private bool isDisposed;
        
        public AbstractFileStringsReader(string pathToFile, Encoding encoding)
        {
            Stream = new StreamReader(pathToFile, encoding);
        }
        
        public IEnumerable<string> ReadStrings()
        {
            CheckDisposed();
            while (!Stream.EndOfStream)
            {
                if (ProcessLine(Stream.ReadLine(), out var line))
                    yield return line;
            }
        }

        protected abstract bool ProcessLine(string line, out string newLine);
        
        protected void CheckDisposed()
        {
            if (isDisposed)
                throw new InvalidOperationException("Object has been already disposed");
        }

        public void Dispose()
        {
            Stream?.Dispose();
            isDisposed = true;
        }
    }
}
