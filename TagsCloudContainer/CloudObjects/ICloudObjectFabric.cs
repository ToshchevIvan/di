namespace TagsCloudContainer.CloudObjects
{
    public interface ICloudObjectFabric<TValue>
    {
        ICloudObject<TValue> Create(TValue value);
    }
}
