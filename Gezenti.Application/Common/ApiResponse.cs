namespace Gezenti.Application.Common
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }

        public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;

        public static ApiResponse<T> Success(T data, int statusCode = 200, string? message = null)
        {
            return new ApiResponse<T>
            {
                Data = data,
                StatusCode = statusCode,
                Message = message
            };
        }

        public static ApiResponse<T> Fail(string error, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Error = error,
                StatusCode = statusCode
            };
        }
    }
}
