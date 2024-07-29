﻿namespace MoneyTracker.Data.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
