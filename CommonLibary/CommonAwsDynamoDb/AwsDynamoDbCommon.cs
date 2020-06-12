namespace CommonLibary.CommonAwsDynamoDb
{
    using Amazon;
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DocumentModel;
    using CommonLibary.CommonAdmin;
    using System;
    using System.Threading.Tasks;

    public class AwsDynamoDbCommon
    {
        public enum AwsDynamoDbTables
        {
            tbl_ChatRegistration_Users = 100,
        }

        public AmazonDynamoDBClient AwsConnection()
        {
            AmazonDynamoDBClient awsDynamoDbInstance = null;

            try
            {
                //  RegionEndpoint..GetBySystemName("ap-south-1")
                awsDynamoDbInstance = new AmazonDynamoDBClient(GetAdminKeyString(AdminKeyIds.DynamoDbAccessKey), GetAdminKeyString(AdminKeyIds.DynamoDbScrectKey), RegionEndpoint.APSouth1);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return awsDynamoDbInstance;
        }

        public string GetAdminKeyString(AdminKeyIds adminKeyIds)
        {
            AdminKeysModels AdminKeysModels;
            AdminKeysDa adminKeysDa = new AdminKeysDa();
            AdminKeysModels = adminKeysDa.GetAdminKey(adminKeyIds);

            if (AdminKeysModels.responseModel != null && AdminKeysModels.responseModel.ExecutionStatus == -1)
            {
                return string.Empty;
            }

            return AdminKeysModels?.Key;
        }

        public async Task<Document> GetItemOnPrimaryKeyString(string primaryKey, string TableName)
        {
            AmazonDynamoDBClient awsDynamoDbInstance = await Task.FromResult(AwsConnection());

            Table UserRegistrationTable = Table.LoadTable(awsDynamoDbInstance, TableName);

            return await UserRegistrationTable.GetItemAsync(primaryKey);
        }

        public async Task<Document> PutItemInTable(Document ItemRecord, string TableName)
        {
            AmazonDynamoDBClient awsDynamoDbInstance = await Task.FromResult(AwsConnection());

            Table UserRegistrationTable = Table.LoadTable(awsDynamoDbInstance, TableName);

            return await UserRegistrationTable.PutItemAsync(ItemRecord);
        }
    }
}
