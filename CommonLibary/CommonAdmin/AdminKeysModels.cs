namespace CommonLibary.CommonAdmin
{
    using CommonLibary.CommonModels;

    public class AdminKeysModels
    {
        public string Key;

        public string Value;

        public string KeyName;

        public string KeyDescription;

        public int KeyId;

        public ResponseModel responseModel;
    }

    public enum AdminKeyIds
    {
        DynamoDbScrectKey = 3,
        DynamoDbAccessKey = 4,
    }
}
