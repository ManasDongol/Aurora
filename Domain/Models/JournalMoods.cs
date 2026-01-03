using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Domain.Models
{
    [Table("JournalMoods")]
    internal class JournalMoods
    {
        [PrimaryKey]

        public Guid JournalID { get; }
        [PrimaryKey]

        public Guid MoodID { get; }
    }
}

