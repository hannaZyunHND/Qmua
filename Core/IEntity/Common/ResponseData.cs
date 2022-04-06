using System.Collections.Generic;

namespace MI.Entity.Common
{
    public class ResponseData
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<object> ListData { get; set; }
        public int TotalRow { get; set; }
        public int ErrorCode { get; set; }
        public string Token { get; set; }
        public string LinkImage { get; set; }
        public ResponseData()
        {
            this.Success = false;
            this.Message = "";
            this.Data = null;
            this.TotalRow = 0;
            this.ErrorCode = 0;
            this.ListData = null;
            this.Id = 0;
        }
        public ResponseData(object data, List<object> listData, bool isSuccess = true, int totalRows = 0, string message = "", int errorCode = 0)
        {
            Success = isSuccess;
            Data = data;
            TotalRow = totalRows;
            Message = message;
            ErrorCode = errorCode;
            ListData = listData;
        }
        public ResponseData(string message, bool isSuccess = false)
        {
            Success = isSuccess;
            Message = message;
        }
    }
    public class ResponseSuports
    {
        public List<object> ListStatus { get; set; }
        public List<object> ListTypes { get; set; }
    }

    public class ResponseTwoList
    {
        public List<object> ListObj1 { get; set; }
        public List<object> ListObj2 { get; set; }
    }

    public class ResponsePaging
    {
        public int Total { get; set; }
        public List<object> ListData { get; set; }
    }
}
