using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuroraJournalingApp.Models;

namespace AuroraJournalingApp.Data
{
    internal class AuroraDbContext
    {
        private const string DB_NAME = "AuroraApp.db3";
        public  SQLiteAsyncConnection _connect { get; init; }
        public AuroraDbContext()
        {
            _connect = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connect.CreateTableAsync<User>().Wait();
            _connect.CreateTableAsync<Journal>().Wait();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _connect.Table<User>().ToListAsync();
        }
        public async Task<User> GetCustomerByID(string id)
        {
            return await _connect.Table<User>().FirstOrDefaultAsync(x => x.UserID.Equals(id));
        }

        public async Task AddUser(User user)
        {
            await _connect.InsertAsync(user);
        }
        public async Task<string> DeleteUserByID(string id)
        {

            var customer = await _connect
          .Table<User>()
          .FirstOrDefaultAsync(x => x.UserID.Equals(id));

            if (customer == null)
            {
                return "Couldn't delete (user not found)";
            }

            await _connect.DeleteAsync(customer);
            return $"Successfully deleted user with ID: {id}";

        }

    }

}
