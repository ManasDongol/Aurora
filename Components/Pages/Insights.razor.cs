using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Models;
using AuroraJournalingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Components.Pages
{
    public partial class Insights(PdfService service)
    {

        private async Task pdfgenerator()
        {
            var journals = await JournalService.GetAllJournals();

            var report = BuildInsightsReport(journals);

            service.GenerateAnalyticsPdf(report);
        }


        private GenerateAnalyticsPdfDTO BuildInsightsReport(List<Models.Journal> journals)
        {
            var report = new GenerateAnalyticsPdfDTO
            {
                CurrentStreak = CurrentStreak,
                LongestStreak = LongestStreak,
                TotalEntries = TotalEntries,
                MostFrequentMood = MostFrequentMood
            };

            // moods
            var moodCounts = new Dictionary<string, int>();
            foreach (var j in journals)
            {
                if (!string.IsNullOrWhiteSpace(j.primaryMood))
                    AddCount(moodCounts, j.primaryMood);

                if (!string.IsNullOrWhiteSpace(j.secondaryMoods))
                {
                    foreach (var s in j.secondaryMoods.Split(',', StringSplitOptions.RemoveEmptyEntries))
                        AddCount(moodCounts, s.Trim());
                }
            }
            report.MoodCounts = moodCounts;

            // tags
            var tagCounts = new Dictionary<string, int>();
            foreach (var j in journals)
            {
                if (!string.IsNullOrWhiteSpace(j.tags))
                {
                    foreach (var t in j.tags.Split(',', StringSplitOptions.RemoveEmptyEntries))
                        AddCount(tagCounts, t.Trim());
                }
            }
            report.TagCounts = tagCounts;

            return report;
        }

    }
}
