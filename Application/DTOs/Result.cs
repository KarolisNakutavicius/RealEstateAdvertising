namespace Application.DTOs
{
    public class Result<T>
    {
        public T? Data { get; set; }

        public bool Success { get; set; }

        public string Error { get; set; } = string.Empty;

        public static Result<T> Ok(T data)
        {         
            return new Result<T>() { Data = data, Success = true };
        }

        public static Result<T> Fail(string error)
        {
            return new Result<T>() { Success = false, Error = error };
        }
    }
}
