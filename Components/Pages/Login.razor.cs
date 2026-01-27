using Microsoft.Maui.ApplicationModel.DataTransfer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuroraJournalingApp.Components.Pages
{
    public partial class Login
    {
        private bool isDark { get; set; }
        protected async override void OnInitialized()
        {
           
            var oauthToken = await SecureStorage.Default.GetAsync("registered");
            var user = await SecureStorage.Default.GetAsync("username");
            var value = await SecureStorage.GetAsync("darkmode");

            isDark = value == "true";
            await ApplyTheme();

        
            if (oauthToken == null && user == null)
            {
             
                navigation.NavigateTo("/signup");
            }
        }

      
    }
}
