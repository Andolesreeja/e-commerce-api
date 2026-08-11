using e_commerce_Api.Data;
using e_commerce_Api.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_Api.Commands.Auth.Login
{
    public class LoginCommandHandler
        : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly JwtService _jwtService;

        public LoginCommandHandler(
            AppDbContext context,
            PasswordService passwordService,
            JwtService jwtService)
        {
            _context = context;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            // Find user
            var user = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Username == request.Username,
                    cancellationToken);

            // User not found
            if (user == null)
            {
                throw new UnauthorizedAccessException(
                    "Invalid username or password");
            }

            // Verify password
            var passwordValid = _passwordService.VerifyPassword(
                request.Password,
                user.PasswordHash);

            if (!passwordValid)
            {
                throw new UnauthorizedAccessException(
                    "Invalid username or password");
            }

            // Generate JWT
            var token = await _jwtService.GenerateToken(
                user.Username);

            return new LoginResponse
            {
                Token = token,
                ExpiresIn = "1 hour"
            };
        }
    }
}