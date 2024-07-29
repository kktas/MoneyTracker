using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MoneyTracker.Data.Services
{
    public class CurrencyService(ApplicationDbContext context)
    {
        public async Task<SelectList> GetSelectList()
        {
            return new SelectList(
                await context.Currencies.OrderBy(c => c.Id)
                    .OrderBy(c => c.Id)
                    .Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = $"{c.Name} ({c.Symbol})" })
                    .ToListAsync(),
                "Value", "Text");
        }
    }
}
