using Microsoft.Maui.ApplicationModel.DataTransfer;
using System;
using System.Collections.Generic;
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
            string oauthToken = await SecureStorage.Default.GetAsync("loggedin");
            var value = await SecureStorage.GetAsync("darkmode");

            isDark = value == "true";
            await ApplyTheme();
        

       

            if (oauthToken == null)
            {
                navigation.NavigateTo("/signup");
            }
        }

      
    }
}
