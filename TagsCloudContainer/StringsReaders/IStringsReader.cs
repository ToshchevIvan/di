using System;
using System.Collections.Generic;


namespace TagsCloudContainer.StringsReaders
{
    public interface IStringsReader
    {
        IEnumerable<string> ReadStrings();
    }
}
