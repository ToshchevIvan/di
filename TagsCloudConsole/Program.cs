﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using CommandLine;
using NHunspell;
using TagsCloudConsole.Options;
using TagsCloudContainer;
using TagsCloudContainer.Filters;
using TagsCloudContainer.Layouters;
using TagsCloudContainer.Normalizers;
using TagsCloudContainer.Renderers;
using TagsCloudContainer.Statisticians;
using TagsCloudContainer.StringsReaders;
using TagsCloudContainer.Stylers;
using TagsCloudContainer.Tags;


namespace TagsCloudConsole
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var cmdOptions = new CommandLineOptions();
            if (!Parser.Default.ParseArguments(args, cmdOptions))
                return;
            Size canvasSize;
            if (cmdOptions.CanvasSide != null)
                canvasSize = new Size((int) cmdOptions.CanvasSide, (int) cmdOptions.CanvasSide);
            else
                canvasSize = new Size(cmdOptions.CanvasSize[0], cmdOptions.CanvasSize[1]);
            var center = new Point(cmdOptions.Center[0], cmdOptions.Center[1]);
            var defaultColorPalette = new[]
            {
                Color.CornflowerBlue, Color.BlueViolet, Color.IndianRed, Color.OliveDrab, Color.CadetBlue
            };

            string[] stopWords;
            stopWords = cmdOptions.StopWordsFile != null ? File.ReadAllLines(cmdOptions.StopWordsFile) : new string[0];

            var options = new CloudOptions(
                cmdOptions.InputFile,
                cmdOptions.OutputFile,
                cmdOptions.InputFormat,
                GetImageFormat(cmdOptions.OutputFormat),
                center,
                canvasSize,
                defaultColorPalette,
                cmdOptions.FontSize,
                cmdOptions.FontFamily,
                cmdOptions.MaxCount,
                cmdOptions.MaxWeight,
                stopWords
            );
            var di = GetDiContainer(options);
            RunProgram(di, options);
        }

        private static void RunProgram(IContainer di, CloudOptions options)
        {
            var result = Result.Of(() => di.Resolve<IStringsReader>().ReadStrings(), "Can't read input")
                .Then(strings =>
                    di.Resolve<IEnumerable<INormalizer>>()
                        .Aggregate(strings, (current, normalizer) => normalizer.Normalize(current)
                        ),
                    "Can't filter strings"
                )
                .Then(normalized =>
                        di.Resolve<IEnumerable<IFilter>>()
                            .Aggregate(normalized, (current, filter) => filter.Filter(current)
                            ),
                    "Can't normalize strings"
                )
                .Then(di.Resolve<IStatistician>().GetStatistic, "Can't get statistics")
                .Then(stats => GetOrderedStatistics(stats, options.MaxCount))
                .Then(di.Resolve<IStyler>().GetStyles, "Can't generate styles")
                .Then(di.Resolve<ILayouter>().GetLayout, "Can't make layout")
                .Then(layout => RenderCloud(layout, di), "Can't render tag cloud")
                .Then(image => image.Save(options.OutputFile, options.OutputFormat), "Can't save image")
                .RefineError("Failed to create cloud");
            try
            {
                result.GetValueOrThrow();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static IDictionary<string, int> GetOrderedStatistics(IDictionary<string, int> stats, int count)
        {
            return stats.OrderByDescending(x => x.Value)
                .Take(count)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private static Bitmap RenderCloud(IEnumerable<ITag> layout, IContainer di)
        {
            using (var renderer = di.Resolve<IRenderer<Bitmap>>())
                return renderer.Render(layout);
        }

        private static IContainer GetDiContainer(CloudOptions options)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BasicTagFactory>()
                .As<ITagFactory>();

            builder.RegisterInstance(new TxtStringsReader(options.InputFile, Encoding.UTF8))
                .As<IStringsReader>();

            builder.RegisterType<StopWordsFilter>()
                .WithParameter("stopWords", new HashSet<string>(options.StopWords))
                .As<IFilter>();
            builder.RegisterType<StringLengthFilter>()
                .WithParameter("threshold", 5)
                .As<IFilter>();

            builder.RegisterType<ToLowerStringsNormalizer>()
                .As<INormalizer>();
            builder.RegisterType<WordStemNormalizer>()
                .WithParameter("affFile", "en_us.aff")
                .WithParameter("dictFile", "en_us.dic")
                .As<INormalizer>();

            builder.RegisterType<StringCountStatistic>()
                .WithParameter("maxCount", options.MaxCount)
                .WithParameter("maxWeight", options.MaxWeight)
                .As<IStatistician>();

            builder.Register(c => new StringsStyler(
                    new Font(options.FontFamily, options.FontEmSize, FontStyle.Regular),
                    1f,
                    options.ColorPalette))
                .As<IStyler>();

            builder.RegisterType<SpiralLayouter>()
                .WithParameter("center", options.Center)
                .As<ILayouter>();

            builder.Register(c => new BitmapRenderer(new Bitmap(options.CanvasSize.Width, options.CanvasSize.Height)))
                .As<IRenderer<Bitmap>>();

            return builder.Build();
        }

        private static ImageFormat GetImageFormat(OutputFormat format)
        {
            const BindingFlags flags = BindingFlags.GetProperty;
            var type = typeof(ImageFormat);
            var o = type.InvokeMember(format.ToString(), flags, null, type, null);
            return (ImageFormat) o;
        }
    }
}