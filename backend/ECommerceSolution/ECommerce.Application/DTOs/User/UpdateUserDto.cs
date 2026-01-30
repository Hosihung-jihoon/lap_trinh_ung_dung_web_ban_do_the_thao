using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.User
{
    public class UpdateUserDto
    {
        [Required]
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? AvatarUrl { get; set; }
    }
}