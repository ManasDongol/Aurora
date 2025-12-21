using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Models
{
    internal class Journal
    {
        [Key]
        public Guid Id { get; }
        public Guid UserID { get; }

        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsUpdated { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; }

        public Journal()
        {

        }
    }
}
