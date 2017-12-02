using System;
using System.Drawing;
using TagsCloudContainer.CloudObjects;


namespace TagsCloudContainer.CloudDrawers
{
    public interface ICloudDrawer<in TValue> : IDisposable
    {
        Image Canvas { get; }
        void DrawAt(Point location, ICloudObject<TValue> cloudObject);
        Size GetSizeOf(ICloudObject<TValue> cloudObject);
    }
}
