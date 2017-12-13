using System.Drawing;
using TagsCloudContainer.Stylers;


namespace TagsCloudContainer.CloudObjects
{
	//TODO RV(atolstov): опять же, зачем вводить интерфейс ITag? Ты собираешься менять логику его работы в дальнейшем? 
    public interface ITag
    {
        string Value { get; }
        Style Style { get; }
        Point Location { get; }
    }
}
