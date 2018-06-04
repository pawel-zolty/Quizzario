using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.BusinessLogic.Extensions;
using Quizzario.Data.Abstracts;
using Quizzario.Data.Entities;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Mappers
{
    public class QuizEntityMapper : IQuizEntityMapper
    {
        private IQuizRepository quizRepository;

        public QuizEntityMapper(IQuizRepository quizRepository)
        {
            this.quizRepository = quizRepository;
        }

        public Quiz CreateQuiz(QuizDTO quizDTO)
        {
            Quiz quiz = GetQuizEntity(quizDTO);
            quiz.AssignedUsers = new List<AssignedUser>();
            foreach (var user in quizDTO.FavouritesUsers)
            {
                var ass = new AssignedUser()
                {
                    ApplicationUserId = user.Id,
                    AssignType = 0,
                    QuizId = quizDTO.Id
                };
                quiz.AssignedUsers.Add(ass);
            }
            return quiz;
        }

        public void Update(QuizDTO quizDTO)
        {
            var quiz = CreateQuiz(quizDTO);
            quizRepository.Update(quiz);
        }

        private Quiz GetQuizEntity(QuizDTO quizDTO)
        {
            var quiz = quizRepository.GetById(quizDTO.Id);
            quiz.Id = quizDTO.Id;
            quiz.ApplicationUserId = quizDTO.ApplicationUserId;
            quiz.FilePath = quizDTO.FilePath;
            quiz.Description = quizDTO.Description;
            quiz.QuizType = QuizTypeExtension.ToEntityQuizType(quizDTO.QuizType);
            quiz.Title = quizDTO.Title;
            return quiz;
        }
    }
}
