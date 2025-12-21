using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Models
{
    internal class JournalMoods
    {
        [Key]

        public Guid JournalID { get; }
        [Key]
        public Guid MoodID { get; }
    }
}

