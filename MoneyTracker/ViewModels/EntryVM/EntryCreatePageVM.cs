using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.ViewModels.EntryVM
{
    public class EntryCreatePageVM
    {
        public CreateEntryVM CreateEntryVM { get; set; }
        public SelectList EntryTypes { get; set; }
        public SelectList Currencies { get; set; }
    }

    public class CreateEntryVM
    {
        [Required]
        [DisplayName("İşlem Türü")]
        public int EntryTypeId { get; set; }
        [Required]
        [DisplayName("Döviz Birimi")]
        public int CurrencyId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        [DisplayName("Miktar")]
        public double Amount { get; set; }
        [Required]
        [MinLength(3)]
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Tarih")]
        public DateTime At { get; set; }
    }
}
