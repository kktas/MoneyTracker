using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data.Models;

namespace MoneyTracker.Data.Services
{
    public class EntryTypeService(ApplicationDbContext context)
    {
        public async Task<SelectList> GetSelectList()
        {
            return new SelectList(
                    await context.EntryTypes
                        .OrderBy(et => et.Id)
                        .Select(et => new SelectListItem() { Value = et.Id.ToString(), Text = et.Name })
                        .ToListAsync(),
                "Value", "Text");
        }
    }
}
