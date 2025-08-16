namespace E_commerce_Core.ApiRespones
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }         
        public string Message { get; set; }         
        public T Data { get; set; }                  
        public bool Success => StatusCode >= 200 && StatusCode < 300;

        public int? TotalPages { get; set; }

        public ApiResponse() { }

        public ApiResponse(int statusCode, string message = null, T data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

   
        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation completed successfully", int statusCode = 200)
        {
            return new ApiResponse<T>(statusCode, message, data);
        }

        public static ApiResponse<T> ErrorResponse(string message, int statusCode = 400)
        {
            return new ApiResponse<T>(statusCode, message, default);
        }
    }
}
