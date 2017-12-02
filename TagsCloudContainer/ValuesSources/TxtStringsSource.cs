using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace TagsCloudContainer.ValuesSources
{
    public class TxtStringsSource : AbstractFileStringsSource
    {
        public TxtStringsSource(string pathToFile, Encoding encoding) : base(pathToFile, encoding)
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
