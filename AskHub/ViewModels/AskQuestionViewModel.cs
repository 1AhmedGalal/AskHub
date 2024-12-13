using System.ComponentModel.DataAnnotations;

namespace AskHub.ViewModels
{
    public class AskQuestionViewModel
    {
        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        [Display(Name = "Question: ")]
        public string Content { get; set; } = null!;

        public bool AskAnonymously { get; set; } = false;

    }
}
