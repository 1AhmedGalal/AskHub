using System.ComponentModel.DataAnnotations;

namespace AskHub.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        [Required]
        public DateTime CreatedDate { get; set; }

        public bool Seen { get; set; } = false;

        public string? SourceAppUserId { get; set; }

        public AppUser? SourceAppUser { get; set; }

        public string? DestinationAppUserId { get; set; } = null!;

        public AppUser? DestinationAppUser { get; set; } = null!;

        public int? AnswerId { get; set; }
        
        public Answer? Answer { get; set; }
    }
}
