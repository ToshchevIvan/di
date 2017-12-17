using System.Collections.Generic;


namespace TagsCloudContainer.Normalizers
{
    public interface INormalizer
    {
        Result<IEnumerable<string>> Normalize(IEnumerable<string> values);
    }
}
