
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

            int UserPin= Convert.ToInt32(dto.pin);
            User newUser = new User
            {
                Username=dto.username,
                PasswordHash=dto.password,
                pin = UserPin
            };
            
            await userRepo.AddUser(newUser);
        }
    }
}
