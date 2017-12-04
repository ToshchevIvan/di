namespace TagsCloudContainer.CloudObjects
{
	//TODO RV(atolstov): паттерн называется Factory а не Fabric
	public class BasicCloudObjectFabric<TValue> : ICloudObjectFabric<TValue>
    {
        public ICloudObject<TValue> Create(TValue value)
        {
            return new BasicCloudObject<TValue>(value, 0);
        }
    }
}
