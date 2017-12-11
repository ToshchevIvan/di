namespace TagsCloudContainer.Filters
{
    public class StringLengthFilter : ParametrizedFilter
    {
        public StringLengthFilter(int threshold) : base(s => s?.Length >= threshold)
        {
        }
    }
}
