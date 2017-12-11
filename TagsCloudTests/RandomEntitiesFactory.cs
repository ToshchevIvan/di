using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudObjects;
using TagsCloudContainer.Layouters;
using TagsCloudContainer.Stylers;


namespace TagsCloudTests
{
    internal static class RandomEntitiesFactory
    {
        private const string Chars = "abcdefghijklmnopqrstuvwxyz";
        private const int MinLength = 3;
        private const int MaxLength = 15;
        private const int MinWeight = 1;
        private const int MaxWeight = 30;

        private static readonly Random Randomizer = new Random();

        public static SpiralLayouter GetRandomLayouter(int canvasSideLength, ITagFactory factory)
        {
            var halfLength = canvasSideLength / 2;
            var fourthLength = canvasSideLength / 4;
            var center = new Point(halfLength, halfLength);
            var size = new Size(fourthLength, fourthLength);

            return new SpiralLayouter(
                GetRandomPoint(DrawingExtensions.CreateRectangleWithCenter(center, size)),
                factory);
        }

        public static Tuple<string, Style> GetRandomStyledString()
        {
            const int emSize = 10;
            var length = Randomizer.Next(MinLength, MaxLength);
            var str = new string(Enumerable.Repeat(Chars, length)
                .Select(s => s[Randomizer.Next(s.Length)])
                .ToArray());
            var weight = Randomizer.Next(MinWeight, MaxWeight);
            var newEmSize = emSize * weight;
            var size = new Size(newEmSize, newEmSize * length);
            var style = new Style(
                Brushes.Black, 
                new Font(FontFamily.GenericSansSerif, newEmSize),
                size
                );

            return Tuple.Create(str, style);
        }

        public static IDictionary<string, Style> GetRandomStyledStrings(int count)
        {
            var strings = new Dictionary<string, Style>(count);
            while (strings.Count < count)
            {
                var str = GetRandomStyledString();
                strings[str.Item1] = str.Item2;
            }
            return strings;
        }

        public static Point GetRandomPoint(Rectangle boundingBox)
        {
            return new Point(
                Randomizer.Next(boundingBox.Left, boundingBox.Right),
                Randomizer.Next(boundingBox.Top, boundingBox.Bottom)
            );
        }
    }
}
