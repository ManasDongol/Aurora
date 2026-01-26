using AuroraJournalingApp.Data;
using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (_db == null)
                throw new Exception("AuroraDbContext is NULL");

            if (_db._connect == null)
                throw new Exception("_connect is NULL");

            db = _db._connect;
        }
        public async Task<List<Journal>> GetJournalsAsync()
        {
            return await db.Table<Journal>().ToListAsync();
        }
        public async Task<Journal> GetJournalById(string id)
        {
            var journalIDS = await db.Table<Journal>()
                         .OrderBy(j => j.Created) // sort for consistency
                         .ToListAsync();

            foreach (var j in journalIDS)
            {
                Debug.WriteLine(j.JournalId);
            }
            try
            {
                var journal =  await db.Table<Journal>()
                               .Where(j => j.JournalId == id)
                               .FirstOrDefaultAsync();
                return journal;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            
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

        public async Task<Journal> GetJournalByDate(DateTime today)
        {
            var start = today.Date;
            var end = start.AddDays(1);

            return await db.Table<Journal>()
                .Where(j => j.Created >= start && j.Created < end)
                .FirstOrDefaultAsync();
        }


        public async Task<List<Journal>> GetJournalsByMood(string primarymood, string secondarymood)
        {
            return await db.Table<Journal>()
                .Where(J => J.primaryMood == primarymood || J.secondaryMoods.Contains(secondarymood))
                .OrderByDescending(j => j.Created) .ToListAsync();
        }

        public async Task<List<Journal>> GetJournalsByTag(string tag)
        {
             return await db.Table<Journal>()
                .Where(J => J.tags.Contains(tag))
                .OrderByDescending(j => j.Created).ToListAsync();
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


        public async Task<List<Journal>> SearchAsync(SearchOptions opts)
        {
            var query = db.Table<Journal>();

            if (!string.IsNullOrWhiteSpace(opts.Content))
            {
                var lowerContent = opts.Content.ToLower();
                // Simple string search - check if title or content contains the search text
                // SQLite LINQ support for Contains is case-insensitive by default usually, but we rely on the provider.
                // Assuming standard SQLite-net behavior where Contains maps to LIKE %...%.
                query = query.Where(j => j.Title.Contains(opts.Content) || j.Content.Contains(opts.Content));
            }

            if (opts.From != null)
            {
                var fromDate = opts.From.Value;
                query = query.Where(j => j.Created >= fromDate);
            }

            if (opts.To != null)
            {
                var toDate = opts.To.Value;
                query = query.Where(j => j.Created <= toDate);
            }

            if (opts.Mood?.Any() == true)
            {
                // Filter mood in memory
                var interimResults = await query.ToListAsync();
                interimResults = interimResults.Where(j => opts.Mood.Contains(j.primaryMood)).ToList();
                
                // If we also have tags, filter them too
                if (opts.Tags?.Any() == true)
                {
                     interimResults = interimResults.Where(j => !string.IsNullOrEmpty(j.tags) && opts.Tags.Any(t => j.tags.Contains(t))).ToList();
                }

                return interimResults.OrderByDescending(j => j.Created).ToList();
            }
            
            if (opts.Tags?.Any() == true)
            {
                 var interimResults = await query.ToListAsync();
                 return interimResults.Where(j => !string.IsNullOrEmpty(j.tags) && opts.Tags.Any(t => j.tags.Contains(t))).OrderByDescending(j => j.Created).ToList();
            }

            return await query.OrderByDescending(j => j.Created).ToListAsync();
        }

        public async Task<List<Journal>> GetPages(PaginationDTO dto)
        {
            int pagesize = 5;
            int offset = pagesize * dto.pageIndex;
            return await db.Table<Journal>()
                           .OrderByDescending(j => j.Created)
                           .Skip(offset)
                           .Take(pagesize)
                           .ToListAsync();
        }
        

    }
}
