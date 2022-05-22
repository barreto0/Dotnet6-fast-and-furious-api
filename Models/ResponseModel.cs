namespace FastAndFuriousApi.Models
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Object Content { get; set; }
        public ResponseModel BuildOkResponse(string message, Object content)
        {
            return new ResponseModel
            {
                StatusCode = 200,
                Message = message,
                Content = content
            };
        }

        public ResponseModel BuildBadRequestResponse(string message, Object content)
        {
            return new ResponseModel
            {
                StatusCode = 400,
                Message = message,
                Content = content
            };
        }

        public ResponseModel BuildErrorResponse(string message, Object content)
        {
            return new ResponseModel
            {
                StatusCode = 500,
                Message = message,
                Content = content
            };
        }
    }
}