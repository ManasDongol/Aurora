using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.DTOs
{
    public class SearchOptions
    {
        public string? Content { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public List<string>? Mood { get; set; }
      
        public List<string>? Tags { get; set; }
    }
}
