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
        private const string DB_NAME = "AuroraApp.db3";
        public SQLiteAsyncConnection _connect { get; init; }
        public AuroraDbContext()
        {
            _connect = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connect.CreateTableAsync<User>().Wait();
            _connect.CreateTableAsync<Journal>().Wait();
        }
    }
}
