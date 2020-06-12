namespace CommonLibary.CommonDb
{
    using CommonLibary.CommonModels;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public static class SqlServerCommon
    {
        private static string HostName = "awsdatabase-1.cklc9dvnkg9x.ap-south-1.rds.amazonaws.com,1433";
        private static string DbUserName = "MainUser";
        private static string DbPassWord = "V4wearefour";

        public enum SqlServerDBs
        {
            DbAdmin = 101,

            SampleDb = 102,
        }

        public static string GetSqlServerConnectionString(SqlServerDBs DbToConnect)
        {
            string DbConnetionString = string.Empty;
            switch (DbToConnect)
            {
                case SqlServerDBs.DbAdmin:
                    DbConnetionString = "Server=" + HostName + ";User Id=" + DbUserName + ";pwd=" + DbPassWord + ";Database=" + DbToConnect.ToString();
                    break;
                case SqlServerDBs.SampleDb:
                    new Exception("No Db like " + DbToConnect.ToString());
                    break;
                default:
                    DbConnetionString = "Server=" + HostName + ";User Id=" + DbUserName + ";pwd=" + DbPassWord + ";Database=" + DbToConnect.ToString();
                    break;
            }
            return DbConnetionString;
        }

        public static SqlCommand GetSpCommandByConnectToDb(SqlServerDBs DbToConnect, string SpName)
        {
            SqlConnection sqlConnection = new SqlConnection(GetSqlServerConnectionString(SqlServerDBs.DbAdmin).ToString());

            sqlConnection.Open();

            return new SqlCommand(SpName, sqlConnection)
            {
                CommandType = System.Data.CommandType.StoredProcedure,
            };
        }

        public static void AddParameter(this SqlCommand commad, string ParamName, SqlDbType DbType, string inputValue, int size = 0, ParameterDirection ParamDirection = ParameterDirection.Input)
        {
            if (size == 0)
            {
                commad.Parameters.Add(new SqlParameter(ParamName, DbType));
            }
            else
            {
                commad.Parameters.Add(new SqlParameter(ParamName, DbType, size));
            }
            commad.Parameters[ParamName].Value = inputValue;
            commad.Parameters[ParamName].Direction = ParamDirection;
        }

        public static void AddCommonInputParams(this SqlCommand commad)
        {
            commad.Parameters.Add(new SqlParameter("@return", SqlDbType.Int));
            commad.Parameters["@return"].Value = DBNull.Value;
            commad.Parameters["@return"].Direction = ParameterDirection.InputOutput;

            commad.Parameters.Add(new SqlParameter("@errorID", SqlDbType.Int));
            commad.Parameters["@errorID"].Value = DBNull.Value;
            commad.Parameters["@errorID"].Direction = ParameterDirection.InputOutput;

            commad.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.VarChar, 2048));
            commad.Parameters["@errorMessage"].Value = DBNull.Value;
            commad.Parameters["@errorMessage"].Direction = ParameterDirection.InputOutput;
        }

        public static void GetCommonOutputParams(this SqlCommand commad, ResponseModel responseModel)
        {
            if (responseModel == null)
            {
                responseModel = new ResponseModel();
            }
            responseModel.ExecutionStatus = commad.Parameters["@return"]?.Value != DBNull.Value ? (int)commad.Parameters["@return"].Value : -1;
            responseModel.ErrorStatus = commad.Parameters["@errorID"]?.Value != DBNull.Value ? (int)commad.Parameters["@errorID"].Value : -1;
            responseModel.ErrorMessage = commad.Parameters["@errorMessage"]?.Value != DBNull.Value ? commad.Parameters["@errorMessage"].Value.ToString() : string.Empty;
        }

        public static string GetDbStriing(this IDataReader reader, string ColumnName, string defaultValue = "")
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? defaultValue : reader[reader.GetOrdinal(ColumnName)].ToString();
        }

        public static Int16 GetDbInt16(this IDataReader reader, string ColumnName, Int16 defaultValue = 0)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? defaultValue : reader.GetInt16(reader.GetOrdinal(ColumnName));
        }

        public static Int32 GetDbInt32(this IDataReader reader, string ColumnName, Int32 defaultValue = 0)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? defaultValue : reader.GetInt32(reader.GetOrdinal(ColumnName));
        }

        public static Int16 GetDbTinyInt(this IDataReader reader, string ColumnName, Int16 defaultValue = 0)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? defaultValue : reader.GetByte(reader.GetOrdinal(ColumnName));
        }

        public static Int64 GetDbInt64(this IDataReader reader, string ColumnName, Int64 defaultValue = 0)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? defaultValue : reader.GetInt64(reader.GetOrdinal(ColumnName));
        }

        public static bool GetDbInt64(this IDataReader reader, string ColumnName, bool defaultValue = false)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? defaultValue : reader.GetBoolean(reader.GetOrdinal(ColumnName));
        }

        public static double GetDbInt64(this IDataReader reader, string ColumnName, double defaultValue = 0.0)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? defaultValue : reader.GetDouble(reader.GetOrdinal(ColumnName));
        }
    }
}
