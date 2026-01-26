using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.DTOs
{
    public class GenerateAnalyticsPdfDTO
    {

        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public int TotalEntries { get; set; }
        public string MostFrequentMood { get; set; } = "-";

        public Dictionary<string, int> MoodCounts { get; set; } = new();
        public Dictionary<string, int> TagCounts { get; set; } = new();

        public DateTime GeneratedOn { get; set; } = DateTime.Now;
    }
}
