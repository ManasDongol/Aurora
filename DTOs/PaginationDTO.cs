using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.DTOs
{
    public class PaginationDTO
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
