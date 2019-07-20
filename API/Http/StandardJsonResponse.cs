namespace API.Http
{
    public class StandardJsonResponse
    {
        public StandardJsonResponse()
        {
        }

        public StandardJsonResponse(int status)
        {
            StatusCode = status;
        }

        public StandardJsonResponse(int status, dynamic data)
        {
            StatusCode = status;
            Data = data;
        }

        public int StatusCode { get; set; } = 200;
        public dynamic Data { get; set; } = null;
    }
}
