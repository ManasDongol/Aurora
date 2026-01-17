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
    public class UserRepository(SQLiteAsyncConnection _db)
    {
     

        public async Task<List<User>> GetUsersAsync()
        {
            return await _db.Table<User>().ToListAsync();
        }
        public async Task<User> GetUserbyId(string id)
        {
            return await _db.Table<User>().FirstOrDefaultAsync(x => x.UserID.Equals(id));
        }

        public async Task AddUser(User user)
        {
            await _db.InsertAsync(user);
        }
        public async Task<string> DeleteUserByID(string id)
        {

            var customer = await _db
          .Table<User>()
          .FirstOrDefaultAsync(x => x.UserID.Equals(id));

            if (customer == null)
            {
                return "Couldn't delete (user not found)";
            }

            await _db.DeleteAsync(customer);
            return $"Successfully deleted user with ID: {id}";

        }
    }
}
