using AskHub.Models;

namespace AskHub.Repositories
{
    public interface IAnswerRepository
    {
        void Add(Answer answer);

        void DeleteByQuestionId(int id);

        Answer? GetByQuestionId(int id);

        void DeleteByAnswerId(int id);

        Answer? GetByAnswerId(int id);

    }
}
