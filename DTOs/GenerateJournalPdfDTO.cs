using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.DTOs
{
    public class GenerateJournalPdfDTO
    {
        public string Title { get; set; }


        public string Content { get; set; }

        public DateTime Created { get; set; }
        public string Tags { get; set; }
        //public Dictionary<string, int> MoodCounts { get; set; } = new();
       // public Dictionary<string, int> TagCounts { get; set; } = new();

        public string MoodCounts { get; set; }
        public string TagCounts { get; set; }
        public DateTime GeneratedOn { get; set; } = DateTime.Now;
    }
}
