using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.Auth
{
    public class GoogleLoginDto
    {
        [Required]
        public string IdToken { get; set; } = string.Empty;
    }
}