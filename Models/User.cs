using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.Models
{
    internal class User
    {

        [Key]
        public Guid UserID;
        public string Username;
        public string PasswordHash;
        public int pin;
    }
}
