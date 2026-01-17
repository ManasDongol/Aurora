using AuroraJournalingApp.Data;
using AuroraJournalingApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Repositories
{
    public class JournalRepository
    {
        SQLiteAsyncConnection db;
        public JournalRepository(AuroraDbContext _db)
        {
            db = _db._connect;
        }
        public async Task<List<Journal>> GetJournalsAsync()
        {
            return await db.Table<Journal>().ToListAsync();
        }
        public async Task<Journal> GetJournalById(string id)
        {
            return await db.Table<Journal>().FirstOrDefaultAsync(x => x.JournalId.Equals(id));
        }

        public async Task<Journal> AddJournal(Journal journal) 
        {
            await db.InsertAsync(journal);
            return journal;
        }
        public async Task<string> DeleteJournalByID(string id)
        {

            var Journal = await db
          .Table <Journal>()
          .FirstOrDefaultAsync(x => x.JournalId.Equals(id));

            if (Journal == null)
            {
                return "Couldn't delete (journal not found)";
            }

            await db.DeleteAsync(Journal);
            return $"Successfully deleted journal with ID: {id}";

        }
        public async Task<List<Journal>> GetJournalsByDateRange(DateTime start, DateTime end)
        {
            return await db.Table<Journal>()
        .Where(j => j.Created >= start && j.Created <= end)
        .OrderByDescending(j => j.Created)
        .ToListAsync();
        }

        public async Task<List<Journal>> GetJournalsByMood(string primarymood, string secondarymood)
        {
            return await db.Table<Journal>()
                .Where(J => J.primaryMood == primarymood || J.secondaryMoods == secondarymood)
                .OrderByDescending(j => j.Created) .ToListAsync();
        }

        public async Task<Journal> UpdateJournal(Journal val)
        {
            var journal = await db.FindAsync<Journal>(val.JournalId);
            if (journal == null) return null;

            journal.Title = val.Title;
            journal.Content = val.Content;
            journal.primaryMood = val.primaryMood;
            journal.secondaryMoods = val.secondaryMoods;
            journal.tags = val.tags;

             await db.UpdateAsync(journal);
             return journal;
        }
    }
}
