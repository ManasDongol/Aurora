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
    public class User
    {

        [PrimaryKey]
        public string UserID { get; set; } =Guid.NewGuid().ToString();
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        
        public string email { get; set; }
        

        //List<Journal> userJournals {  get; set; }
    }

}
