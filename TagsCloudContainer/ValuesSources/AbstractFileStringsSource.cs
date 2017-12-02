using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace TagsCloudContainer.ValuesSources
{
    public abstract class AbstractFileStringsSource : IValuesSource<string>
    {
        protected readonly StreamReader Reader;
        private bool isDisposed;
        
        public AbstractFileStringsSource(string pathToFile, Encoding encoding)
        {
            Reader = new StreamReader(pathToFile, encoding);
        }
        
        public IEnumerable<string> GetCloudObjectsValues()
        {
            CheckDisposed();
            while (!Reader.EndOfStream)
            {
                if (ProcessLine(Reader.ReadLine(), out var line))
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
            Reader?.Dispose();
            isDisposed = true;
        }
    }
}
