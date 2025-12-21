using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuroraJournalingApp.Models;

namespace AuroraJournalingApp.Data
{
    internal class AuroraDbContext :DbContext
    {
        public AuroraDbContext(DbContextOptions<AuroraDbContext> options) : base(options)
        {

        }
        DbSet<User> users { get; set; }
    }
}
