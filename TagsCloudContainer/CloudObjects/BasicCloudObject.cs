namespace TagsCloudContainer.CloudObjects
{
    public class BasicCloudObject<TValue> : ICloudObject<TValue>
    {
        public TValue Value { get; }
        public int Weight { get; private set; }

        public BasicCloudObject(TValue value, int weight)
        {
            Value = value;
            Weight = weight;
        }

        public void WeightIncrement()
        {
            Weight += 1;
        }
    }
}
