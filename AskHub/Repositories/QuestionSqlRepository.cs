using AskHub.Models;
using Microsoft.AspNetCore.Identity;

namespace AskHub.Repositories
{
    public class QuestionSqlRepository : IQuestionRepository
    {
        private readonly AppDbContext _appDbContext;

        public QuestionSqlRepository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public void Add(Question question)
        {
            _appDbContext.Questions.Add(question);
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Question? question = GetByQuestionId(id);

            if (question is not null)
            {
                _appDbContext.Questions.Remove(question);
            }
            else
            {
                throw new Exception("Couldn't Delete Question");
            }
        }

        public List<Question>? GetAllByUserId(string userId, QuestionDirection direction)
        {
            if (direction == QuestionDirection.SOURCE)
            {
                return _appDbContext.Questions.Where(q => q.SourceAppUser != null
                                                      && q.SourceAppUserId == userId).ToList();
            }
            else
            {
                return _appDbContext.Questions.Where(q => q.DestinationAppUser != null
                                                      && q.DestinationAppUserId == userId).ToList();
            }
        }

        public Question? GetByQuestionId(int id)
        {
            return _appDbContext.Questions.FirstOrDefault(q => q.Id == id);
        }

        public void Update(Question newQuestion)
        {
            Question? oldQuestion = GetByQuestionId(newQuestion.Id);

            if (oldQuestion is not null && !oldQuestion.Seen)
            {
                oldQuestion.Content = newQuestion.Content;
                _appDbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Couldn't Update Question");
            }
        }
    }
}
