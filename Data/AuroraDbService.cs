using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuroraJournalingApp.Domain.Models;

namespace AuroraJournalingApp.Data
{
    internal class AuroraDbService
    {
        private const string DB_NAME = "Aurora.db3";
        private SQLiteAsyncConnection _connect;
        public AuroraDbService()
        {
            _connect = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connect.CreateTableAsync<User>();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _connect.Table<User>().ToListAsync();
        }
        public async Task<User> GetCustomerByID(Guid id)
        {
            return await _connect.Table<User>().FirstOrDefaultAsync(x => x.UserID == id);
        }

        public async Task AddCustomer(User user)
        {
            await _connect.InsertAsync(user);
        }
        public async Task<string> DeleteCustomerByID(Guid id)
        {

            var customer = await _connect
          .Table<User>()
          .FirstOrDefaultAsync(x => x.UserID == id);

            if (customer == null)
            {
                return "Couldn't delete (user not found)";
            }

            await _connect.DeleteAsync(customer);
            return $"Successfully deleted user with ID: {id}";

        }

    }

}
