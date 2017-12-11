using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;
using CommandLine.Text;
using TagsCloudConsole.Options;


namespace TagsCloudConsole
{
    public class CommandLineOptions
    {
        [Option('i', "input", Required = true, HelpText = "Input file to read.")]
        public string InputFile { get; set; }
        
        [Option('o', "output", Required = true, HelpText = "Output file.")]
        public string OutputFile { get; set; }

        [Option("input-format", HelpText = "Must be one of the following: Txt.")]
        public InputFormat InputFormat { get; set; } = InputFormat.Txt;

        // Кажется, не понимает значения enum, вываливает справку. Хотя enum строчкой выше работает :\
        [Option("output-format", HelpText = "Must have one of the ImageFormat enum values.")]
        public ImageFormat OutputFormat { get; set; } = ImageFormat.Png;

        [Option("max-count", DefaultValue = 100, HelpText = "The maximum number of words in cloud.")]
        public int MaxCount { get; set; }
        
        [Option("max-weight", DefaultValue = 150, HelpText = "The maximum weight of word in cloud.")]
        public int MaxWeight { get; set; }
        
        [OptionArray('c', "center", DefaultValue = new[] {1500, 1500}, HelpText = "The center (pair of ints) of cloud.")]
        public int[] Center { get; set; }
        
        [Option("canvas-side", MutuallyExclusiveSet = "canvas-size", HelpText = "Length of canvas side.")]
        public int? CanvasSide { get; set; }
        
        [OptionArray("canvas-size", DefaultValue = new[] {3000, 3000}, MutuallyExclusiveSet = "canvas-side", 
            HelpText = "Pair of integers setting the size of canvas.")]
        public int[] CanvasSize { get; set; }
        
        [Option('s', "font-size", DefaultValue = 20, HelpText = "Font em size.")]
        public float FontSize { get; set; }
        
        [Option('f', "font-family", HelpText = "Must have one of the FontFamily enum values.")]
        public FontFamily FontFamily { get; set; } = FontFamily.GenericSansSerif;
        
        [Option("stop-words-file", DefaultValue = null, HelpText = "Txt file with words that should be excluded.")]
        public string StopWordsFile { get; set; }
        
        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText {
                Heading = new HeadingInfo("TagsCloud"),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            help.AddPreOptionsLine("Usage: TagsCloudConsole.exe -i input.txt -o output.txt");
            help.AddOptions(this);
            return help;
        }
    }
}
