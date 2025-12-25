using Gezenti.Domain.Common;

namespace Gezenti.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; } = null!;
        public string UserGmail { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        public DateTime? LastLoginDate { get; set; }

        public ICollection<UserComment> UserComments { get; set; } = new List<UserComment>();
        public ICollection<UserFavorite> UserFavorites { get; set; } = new List<UserFavorite>();
        public ICollection<UserInteraction> UserInteractions { get; set; } = new List<UserInteraction>();
        public UserPreference? UserPreference { get; set; }
        public ICollection<UserVisitHistory> UserVisitHistory { get; set; } = new List<UserVisitHistory>();
    }
}
