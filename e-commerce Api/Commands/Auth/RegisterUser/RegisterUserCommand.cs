using MediatR;

namespace e_commerce_Api.Commands.Auth.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterUserResponse>
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class RegisterUserResponse
    {
        public string Message { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;
    }
}