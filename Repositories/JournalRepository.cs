using AuroraJournalingApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Repositories
{
    public class JournalRepository()
    {
        SQLiteAsyncConnection db;
        public async Task<List<Journal>> GetJournalsAsync()
        {
            return await db.Table<Journal>().ToListAsync();
        }
        public async Task<Journal> GetJournalsbyId(string id)
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
    }
}
