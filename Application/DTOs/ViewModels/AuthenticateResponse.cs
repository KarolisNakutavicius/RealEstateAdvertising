﻿namespace Application.DTOs.ViewModels;

public class AuthenticateResponse
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expiration { get; set; } = new DateTime();
}