using TagsCloudContainer.WordsSources;


namespace TagsCloudContainer.CloudCreators
{
    public interface ICloudCreator
    {
        ICloudCreator PlaceWords();
        ICloudCreator SaveToFile(string filePath);
    }
}
