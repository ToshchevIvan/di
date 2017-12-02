using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Autofac;
using CommandLine;
using TagsCloudConsole.Options;
using TagsCloudContainer;
using TagsCloudContainer.CloudDrawers;
using TagsCloudContainer.CloudDrawers.Stylers;
using TagsCloudContainer.CloudObjects;
using TagsCloudContainer.CloudObjectsContainers;
using TagsCloudContainer.RectanglesLayouters;
using TagsCloudContainer.ValuesPreprocessors;
using TagsCloudContainer.ValuesSources;


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
            // Не умеет читать Point
            var center = new Point(cmdOptions.Center[0], cmdOptions.Center[1]);
            // Как задавать список цветов через консоль по-человечески? Проблема.
            var defaultColorPalette = new[]
            {
                Color.CornflowerBlue, Color.BlueViolet, Color.IndianRed, Color.OliveDrab, Color.CadetBlue
            };
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
                cmdOptions.MaxCount
            );
            var creator = GetCloudCreator(options);
            RunProgram(creator, options);
        }

        public static void RunProgram(ICloudCreator<string> creator, CloudOptions options)
        {
            using (creator)
            {
                creator.ReadData()
                    .DrawCloud()
                    .SaveToFile(options.OutputFile, options.OutputFormat);
            }
        }

        public static ICloudCreator<string> GetCloudCreator(CloudOptions options)
        {
            // Не всё получается сделать дженериками...
            var boringWordsPreprocessor =
                new BoringWordsStringsPreprocessor(new HashSet<string> {"be", "a", "the", "in", "to", "that", "alice"});
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(CloudCreator<>)).AsSelf();
            builder.RegisterGeneric(typeof(BasicCloudObjectFabric<>))
                .As(typeof(ICloudObjectFabric<>));
            builder.RegisterInstance(new TxtStringsSource(options.InputFile, Encoding.UTF8))
                .As<IValuesSource<string>>();
            builder.RegisterInstance(new ToLowerStringsPreprocessor())
                .As<IValuesPreprocessor<string>>();
            builder.RegisterInstance(boringWordsPreprocessor)
                .As<IValuesPreprocessor<string>>();
            builder.RegisterInstance(new StringLengthPreprocessor(5))
                .As<IValuesPreprocessor<string>>();
            // Не может забиндить словарь. Не пойму, почему, есть же пустой конструктор
//            builder.RegisterGeneric(typeof(Dictionary<,>))
//                .As(typeof(IDictionary<,>));
            builder.Register(c => new Dictionary<string, ICloudObject<string>>())
                .As<IDictionary<string, ICloudObject<string>>>();
            builder.RegisterGeneric(typeof(CloudObjectsDictionary<>))
                .WithParameter("maxCount", options.MaxCount)
//                .WithParameter("maxWeight", 350)
                .As(typeof(ICloudObjectContainer<>));
            builder.RegisterInstance(new SpiralRectanglesLayouter(options.Center))
                .As<IRectanglesLayouter>();
            builder.RegisterType<BitmapStringCloudDrawer>()
                .As<ICloudDrawer<string>>();
            builder.Register(c => new Bitmap(options.CanvasSize.Width, options.CanvasSize.Height))
                .As<Image>();
            builder.Register(c => new StringsStyler(
                    new Font(options.FontFamily, options.FontEmSize, FontStyle.Regular),
                    1f,
                    options.ColorPalette))
                .As<ICloudObjectsStyler<string>>();
            var container = builder.Build();

            return container.Resolve<CloudCreator<string>>();

// // How does it really look like
//            var creator = new CloudCreator<string>(
//                new TxtStringsSource("source.txt", Encoding.UTF8),
//                new IValuesPreprocessor<string>[]
//                {
//                    new StringLengthPreprocessor(5),
//                    new ToLowerStringsPreprocessor(),
//                    new BoringWordsStringsPreprocessor(
//                        new HashSet<string>
//                        {
//                            "to",
//                            "be",
//                            "a",
//                            "an",
//                            "the",
//                            "and",
//                            "of",
//                            "i",
//                            "my",
//                            "that",
//                            "this",
//                            "thou"
//                        }
//                    )
//                },
//                new CloudObjectsDictionary<string>(
//                    new Dictionary<string, ICloudObject<string>>(),
//                    new BasicCloudObjectFabric<string>(), 100, 350),
//                new SpiralRectanglesLayouter(new Point(2500, 2500)),
//                new BitmapStringCloudDrawer(new Bitmap(5000, 5000),
//                    new StringsStyler(
//                        new Font(FontFamily.GenericSansSerif, 50, FontStyle.Regular),
//                        2f,
//                        new Pen(Color.Chocolate),
//                        new[]
//                        {
//                            Color.CornflowerBlue, Color.BlueViolet, Color.IndianRed, Color.OliveDrab, Color.CadetBlue
//                        }))
//            );
        }
    }
}
