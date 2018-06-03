using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.BusinessLogic.Extensions;
using Quizzario.Data.Abstracts;
using Quizzario.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzario.BusinessLogic.Factories
{
    public class QuizEntityMapper : IQuizEntityMapper
    {
        private IQuizRepository quizRepository;

        public QuizEntityMapper(IQuizRepository quizRepository)
        {
            this.quizRepository = quizRepository;
        }

        public Quiz CreateQuizEntity(QuizDTO quizDTO)
        {
            var quiz = quizRepository.GetById(quizDTO.Id);
            quiz.Id = quizDTO.Id;
            quiz.ApplicationUserId = quizDTO.ApplicationUserId;
            quiz.FilePath = quizDTO.FilePath;
            quiz.Description = quizDTO.Description;
            quiz.QuizType = QuizTypeExtension.ToEntityQuizType(quizDTO.QuizType);
            quiz.Title = quizDTO.Title;

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

        public void SaveQuiz(QuizDTO quizDTO)
        {
            var quiz = CreateQuizEntity(quizDTO);
            quizRepository.Update(quiz);
        }
    }
}
