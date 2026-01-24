using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuroraJournalingApp.Models;

namespace AuroraJournalingApp.Data
{
    public class AuroraDbContext
    {
        private const string DB_NAME = "AuroraAppV3.db3";
        public SQLiteAsyncConnection _connect { get; init; }
        public AuroraDbContext()
        {
            this._connect = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            

        }

        public async Task InitializeAsync()
        {
            await _connect.CreateTableAsync<User>();
            await _connect.CreateTableAsync<Journal>();
        }
    }
}
