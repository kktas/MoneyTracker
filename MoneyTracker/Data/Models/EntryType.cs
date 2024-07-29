namespace MoneyTracker.Data.Models
{
    public class EntryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
