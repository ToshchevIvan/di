using System.Drawing;
using TagsCloudContainer.CloudObjects;


namespace TagsCloudContainer.CloudDrawers.Stylers
{
    public interface ICloudObjectsStyler<in TValue>
    {
        Style GetStylesFor(ICloudObject<TValue> cloudObject);
        Font GetFontFor(ICloudObject<TValue> cloudObject);
        Pen GetPenFor(ICloudObject<TValue> cloudObject);
        Brush GetBrushFor(ICloudObject<TValue> cloudObject);
    }
}
