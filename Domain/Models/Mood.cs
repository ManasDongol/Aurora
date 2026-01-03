using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Domain.Models
{
    internal class Mood
    {
        [Key]
        public Guid MoodID { get; }
        public string MoodValue { get; set; }
    }
}
