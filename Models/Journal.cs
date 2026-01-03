using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Models
{
   
    internal class Journal
    {

    
        public Guid Id { get; } = Guid.NewGuid();

        public Guid UserID { get; }

        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsUpdated { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; }

    }
}
