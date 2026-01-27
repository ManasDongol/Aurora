using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraJournalingApp.DTOs
{
    public record ToastMessageDTO(string Title, string Content, string CssClass);
}
