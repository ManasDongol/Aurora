using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Models;
using AuroraJournalingApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Components.Pages
{
    public partial class Signin(UserService userservice)
    {
        private string Username;
        private string Password;
        private int Pin;
        private SigninDTO userModel = new SigninDTO();

        private async Task OnSubmit()
        {
            Debug.WriteLine("Submit clicked");
            try
            {
                userModel.username = Username;
                userModel.password = Password;
                userModel.pin = Pin;

                await userservice.RegisterUser(userModel);

                await SecureStorage.Default.SetAsync("Username", Username);
                await SecureStorage.Default.SetAsync("Pin", Pin.ToString());
                await SecureStorage.Default.SetAsync("Password", Password);

                Debug.WriteLine("User has been inserted successfully");
                navigate.NavigateTo("/login");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
