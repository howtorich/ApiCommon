namespace CommonLibary.CommonAdmin
{
    using CommonLibary.CommonDb;
    using System.Data;
    using System.Data.SqlClient;

    public class AdminKeysDa
    {
        public AdminKeysModels GetAdminKey(AdminKeyIds adminKeyIds)
        {
            AdminKeysModels adminKeysModels = null;

            SqlCommand command = SqlServerCommon.GetSpCommandByConnectToDb(SqlServerCommon.SqlServerDBs.DbAdmin, "usp_AdminKeys_Get");

            command.AddParameter("@KeyEnumID", SqlDbType.TinyInt, ((int)adminKeyIds).ToString());

            command.AddCommonInputParams();

            using (IDataReader reader = command.ExecuteReader())
            {
                adminKeysModels = new AdminKeysModels();

                if (reader.Read())
                {
                    adminKeysModels.Key = reader.GetDbStriing("KeyValue");
                    adminKeysModels.KeyName = reader.GetDbStriing("KeyName");
                    adminKeysModels.KeyDescription = reader.GetDbStriing("KeyDescription");
                    adminKeysModels.KeyId = reader.GetDbTinyInt("KeyEnumId");
                }
                else
                {
                    command.GetCommonOutputParams(adminKeysModels.responseModel);
                }
            }
            command.Connection.Close();

            return adminKeysModels;
        }
    }
}
