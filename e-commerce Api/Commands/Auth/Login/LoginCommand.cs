using MediatR;

namespace e_commerce_Api.Commands.Auth.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;

        public string ExpiresIn { get; set; } = "1 hour";
    }
}