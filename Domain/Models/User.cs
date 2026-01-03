using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Domain.Models
{
    [Table("users")]
    internal class User
    {

        [PrimaryKey]
        public Guid UserID { get; }=Guid.NewGuid();
        public string Username;
        public string PasswordHash;
        public int pin;
        
        //List<Journal> userJournals {  get; set; }
    }

}
