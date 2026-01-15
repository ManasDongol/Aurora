using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Models
{
    internal class JournalTag

    {
        [PrimaryKey]

        public string JournalID { get; set; } 
        [PrimaryKey]
        public string TagId { get; set; }
    }
}
