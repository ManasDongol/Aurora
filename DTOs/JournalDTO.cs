using System;
using System.ComponentModel.DataAnnotations;

namespace AuroraJournalingApp.DTOs
{
    public class JournalDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        public string PrimaryMood { get; set; }
        public string SecondaryMoods { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Tags { get; set; }
    }
}
