using System.Drawing;
using TagsCloudContainer.CloudPainters;
using TagsCloudContainer.RectangleLayouters;
using TagsCloudContainer.WordsCollections;
using TagsCloudContainer.WordsSources;


namespace TagsCloudContainer.CloudCreators
{
    public class DefaultCloudCreator : ICloudCreator
    {
        private readonly IWordsCollection words;
        private readonly IRectangleLayouter layouter;
        private readonly ICloudPainter painter;
        

        public DefaultCloudCreator(IWordsSource source, IWordsCollection words, IRectangleLayouter layouter, ICloudPainter painter)
        {
            this.words = words;
            foreach (var word in source.GetWords())
                words.AddWord(word);
            this.layouter = layouter;
            this.painter = painter;
        }

        public ICloudCreator PlaceWords()
        {
            foreach (var word in words.GetOrderedWords())
            {
                var font = new Font(painter.Font.FontFamily, painter.Font.Size * word.Weight);
                var size = Size.Truncate(painter.Graphics.MeasureString(word.Word, font));
                var coords = layouter.PutNextRectangle(size).Location;
                painter.PaintWord(word.Word, font);
            }
            return this;
        }

        public ICloudCreator SaveToFile(string filePath)
        {
            painter.SaveToFile(filePath);
            return this;
        }
    }
}
