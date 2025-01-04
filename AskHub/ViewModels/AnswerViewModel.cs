using System.ComponentModel.DataAnnotations;

namespace AskHub.ViewModels
{
    public class AnswerViewModel
    {
        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        [Display(Name = "Answer: ")]
        public string Content { get; set; } = null!;
    }
}
