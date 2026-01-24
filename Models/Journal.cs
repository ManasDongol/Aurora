using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Models
{

    public class Journal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }   // FTS rowid reference

        [NotNull]
        public string JournalId { get; set; } = Guid.NewGuid().ToString();

        [NotNull]
        public string Title { get; set; }
        [NotNull]
        public string Content { get; set; }

        [NotNull]
        public string primaryMood { get; set; }
        public string secondaryMoods { get; set; }
        [NotNull]
        public string tags { get; set; }
        public bool IsUpdated { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
    }

}
