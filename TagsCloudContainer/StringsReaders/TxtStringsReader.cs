using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override IEnumerable<string> ProcessLines(IEnumerable<string> lines)
        {
            return lines.Select(line => line?.Trim())
                .Where(line => !string.IsNullOrEmpty(line));
        }
    }
}
