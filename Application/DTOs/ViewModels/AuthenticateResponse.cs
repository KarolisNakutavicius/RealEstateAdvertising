namespace Application.DTOs.ViewModels
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
