using AskHub.Models;

namespace AskHub.Repositories
{
    public class AnswerSqlRepository : IAnswerRepository
    {
        private readonly AppDbContext _appDbContext;

        public AnswerSqlRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(Answer answer)
        {
            _appDbContext.Answers.Add(answer);
            _appDbContext.SaveChanges();
        }

        public void DeleteByAnswerId(int id)
        {
            Answer? answer = GetByAnswerId(id);
            delete(answer);
        }

        public void DeleteByQuestionId(int id)
        {
            Answer? answer = GetByQuestionId(id);
            delete(answer);
        }

        public Answer? GetByAnswerId(int id)
        {
            return _appDbContext.Answers.FirstOrDefault(a => a.Id == id);
        }

        public Answer? GetByQuestionId(int id)
        {
            return _appDbContext.Answers.FirstOrDefault(a => a.QuestionId == id);
        }

        private void delete(Answer? answer)
        {
            if (answer is not null)
            {
                _appDbContext.Answers.Remove(answer);
                _appDbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Couldn't Delete Answer");
            }
        }
    }
}
