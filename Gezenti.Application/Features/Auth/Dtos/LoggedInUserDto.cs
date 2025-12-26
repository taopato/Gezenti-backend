namespace Gezenti.Application.Features.Auth.Dtos
{
    public class LoggedInUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
    }
}
