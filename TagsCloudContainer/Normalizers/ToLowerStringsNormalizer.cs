using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer.Normalizers
{
    public class ToLowerStringsNormalizer : INormalizer
    {
        public Result<IEnumerable<string>> Normalize(IEnumerable<string> values)
        {
            return Result.Of(() => values.Select(v => v?.ToLowerInvariant()));
        }
    }
}
