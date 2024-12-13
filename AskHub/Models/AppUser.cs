using AskHub.Models;
using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    public ICollection<Question> SourceQuestions { get; set; } = new List<Question>(); // Questions sent by this user
    public ICollection<Question> DestinationQuestions { get; set; } = new List<Question>(); // Questions received by this user
}
