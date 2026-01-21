using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Models;
using AuroraJournalingApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Components.Cards
{
    public partial class AddJournalCard
    {
        [Inject]
        public JournalService journal { get; set; }

        [Inject]
        public NavigationManager nav { get; set; }

      
        bool editJournal = false;
        Journal existingJournal;

       

        protected override async Task OnInitializedAsync()
        {
            var checkEntry = await journal.GetJournalByDate(DateTime.Now.Date);
            if (checkEntry != null)
            {
                existingJournal = checkEntry;
                editJournal = true;

                // Populate form with existing data
                dto.Title = checkEntry.Title;
                dto.Content = checkEntry.Content ?? string.Empty;

                if (!string.IsNullOrEmpty(checkEntry.primaryMood))
                {
                    selectedMoods.Add(checkEntry.primaryMood);
                }
                if (!string.IsNullOrEmpty(checkEntry.secondaryMoods))
                {
                    var secs = checkEntry.secondaryMoods.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in secs) selectedMoods.Add(s.Trim());
                }

                if (!string.IsNullOrEmpty(checkEntry.tags))
                {
                    var t = checkEntry.tags.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var tag in t) selectedTags.Add(tag.Trim());
                }
            }
        }

        public async Task addJournal()
        {
            // Sync moods/tags to DTO if not bound directly
            dto.PrimaryMood = selectedMoods.FirstOrDefault();
            dto.SecondaryMoods = string.Join(",", selectedMoods.Skip(1));
            dto.Tags = string.Join(",", selectedTags);
            dto.Created = editJournal ? existingJournal.Created : DateTime.Now;

            try
            {
                if (editJournal)
                {
                    await journal.UpdateJournal(existingJournal.JournalId, dto);
                }
                else
                {
                    await journal.AddNewJournal(dto);
                }
                nav.NavigateTo("/history"); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task deleteJournal()
        {
            if (editJournal && existingJournal != null)
            {
                try
                {
                    await journal.DeleteJournal(existingJournal.JournalId);
                    nav.NavigateTo("/history");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
