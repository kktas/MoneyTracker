using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyTracker.Data.Models;
using MoneyTracker.Data.Services;
using MoneyTracker.ViewModels.EntryVM;
using System.Security.Claims;

namespace MoneyTracker.Controllers
{
    [Authorize]
    [Route("")]
    public class EntryController(
        IHttpContextAccessor httpContextAccessor,
        CurrencyService currencyService,
        EntryService entryService,
        EntryTypeService entryTypeService
    ) : Controller
    {
        [HttpGet("")]
        public async Task<IActionResult> Index([FromQuery] EntryIndexPageSearchOptionsVM searchOptions)
        {
            var startAt = searchOptions.StartAt;
            var endAt = searchOptions.EndAt;
            DateTime now = DateTime.Now;
            startAt = DateTime.Compare(startAt.ToDateTime(TimeOnly.MinValue), DateTime.MinValue) > 0 ? startAt : new DateOnly(now.Year, now.Month, 1);
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            List<Entry> entries = await entryService.GetEntriesForUser(userId, startAt, endAt);
            SelectList currencies = await currencyService.GetSelectList();
            SelectList entryTypes = await entryTypeService.GetSelectList();

            var viewModel = new EntryIndexPageVM()
            {
                Entries = entries,
                Currencies = currencies,
                EntryTypes = entryTypes,
                SearchOptions = new() { StartAt = startAt, EndAt = endAt }
            };

            return View(viewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            SelectList currencies = await currencyService.GetSelectList();
            SelectList entryTypes = await entryTypeService.GetSelectList();

            var viewModel = new EntryCreatePageVM()
            {
                Currencies = currencies,
                EntryTypes = entryTypes
            };

            return View(viewModel);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateEntryVM createEntryVM)
        {
            if (!ModelState.IsValid)
            {
                SelectList currencies = await currencyService.GetSelectList();
                SelectList entryTypes = await entryTypeService.GetSelectList();

                var viewModel = new EntryCreatePageVM()
                {
                    Currencies = currencies,
                    EntryTypes = entryTypes,
                    CreateEntryVM = createEntryVM
                };
                return View(viewModel);
            }
            createEntryVM.At = DateTime.SpecifyKind(createEntryVM.At, DateTimeKind.Utc);

            await entryService.CreateAsync(new Entry()
            {
                UserId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)),
                EntryTypeId = createEntryVM.EntryTypeId,
                CurrencyId = createEntryVM.CurrencyId,
                Amount = createEntryVM.Amount,
                Description = createEntryVM.Description,
                At = createEntryVM.At.ToUniversalTime(),
            });

            return LocalRedirect("/money-tracker");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await entryService.DeleteAsync(id);

            return Ok();
        }
    }
}
