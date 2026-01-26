using AuroraJournalingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Components.Pages
{
    public partial class Journal(JournalService service)
    {
        bool entrystatus = false;
        protected override async Task OnInitializedAsync()
        {
           

            var checkEntry = await service.GetJournalByDate(DateTime.Now.Date);
            if (checkEntry != null)
            {
                entrystatus = true;
            }

        }


    }
}
