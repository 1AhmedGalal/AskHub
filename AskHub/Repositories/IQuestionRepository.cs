using AskHub.Models;

namespace AskHub.Repositories
{

    public interface IQuestionRepository
    {
        void Add(Question question);

        void Update(Question question);

        void Delete(int id);

        Question? GetByQuestionId(int id);

        List<Question>? GetAllByUserId(string userId, QuestionDirection direction);

        public void MarkAsSeen(Question question);

    }
}
