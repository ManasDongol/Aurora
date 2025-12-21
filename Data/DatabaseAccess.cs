using AuroraJournalingApp.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace AuroraJournalingApp.Data
{
   
     public class DatabaseAccess
    {
        SQLiteAsyncConnection database;


        async Task Init()
        {
            if (database is not null)
                return;

            database = new SQLiteAsyncConnection(SQLiteConfig.DatabasePath, SQLiteConfig.Flags);
            var result = await database.CreateTableAsync<User>();

            await database.CreateTableAsync<Journal>();
            await database.CreateTableAsync<JournalMoods>();
            await database.CreateTableAsync<Mood>();
            await database.CreateTableAsync<Tag>();

        }


        async Task<User> GetUser(string username)
        {
            await Init();
            return await database.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
        }

        async Task<Boolean> CheckUserNameExists(string username)
        {
            await Init();
            var result = await database.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            return true;

        }
    }
}
