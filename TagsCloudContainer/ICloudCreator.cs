using System;
using System.Drawing.Imaging;


namespace TagsCloudContainer
{
	//TODO RV(atolstov): тебе не кажется, что проще разделитьвесь код на несколько этапов:
	//	1) Считывание данных: IStringsReader, IEnumerable<string>
	//	2) Фильтрация данных: IFilter, IEnumerable<string>
	//	3) Нормализация данных: INormalizer, IEnumerable<string>
	//	4) Подсчет статистики: IStatistician, StringsStatistic: string -> weight
	//	5) "Раскладка" тегов: ILayouter, TagCloud: string -> Tag
	//	6) Рендеринг тегов: IRenderer, Bitmap/Image/Form/...
	public interface ICloudCreator<TValue> : IDisposable
    {
        CloudCreator<TValue> ReadData(); //TODO RV(atolstov): нарушение DIP: внутри абатсракции ты знаешь о конкретной реализации
        CloudCreator<TValue> DrawCloud();
        void SaveToFile(string filePath, ImageFormat format);
    }
}
