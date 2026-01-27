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
    public partial class JournalHistory(JournalRepository repo, PdfService service)
    {
        public string moodstring { get; set; } = "";
        public string tagstring { get; set; } = "";
        List<AuroraJournalingApp.Models.Journal> journallist = new List<AuroraJournalingApp.Models.Journal>();

        // Pagination size
        private int currentPage = 1;
        private int totalPages = 1;
        private const int PageSize = 5;


        //searchinh
        private string SearchText { get; set; } = "";
        private List<Models.Journal> Results { get; set; } = new();
        private List<string> Moods { get; set; } = new();
        private List<string> Tags   { get; set; } = new();
        private CancellationTokenSource _cts;


        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task OnSearchChanged(ChangeEventArgs e)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
           
            try
            {
                await Task.Delay(300, _cts.Token);
                await RunSearch();
            }
            catch (TaskCanceledException)
            {
                
            }
        }


        private async Task LoadData()
        {

            var count = await JournalService.GetTotalCountAsync();
            totalPages = (int)Math.Ceiling(count / (double)PageSize);
            if (totalPages < 1) totalPages = 1;


            var dto = new PaginationDTO { pageIndex = currentPage - 1 };
            journallist = await JournalService.GetPages(dto);
        }

        private async Task OnPageChanged(int page)
        {
            currentPage = page;
            await LoadData();
        }

        private async Task OnDateSelected(DateTime date)
        {
            await LoadJournals(date);
        }

        private async Task LoadJournals(DateTime date)
        {

            currentPage = 1;
            totalPages = 1;

            var start = date.Date;
            var end = date.Date.AddDays(1).AddTicks(-1);
            journallist = await JournalService.GetJournalsByDateRange(start, end);
        }

        private async Task PerformSearch()
        {
            var opts = new SearchOptions
            {
                Content = SearchText,
                Mood = !string.IsNullOrWhiteSpace(moodstring) ? new List<string> { moodstring } : null,
                Tags = !string.IsNullOrWhiteSpace(tagstring) ? new List<string> { tagstring } : null
            };

            if (string.IsNullOrWhiteSpace(SearchText) && string.IsNullOrWhiteSpace(moodstring) && string.IsNullOrWhiteSpace(tagstring))
            {
                // If search is cleared, reload the full history (paginated)
                currentPage = 1;
                await LoadData();
                return;
            }

            // For search results, we show all matches (disable pagination)
            currentPage = 1;
            totalPages = 1;
            journallist = await JournalService.SearchAsync(opts);
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


        public void JournalPdf(Models.Journal journal)
        {
            var moods = journal.primaryMood +","+ journal.secondaryMoods;

            var day = journal.Created.Date;
            var plainText = HTMLService.ToPlainText(journal.Content);
            GenerateJournalPdfDTO dto = new GenerateJournalPdfDTO
            {
                Title = journal.Title,
                Content = plainText,
                Created = day,
                TagCounts = journal.tags,
                MoodCounts = moods

            };
            service.GenerateJournalPdf(dto);

        }
    }
}
