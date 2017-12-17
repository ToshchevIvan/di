using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Autofac;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.Layouters;
using TagsCloudContainer.Statisticians;
using TagsCloudContainer.Stylers;
using TagsCloudContainer.Tags;


namespace TagsCloudTests
{
    public class SpiralLayouter_should
    {
        private const int CanvasSideLength = 2500;
        private const int TagsCount = 100;

        private IContainer di;
        private ILayouter layouter;
        private List<ITag> currentLayout;
        private IStyler styler;

        [SetUp]
        public void SetUp()
        {
            di = GetDiContainer();
            layouter = RandomEntitiesFactory.GetRandomLayouter(CanvasSideLength, di.Resolve<ITagFactory>());
            currentLayout = new List<ITag>();
            styler = Substitute.For<IStyler>();
            styler.GetStyles(Arg.Any<IDictionary<string, int>>())
                .Returns(RandomEntitiesFactory
                    .GetRandomStyledStrings(TagsCount)
                    .AsResult());
        }

        private static IContainer GetDiContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BasicTagFactory>()
                .As<ITagFactory>();
            builder.RegisterType<StringCountStatistic>()
                .As<IStatistician>();
            return builder.Build();
        }

        [Test]
        public void Throw_WhenSuppliedWithIncorrectCenter()
        {
            Action act = () => new SpiralLayouter(new Point(-10, -10), di.Resolve<ITagFactory>());

            act.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void PlaceFirstTag_IntoCenter()
        {
            var center = new Point(CanvasSideLength / 2, CanvasSideLength / 2);
            var layouter = new SpiralLayouter(center, di.Resolve<ITagFactory>());

            var firstTag = styler.GetStyles(Arg.Any<IDictionary<string, int>>())
                .Then(layouter.GetLayout)
                .GetValueOrThrow()
                .First();
            currentLayout.Add(firstTag);

            firstTag.BoundingBox()
                .GetCenter()
                .Should()
                .Be(center);
        }

        [Test]
        public void NotPlace_IntersectingTags()
        {
            currentLayout = GetLayout();

            var intersections = currentLayout.Select(tag => tag.BoundingBox())
                .Select((rect, index) => new {rect, index})
                .SelectMany(r => currentLayout.Skip(r.index + 1),
                    (r1, r2) => r1.rect.IntersectsWith(r2.BoundingBox()));

            intersections.Any(b => b)
                .Should()
                .BeFalse();
        }

        private double GetLayoutRadius(List<Rectangle> rectangles)
        {
            var center = rectangles[0].GetCenter();

            return rectangles.Select(r => r.Location.DistanceTo(center))
                .Max();
        }

        [Test]
        public void GenerateDenseLayout()
        {
            const double margin = 0.4;

            currentLayout = GetLayout();
            var radius = GetLayoutRadius(currentLayout.Select(t => t.BoundingBox()).ToList());
            var coveringCircleArea = radius * radius * Math.PI;
            var actualArea = currentLayout.Select(t => t.BoundingBox().Area())
                .Sum();

            Math.Min(coveringCircleArea / actualArea, actualArea / coveringCircleArea)
                .Should()
                .BeGreaterOrEqualTo(margin);
        }

        private List<ITag> GetLayout()
        {
            return styler.GetStyles(Arg.Any<IDictionary<string, int>>())
                .Then(layouter.GetLayout)
                .GetValueOrThrow()
                .ToList();
        }
    }

    internal static class TagExtensions
    {
        public static Rectangle BoundingBox(this ITag tag) =>
            new Rectangle(tag.Location, tag.Style.Size);
    }
}
