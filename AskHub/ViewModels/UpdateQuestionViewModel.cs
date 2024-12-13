using System.ComponentModel.DataAnnotations;

namespace AskHub.ViewModels
{
    public class UpdateQuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        [Display(Name = "Question: ")]
        public string Content { get; set; } = null!;
    }
}
