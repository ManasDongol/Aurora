using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Repositories;
using AuroraJournalingApp.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuroraJournalingApp.Components.Pages
{
    public partial class JournalHistory(JournalRepository repo)
    {
        private string SearchText { get; set; } = "";
        private List<Models.Journal> Results { get; set; } = new();
        private List<string> Moods { get; set; } = new();
        private List<string> Tags   { get; set; } = new();
        private CancellationTokenSource _cts;

        private async Task OnSearchChanged(ChangeEventArgs e)
        {
            // Cancel any previous pending search
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            // Waiting 300ms to avoid querying on every keystroke
            try
            {
                await Task.Delay(300, _cts.Token);
                await RunSearch();
            }
            catch (TaskCanceledException)
            {
                
            }
        }

        private async Task RunSearch()
        {

            var SelectedMood = converter(moodstring);
            var SelectedTag = converter(tagstring);
            var options = new SearchOptions
            {
                Content = SearchText,
                Mood = SelectedMood,
                Tags = SelectedTag,

            };

            Results = await repo.SearchAsync(options);
            StateHasChanged();
        }

        public List<string> converter(string value)
        {
            var finallist = value
    .Split(',', StringSplitOptions.RemoveEmptyEntries)
    .Select(t => t.Trim())
    .ToList();
            return finallist;
        }

    }
}
