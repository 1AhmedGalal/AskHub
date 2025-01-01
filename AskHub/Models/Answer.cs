namespace AskHub.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public int QuestionId { get; set; }

        public Question Question { get; set; } = null!;

        DateTime CreatedDate { get; set; }
    }
}
