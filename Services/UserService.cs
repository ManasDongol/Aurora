
using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Models;
using AuroraJournalingApp.Repositories;



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AuroraJournalingApp.Services
{
    public class UserService(UserRepository userRepo)
    {
        public async Task RegisterUser(SigninDTO dto)
        {

         
            User newUser = new User
            {
                Username=dto.username,
                PasswordHash=dto.password,
                email = dto.email
            };
            
            await userRepo.AddUser(newUser);
        }

        public async Task<bool> UpdatePassword(string email, string newPassword)
        {
            var user = await userRepo.GetUserByEmail(email);
            if (user == null) return false;
            
            user.PasswordHash = newPassword;
            await userRepo.UpdateUser(user);
            return true;
        }
    }
}
