using AuroraJournalingApp.Models;
using AuroraJournalingApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AuroraJournalingApp.Components.Cards
{

    public partial class EntryJournalCard(JournalService service)
    {
        [Inject]
        public NavigationManager nav { get; set; }

        Journal todayEntry=new();
        protected override async Task OnInitializedAsync()
        {
            todayEntry = await service.GetJournalByDate(DateTime.Now.Date);
            

        }

        public  async Task deleteAsync()
        {
            if (todayEntry == null) return;
            try
            {
                LoadingService.Show();
                await service.DeleteJournal(todayEntry.JournalId);
                await ToastService.ShowSuccess("Journal deleted successfully");
                todayEntry = null;


                StateHasChanged();
            }
            catch
            {
                await ToastService.ShowError("failed to delete journal");
            }
            finally
            {
                LoadingService.Hide();
            }

           

          
           



        }
       

    }
}
