﻿namespace CommonLibary.CommonModels
{
    using Newtonsoft.Json;

    public class ResponseModel
    {
        // Execution Status 1 --> Success -1 --> Failed.
        public int ExecutionStatus;

        public int ErrorStatus;

        public string ErrorMessage;

        [JsonIgnore]
        public dynamic ResponseData;
    }
}
