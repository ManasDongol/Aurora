using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Models;
using AuroraJournalingApp.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Components.Cards
{
    public  partial class AddJournalCard(JournalService journal,NavigationManager nav)
    {
     //   JournalDTO dto = new JournalDTO();
        public void addJournal()
        {
            JournalDTO journalobj = new JournalDTO
            {
                Content = RteValue,
                Title = dto.Title,
                Created = DateTime.Now,
                PrimaryMood = selectedMoods.FirstOrDefault(), // null if none
                SecondaryMoods = string.Join(",", selectedMoods.Skip(1).ToList()), // rest
                Tags = string.Join(",", selectedTags.ToList())
            };
            try
            {
                journal.addnewJournal(journalobj);
                nav.NavigateTo("/insights");
            }catch(Exception e)
            {
                nav.NavigateTo("/login");
            }
         

            
            
        }
    }
}
