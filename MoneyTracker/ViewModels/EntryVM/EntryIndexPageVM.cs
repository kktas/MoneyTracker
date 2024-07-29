using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyTracker.Data.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.ViewModels.EntryVM
{
    public class EntryIndexPageVM
    {
        public List<Entry> Entries { get; set; }
        public SelectList EntryTypes { get; set; }
        public SelectList Currencies { get; set; }

        public EntryIndexPageSearchOptionsVM SearchOptions { get; set; }
    }

    public class EntryIndexPageSearchOptionsVM()
    {
        [DisplayName("Başlangıç")]
        [DataType(DataType.Date)]
        public DateOnly StartAt { get; set; }

        [DisplayName("Bitiş")]
        [DataType(DataType.Date)]
        public DateOnly? EndAt { get; set; }
    }
}
