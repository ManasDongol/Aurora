using AuroraJournalingApp.DTOs;
using AuroraJournalingApp.Models;
using Microsoft.Maui.Storage;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AuroraJournalingApp.Services;

public class PdfService
{
    public void GenerateAnalyticsPdf(GenerateAnalyticsPdfDTO report)
    {
        string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            $"Aurora-Insights-{DateTime.Now:yyyyMMddHHmm}.pdf"
        );

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);

                page.Header()
                    .Text("Aurora Journaling — Insights Report")
                    .SemiBold()
                    .FontSize(26);

                page.Content().Column(col =>
                {
                    col.Spacing(15);

                    col.Item().Text($"Generated: {report.GeneratedOn:g}");

                    col.Item().LineHorizontal(1);

                    col.Item().Row(r =>
                    {
                        r.RelativeItem().Text($"Current Streak: {report.CurrentStreak} days");
                        r.RelativeItem().Text($"Longest Streak: {report.LongestStreak} days");
                    });

                    col.Item().Row(r =>
                    {
                        r.RelativeItem().Text($"Total Entries: {report.TotalEntries}");
                        r.RelativeItem().Text($"Top Mood: {report.MostFrequentMood}");
                    });

                    col.Item().LineHorizontal(1);

                    col.Item().Text("Mood Breakdown").SemiBold();

                    foreach (var mood in report.MoodCounts.OrderByDescending(x => x.Value))
                    {
                        col.Item().Text($"{mood.Key}: {mood.Value}");
                    }

                    col.Item().LineHorizontal(1);

                    col.Item().Text("Tag Usage").SemiBold();

                    foreach (var tag in report.TagCounts.OrderByDescending(x => x.Value))
                    {
                        col.Item().Text($"{tag.Key}: {tag.Value}");
                    }
                });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        })
        .GeneratePdf(path);

        Launcher.OpenAsync(new OpenFileRequest
        {
            File = new ReadOnlyFile(path)
        });
    }


    public void GenerateJournalPdf(GenerateJournalPdfDTO journal)
    {
        string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            $"Journal-{journal.Title}-{DateTime.Now:yyyyMMddHHmm}.pdf"
        );
        string journalName = journal.Title;

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);

                page.Header()
                    .Text($"Journal-{journal.Title}")
                    .SemiBold()
                    .FontSize(26);

                page.Content().Column(col =>
                {
                    col.Spacing(15);

                    col.Item().Text($"Generated: {journal.Created:g}");

                  

                
                    col.Item().LineHorizontal(1);

                    col.Item().Text($"Moods :{journal.MoodCounts}").SemiBold();
                    col.Item().Text($"Tags :{journal.TagCounts}").SemiBold();

                    col.Item().LineHorizontal(1);

                    col.Item().Text($"{journal.Content}")
                   .FontSize(12);
                });


            
                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        })
        .GeneratePdf(path);

        Launcher.OpenAsync(new OpenFileRequest
        {
            File = new ReadOnlyFile(path)
        });
    }
}
