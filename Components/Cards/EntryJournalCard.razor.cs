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

        Journal todayEntry = new Journal
        {
            Title = "",
            Content = ""
        };
        protected override async Task OnInitializedAsync()
        {
            var loaded = await service.GetJournalByDate(DateTime.Now.Date);

            if (loaded != null)
            {
                todayEntry.JournalId = loaded.JournalId;
                todayEntry.Title = loaded.Title;
                todayEntry.Content = loaded.Content;
            }


        }

        public  async Task deleteAsync()
        {
            if (todayEntry == null) return;
            try
            {
                LoadingService.Show();
                await service.DeleteJournal(todayEntry.JournalId);
                await ToastService.ShowSuccess("Journal deleted successfully");
                todayEntry.JournalId = "";
                todayEntry.Title = "";
                todayEntry.Content = "";
                todayEntry.secondaryMoods = "";
                todayEntry.primaryMood = "";


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
