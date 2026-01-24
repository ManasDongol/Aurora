using AuroraJournalingApp.Models;
using AuroraJournalingApp.Services;
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
        Journal todayEntry=new();
        protected override async Task OnInitializedAsync()
        {
            todayEntry = await service.GetJournalByDate(DateTime.Now.Date);
            

        }

        public  async Task deleteAsync()
        {
             await service.DeleteJournal(todayEntry.JournalId);

        }
       

    }
}
