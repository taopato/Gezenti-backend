using Gezenti.Domain.Common;
using Gezenti.Domain.Enums;

namespace Gezenti.Domain
{
    public class User : BaseEntity
    {
        // Ad
        public string FirstName { get; set; } = null!;

        // Soyad
        public string LastName { get; set; } = null!;

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
        //Cinsiyet
        public Gender Gender { get; set; } = Gender.Unknown;

        public DateTime? BirthDate { get; set; }
        public string? City { get; set; }
    }
}
