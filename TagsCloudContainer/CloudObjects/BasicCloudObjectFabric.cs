namespace TagsCloudContainer.CloudObjects
{
    public class BasicCloudObjectFabric<TValue> : ICloudObjectFabric<TValue>
    {
        public ICloudObject<TValue> Create(TValue value)
        {
            return new BasicCloudObject<TValue>(value, 0);
        }
    }
}
