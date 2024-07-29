namespace MoneyTracker.Data.Models
{
    public class Entry : ISoftDeletable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int EntryTypeId { get; set; }
        public EntryType EntryType { get; set; }
        public double Amount { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public DateTime At { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
    }
}
