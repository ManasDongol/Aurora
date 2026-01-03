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
        [Key]

        public Guid JournalID { get; }
        [Key]
        public Guid TagId { get; }
    }
}
