using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer.Normalizers
{
    public class ToLowerStringsNormalizer : INormalizer
    {
        public IEnumerable<string> Normalize(IEnumerable<string> values)
        {
            return values.Select(v => v?.ToLowerInvariant());
        }
    }
}
