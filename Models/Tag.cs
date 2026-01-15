using SQLite;
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
        [PrimaryKey]
        public string TagID { get; } = Guid.NewGuid().ToString();   
        public string TagValue { get; set; }
        public bool IsPreDefined { get; set; } = true;
    }
}
