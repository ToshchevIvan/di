using System.Collections.Generic;


namespace TagsCloudContainer.Normalizers
{
    public interface INormalizer
    {
        IEnumerable<string> Normalize(IEnumerable<string> values);
    }
}
