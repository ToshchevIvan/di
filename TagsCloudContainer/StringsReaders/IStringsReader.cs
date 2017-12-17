using System;
using System.Collections.Generic;


namespace TagsCloudContainer.StringsReaders
{
    public interface IStringsReader
    {
        Result<IEnumerable<string>> ReadStrings();
    }
}
