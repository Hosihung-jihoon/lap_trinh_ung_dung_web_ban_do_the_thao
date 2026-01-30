using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.Helpers;
using ECommerce.Application.Services.Interfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly JwtSettings _jwtSettings;
        private readonly GoogleSettings _googleSettings;

        // In-memory storage for refresh tokens (trong production nên dùng Redis hoặc database)
        private static readonly Dictionary<int, string> _refreshTokens = new();
        private static readonly Dictionary<string, DateTime> _resetTokens = new();

        public AuthService(
            IUnitOfWork unitOfWork,
            ITokenService tokenService,
            IEmailService emailService,
            IOptions<JwtSettings> jwtSettings,
            IOptions<GoogleSettings> googleSettings)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
            _googleSettings = googleSettings.Value;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // Check if email already exists
            var existingUser = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            // Create new user
            var user = new User
            {
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "Customer",
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Send welcome email (không await để không block)
            _ = _emailService.SendWelcomeEmailAsync(user.Email, user.FullName);

            // Generate tokens
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Store refresh token
            _refreshTokens[user.Id] = refreshToken;

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password");
            }

            if (user.IsLocked)
            {
                throw new Exception("Account is locked. Please contact support.");
            }

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _refreshTokens[user.Id] = refreshToken;

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes)
            };
        }

        public async Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginDto googleLoginDto)
        {
            try
            {
                // Verify Google token
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginDto.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _googleSettings.ClientId }
                });

                // Check if user exists
                var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.GoogleId == payload.Subject || u.Email == payload.Email);

                if (user == null)
                {
                    // Create new user from Google
                    user = new User
                    {
                        Email = payload.Email,
                        FullName = payload.Name,
                        GoogleId = payload.Subject,
                        AvatarUrl = payload.Picture,
                        Role = "Customer",
                        CreatedAt = DateTime.UtcNow
                    };

                    await _unitOfWork.Users.AddAsync(user);
                    await _unitOfWork.SaveChangesAsync();
                }
                else if (string.IsNullOrEmpty(user.GoogleId))
                {
                    // Link existing account with Google
                    user.GoogleId = payload.Subject;
                    _unitOfWork.Users.Update(user);
                    await _unitOfWork.SaveChangesAsync();
                }

                if (user.IsLocked)
                {
                    throw new Exception("Account is locked");
                }

                var accessToken = _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();

                _refreshTokens[user.Id] = refreshToken;

                return new AuthResponseDto
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes)
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid Google token: " + ex.Message);
            }
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var userEntry = _refreshTokens.FirstOrDefault(x => x.Value == refreshToken);
            if (userEntry.Equals(default(KeyValuePair<int, string>)))
            {
                throw new Exception("Invalid refresh token");
            }

            var user = await _unitOfWork.Users.GetByIdAsync(userEntry.Key);
            if (user == null || user.IsLocked)
            {
                throw new Exception("User not found or locked");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            _refreshTokens[user.Id] = newRefreshToken;

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes)
            };
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == forgotPasswordDto.Email);
            if (user == null)
            {
                // Don't reveal that user doesn't exist
                return true;
            }

            // Generate reset token
            var resetToken = GenerateResetToken();
            _resetTokens[resetToken] = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour

            // Send reset email
            await _emailService.SendPasswordResetEmailAsync(user.Email, resetToken);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            // Validate reset token
            if (!_resetTokens.TryGetValue(resetPasswordDto.Token, out var expiryTime) || DateTime.UtcNow > expiryTime)
            {
                throw new Exception("Invalid or expired reset token");
            }

            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == resetPasswordDto.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Update password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            // Remove used token
            _resetTokens.Remove(resetPasswordDto.Token);

            return true;
        }

        public async Task LogoutAsync(int userId)
        {
            _refreshTokens.Remove(userId);
            await Task.CompletedTask;
        }

        private string GenerateResetToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber).Replace("+", "").Replace("/", "").Replace("=", "");
        }
    }
}