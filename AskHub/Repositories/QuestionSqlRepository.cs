﻿using AskHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AskHub.Repositories
{
    public class QuestionSqlRepository : IQuestionRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAnswerRepository _answerRepository;
        public QuestionSqlRepository(AppDbContext appDbContext, IAnswerRepository answerRepository) 
        {
            _appDbContext = appDbContext;
            _answerRepository = answerRepository;
        }

        public void Add(Question question)
        {
            _appDbContext.Questions.Add(question);
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Question? question = GetByQuestionId(id);

            if (question is not null /*&& !question.Seen*/)
            {
                if(question.Seen)
                {
                    _answerRepository.DeleteByQuestionId(question.Id);
                }

                _appDbContext.Questions.Remove(question);
                _appDbContext.SaveChanges();
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
                return _appDbContext.Questions
                    .Include(q => q.SourceAppUser) // Include SourceAppUser
                    .Include(q => q.DestinationAppUser) // Include DestinationAppUser
                    .Where(q => q.SourceAppUserId == userId)
                    .ToList();
            }
            else
            {
                return _appDbContext.Questions
                    .Include(q => q.SourceAppUser) // Include SourceAppUser
                    .Include(q => q.DestinationAppUser) // Include DestinationAppUser
                    .Where(q => q.DestinationAppUserId == userId)
                    .ToList();
            }
        }


        public Question? GetByQuestionId(int id)
        {
            return _appDbContext.Questions.FirstOrDefault(q => q.Id == id);
        }

        public void Update(Question question)
        {
            Question? oldQuestion = GetByQuestionId(question.Id);

            if (oldQuestion is not null && !oldQuestion.Seen)
            {
                oldQuestion.Content = question.Content;
                _appDbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Couldn't Update Question");
            }
        }

        public void MarkAsSeen(Question question)
        {
            if (question is not null && !question.Seen)
            {
                question.Seen = true;
                _appDbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Couldn't Update Question");
            }
        }
    }
}
