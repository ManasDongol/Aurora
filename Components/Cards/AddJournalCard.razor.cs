using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Models;
using AuroraJournalingApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        bool isLoaded;



        protected override async Task OnInitializedAsync()
        {
            //checking if user has already entered a journal
            var checkEntry = await journal.GetJournalByDate(DateTime.Now.Date);

           //if user already has an entry
            if (checkEntry != null)
            {
                //Initializing all the card/form variables
                existingJournal = checkEntry;
                editJournal = true;

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
            isLoaded = true;
        }

        public async Task addJournal()
        {
            
           
            dto.PrimaryMood = selectedMoods.FirstOrDefault();
            dto.SecondaryMoods = string.Join(",", selectedMoods.Skip(1));
            dto.Tags = string.Join(",", selectedTags);
            dto.Created = editJournal ? existingJournal.Created : DateTime.Today;
            
            try
            {
                if (editJournal)
                {
      
                    try
                    {
                        LoadingService.Show();
                        await journal.UpdateJournal(existingJournal.JournalId, dto);
                        await ToastService.ShowSuccess("Journal edited successfully");
                    }
                    catch
                    {
                        await ToastService.ShowSuccess("Journal could not be edited");
                    }
                    finally
                    {
                        LoadingService.Hide();
                    }
                }
                else
                {
                    try
                    {
                        LoadingService.Show();
                        await journal.AddNewJournal(dto);
                        await ToastService.ShowSuccess("Journal added successfully");
                    }
                    catch
                    {
                        await ToastService.ShowSuccess("Journal could not be added");
                    }
                    finally
                    {
                        LoadingService.Hide();
                    }
                    
                }
                nav.NavigateTo("/history"); 
            }
            catch (Exception e)
            {
                await ToastService.ShowError("Journal could not updated");
                Console.WriteLine(e);
            }
        }

        public async Task deleteJournal()
        {
            if (editJournal && existingJournal != null)
            {
                try
                {
                    LoadingService.Show();
                    await journal.DeleteJournal(existingJournal.JournalId);
                    await ToastService.ShowSuccess("Journal deleted successfully");
                    nav.NavigateTo("/history");
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    LoadingService.Hide();
                }
            }
        }
    }
}
