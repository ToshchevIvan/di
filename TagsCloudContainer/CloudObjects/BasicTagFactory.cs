using System.Drawing;
using TagsCloudContainer.Stylers;


namespace TagsCloudContainer.CloudObjects
{
	//TODO RV(atolstov): паттерн называется Factory а не Fabric
	public class BasicTagFactory : ITagFactory
    {
        public ITag Create(string value, Style style, Point location)
        {
            return new BasicTag(value, style, location);
        }
    }
}
