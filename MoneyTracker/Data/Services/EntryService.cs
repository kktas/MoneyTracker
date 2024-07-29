using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data.Models;

namespace MoneyTracker.Data.Services
{
    public class EntryService(ApplicationDbContext context)
    {
        public async Task<List<Entry>> GetEntriesForUser(int userId, DateOnly startAt, DateOnly? endAt)
        {
            return await context.Entries
               .Include(e => e.EntryType)
               .Include(e => e.Currency)
               .Where(e => e.UserId == userId
                   && e.At >= startAt.ToDateTime(TimeOnly.MinValue).ToUniversalTime()
                   && (endAt == null || ((DateOnly)endAt).ToDateTime(TimeOnly.MinValue).ToUniversalTime() >= e.At)
               )
               .OrderByDescending(e => e.At)
               .ThenByDescending(e => e.CreatedAt)
               .ToListAsync();
        }

        public async Task<Entry> CreateAsync(Entry entry)
        {
            await context.Entries.AddAsync(entry);
            //return await context.Entries.FindAsync(entry.Id);
            await context.SaveChangesAsync(new CancellationToken());
            return entry;
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await context.Entries.FindAsync(id) ?? throw new Exception("Kayıt Bulunamadı!");
            context.Entries.Remove(entry);
            await context.SaveChangesAsync(new CancellationToken());
        }
    }
}
