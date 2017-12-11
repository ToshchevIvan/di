using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Autofac;
using CommandLine;
using NHunspell;
using TagsCloudConsole.Options;
using TagsCloudContainer.CloudObjects;
using TagsCloudContainer.Filters;
using TagsCloudContainer.Layouters;
using TagsCloudContainer.Normalizers;
using TagsCloudContainer.Renderers;
using TagsCloudContainer.Statisticians;
using TagsCloudContainer.StringsReaders;
using TagsCloudContainer.Stylers;


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
            stopWords = cmdOptions.StopWordsFile != null ? 
                File.ReadAllLines(cmdOptions.StopWordsFile) : new string[0];    
            var options = new CloudOptions(
                cmdOptions.InputFile,
                cmdOptions.OutputFile,
                cmdOptions.InputFormat,
                cmdOptions.OutputFormat,
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
            var strings = di.Resolve<IStringsReader>().ReadStrings();
            var normalized = di.Resolve<IEnumerable<INormalizer>>()
                .Aggregate(strings, (current, normalizer) => normalizer.Normalize(current));
            var filtered = di.Resolve<IEnumerable<IFilter>>()
                .Aggregate(normalized, (current, filter) => filter.Filter(current));
            IDictionary<string, int> statistics = di.Resolve<IStatistician>()
                .GetStatistic(filtered)
                .OrderByDescending(x => x.Value)
                .Take(options.MaxCount)
                .ToDictionary(x => x.Key, x => x.Value);
            var styled = di.Resolve<IStyler>()
                .GetStyles(statistics);
            var layout = di.Resolve<ILayouter>()
                .GetLayout(styled);
            Bitmap bitmap;
            using (var renderer = di.Resolve<IRenderer<Bitmap>>())
                bitmap = renderer.Render(layout);
            bitmap.Save(options.OutputFile, options.OutputFormat);
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
                .WithParameter("hunspell", new Hunspell("en_us.aff", "en_us.dic"))
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
    }
}
