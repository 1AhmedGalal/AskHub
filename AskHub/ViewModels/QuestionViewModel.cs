using System.ComponentModel.DataAnnotations;

namespace AskHub.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Question: ")]
        public string Content { get; set; } = null!;

        [Display(Name = "Sent By: ")]
        public string? SourceUsername { get; set; } = null!;

        [Display(Name = "Sent To: ")]
        public string? DestinationUsername { get; set; } = null!;

        [Display(Name = "Date: ")]
        public DateTime DateTime { get; set; }

        public bool Seen {  get; set; }
    }
}
