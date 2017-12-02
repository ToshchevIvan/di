using System;
using System.Drawing.Imaging;


namespace TagsCloudContainer
{
    public interface ICloudCreator<TValue> : IDisposable
    {
        CloudCreator<TValue> ReadData();
        CloudCreator<TValue> DrawCloud();
        void SaveToFile(string filePath, ImageFormat format);
    }
}
