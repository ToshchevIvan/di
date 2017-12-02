namespace TagsCloudContainer.CloudObjects
{
    public interface ICloudObject<out TValue>
    {
        TValue Value { get; }
        int Weight { get; }
        // Выглядит странно, вообще говоря
        void WeightIncrement();
    }
}
