using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Models
{
    [Table("users")]
    internal class User
    {

        [PrimaryKey]
        public Guid UserID { get; set; } =Guid.NewGuid();
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int pin { get; set; }

        //List<Journal> userJournals {  get; set; }
    }

}
