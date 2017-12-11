using System;
using System.Text;


namespace TagsCloudContainer.StringsReaders
{
    public class TxtStringsReader : AbstractFileStringsReader
    {
        public TxtStringsReader(string pathToFile, Encoding encoding) : base(pathToFile, encoding)
        {
            if (pathToFile == null || !pathToFile.EndsWith(".txt"))
                throw new ArgumentException();
        }

        protected override bool ProcessLine(string line, out string newLine)
        {
            newLine = line?.Trim();
            return !string.IsNullOrEmpty(line);
        }
    }
}
