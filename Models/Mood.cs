using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Models
{
    internal class Mood
    {
        [PrimaryKey]
        public string MoodID { get; } = Guid.NewGuid().ToString();
        public string MoodValue { get; set; }
    }
}
