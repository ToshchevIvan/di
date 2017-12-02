using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudContainer.CloudDrawers;
using TagsCloudContainer.CloudObjects;
using TagsCloudContainer.CloudObjectsContainers;
using TagsCloudContainer.RectanglesLayouters;
using TagsCloudContainer.ValuesPreprocessors;
using TagsCloudContainer.ValuesSources;


namespace TagsCloudContainer
{
    public class CloudCreator<TValue> : ICloudCreator<TValue>
    {
        private readonly IValuesSource<TValue> dataValuesSource;
        private readonly IEnumerable<IValuesPreprocessor<TValue>> preprocessors;
        private readonly ICloudObjectContainer<TValue> container;
        private readonly IRectanglesLayouter layouter;
        private readonly ICloudDrawer<TValue> drawer;

        public CloudCreator(IValuesSource<TValue> dataValuesSource,
            IEnumerable<IValuesPreprocessor<TValue>> preprocessors,
            ICloudObjectContainer<TValue> container,
            IRectanglesLayouter layouter,
            ICloudDrawer<TValue> drawer)
        {
            this.dataValuesSource = dataValuesSource;
            this.preprocessors = preprocessors;
            this.container = container;
            this.layouter = layouter;
            this.drawer = drawer;
        }

        public CloudCreator<TValue> ReadData()
        {
            foreach (var value in dataValuesSource.GetCloudObjectsValues())
            {
                var newValue = value;
                if (preprocessors.All(p => p.Process(newValue, out newValue)))
                    container.AddValue(newValue);
            }

            return this;
        }

        public CloudCreator<TValue> DrawCloud()
        {
            var objects = container.GetOrderedCloudObjects<TValue>()
                .ToArray();
            var locations = GetLocations(objects);
            var locatedObjects = locations.Zip<Point, ICloudObject<TValue>, object>(objects,
                    (point, obj) =>
                    {
                        drawer.DrawAt(point, obj);
                        return null;
                    })
                .ToArray();

            return this;
        }

        public void SaveToFile(string filePath, ImageFormat format)
        {
            drawer.Canvas.Save(filePath, format);
        }

        private IEnumerable<Point> GetLocations(IEnumerable<ICloudObject<TValue>> objects)
        {
            return objects
                .Select(o => drawer.GetSizeOf(o))
                .Select(s => layouter.PutNextRectangle(s))
                .Select(r => r.Location);
        }

        public void Dispose()
        {
            dataValuesSource?.Dispose();
            drawer?.Dispose();
        }
    }
}
