﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace TagsCloudContainer.StringsReaders
{
    public abstract class AbstractFileStringsReader : IStringsReader
    {
        private readonly string pathToFile;
        private readonly Encoding encoding;

        protected AbstractFileStringsReader(string pathToFile, Encoding encoding)
        {
            this.pathToFile = pathToFile;
            this.encoding = encoding;
        }
        
        public IEnumerable<string> ReadStrings()
        {
            return ProcessLines(File.ReadLines(pathToFile, encoding));
        }
        
        protected abstract IEnumerable<string> ProcessLines(IEnumerable<string> lines);
    }
}