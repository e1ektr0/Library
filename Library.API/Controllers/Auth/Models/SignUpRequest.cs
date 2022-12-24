﻿namespace Library.API.Controllers.Auth.Models;

public class SignUpRequest
{
    public string Password { get; init; } = null!;
    public string UserName { get; init; } = null!;
}