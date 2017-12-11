using System.Drawing;
using TagsCloudContainer.Stylers;


namespace TagsCloudContainer.CloudObjects
{
	//TODO RV(atolstov): не стоит делать настолько абстрактные интерфейсы. Почему ICloudObject не может быть ITag, знающим свою строку, цвет, вес
    public interface ITag
    {
        string Value { get; }
        Style Style { get; }
        Point Location { get; }
    }
}
