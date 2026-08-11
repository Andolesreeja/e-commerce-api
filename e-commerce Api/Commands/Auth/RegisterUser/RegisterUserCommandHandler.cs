using e_commerce_Api.Data;
using e_commerce_Api.Models;
using e_commerce_Api.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_Api.Commands.Auth.RegisterUser
{
    public class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;

        public RegisterUserCommandHandler(
            AppDbContext context,
            PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<RegisterUserResponse> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            // Check whether username already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Username == request.Username,
                    cancellationToken);

            if (existingUser != null)
            {
                throw new Exception("Username already exists");
            }

            // Hash password
            var passwordHash = _passwordService.HashPassword(
                request.Password);

            // Create user
            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);

            return new RegisterUserResponse
            {
                Message = "User registered successfully",
                Username = user.Username
            };
        }
    }
}