using Application.DTOs.ViewModels;

namespace Application.DTOs;

public class Result
{
    public IList<ErrorResponse> Errors { get; set; } = new List<ErrorResponse>();
    
    public bool Success { get; set; }
    
    public static Result Ok()
    {
        return new Result {Success = true };
    }

    public static Result Fail(string error)
    {
        return new Result { Success = false, Errors = new List<ErrorResponse> { new() { Error = error } } };
    }

    public static Result Fail(IList<string> errors)
    {
        return new Result { Success = false, Errors = errors.Select(e => new ErrorResponse { Error = e }).ToList() };
    }
}