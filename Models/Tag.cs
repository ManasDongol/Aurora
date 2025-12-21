using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Models
{
    internal class Tag
    {
        [Key]
        public Guid TagID { get; }
        public string TagValue { get; set; }
        public bool IsPreDefined { get; set; }
    }
}
