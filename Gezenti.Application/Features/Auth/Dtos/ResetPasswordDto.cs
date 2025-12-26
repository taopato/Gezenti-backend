namespace Gezenti.Application.Features.Auth.Dtos
{
    public class ResetPasswordDto
    {
        public string Email { get; set; } = null!;
        public string ResetToken { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
