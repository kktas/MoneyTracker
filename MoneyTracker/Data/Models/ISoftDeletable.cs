namespace MoneyTracker.Data.Models
{
    public interface ISoftDeletable
    {
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }

    }
}
