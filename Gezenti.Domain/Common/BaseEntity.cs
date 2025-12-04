namespace Gezenti.Domain.Common
{
    // Ortak alanlar (Id, CreatedAt, UpdatedAt)
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
