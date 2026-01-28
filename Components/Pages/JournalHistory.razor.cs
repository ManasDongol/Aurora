using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Repositories;
using AuroraJournalingApp.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private const int PageSize = 2;

     


        //searchinh
        private string SearchText { get; set; } = "";
        private List<Models.Journal> Results { get; set; } = new();
        private List<string> Moods { get; set; } = new();
        private List<string> Tags   { get; set; } = new();
        private CancellationTokenSource _cts;

        private bool IsSearching =>
    !string.IsNullOrWhiteSpace(SearchText) ||
    !string.IsNullOrWhiteSpace(moodstring) ||
    !string.IsNullOrWhiteSpace(tagstring);

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

            var dto = new PaginationDTO
            {
                pageIndex = currentPage - 1,
                pageSize = PageSize
            };

            journallist = await JournalService.GetPages(dto);

        }

        private async Task OnPageChanged(int page)
        {
            currentPage = page;

            bool hasFilters = !string.IsNullOrWhiteSpace(SearchText) ||
                              !string.IsNullOrWhiteSpace(moodstring) ||
                              !string.IsNullOrWhiteSpace(tagstring);

            if (hasFilters)
                await PerformSearch();
            else
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
            // Only compute search mode
            bool hasFilters = !string.IsNullOrWhiteSpace(SearchText) ||
                              !string.IsNullOrWhiteSpace(moodstring) ||
                              !string.IsNullOrWhiteSpace(tagstring);

            currentPage = 1;

            if (!hasFilters)
            {
                await LoadData();
                return;
            }

            var opts = new SearchOptions
            {
                Content = SearchText,
                Mood = converter(moodstring),
                Tags = converter(tagstring),
                PageIndex = currentPage - 1,
                PageSize = PageSize
            };

            // Get total count first
            var total = await repo.GetSearchCountAsync(opts);
            totalPages = (int)Math.Ceiling(total / (double)PageSize);

            // Load first page
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
