namespace TagsCloudContainer.CloudObjects
{
	//TODO RV(atolstov): не стоит делать настолько абстрактные интерфейсы. Почему ICloudObject не может быть ITag, знающим свою строку, цвет, вес
    public interface ICloudObject<out TValue>
    {
        TValue Value { get; }
        int Weight { get; }
        // Выглядит странно, вообще говоря
        void WeightIncrement();
    }
}
