using Application.DTOs.ViewModels;
using System.Net;
using System.Text.Json.Serialization;

namespace Application.DTOs
{
    public class Result<T>
    {
        public T? Data { get; set; }

        public bool Success { get; set; }

        public IList<ErrorResponse> Errors { get; set; } = new List<ErrorResponse>();

        public static Result<T> Ok(T data)
        {         
            return new Result<T>() { Data = data, Success = true };
        }

        public static Result<T> Fail(string error)
        {
            return new Result<T>() { Success = false, Errors = new List<ErrorResponse>() { new ErrorResponse { Error = error } } };
        }

        public static Result<T> Fail(IList<string> errors)
        {
            return new Result<T>() { Success = false, Errors = errors.Select(e => new ErrorResponse { Error = e }).ToList() };
        }
    }
}
