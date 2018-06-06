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

        private Quiz CreateQuiz(QuizDTO quizDTO)
        {
            Quiz quiz = GetQuizEntity(quizDTO);
            quiz.AssignedUsers = new List<AssignedUser>();
            var type = Data.Entities.AssignType.Favourite;
            CreateQuizEntityAssignList(quizDTO, quiz, type, quizDTO.FavouritesUsers);
            type = Data.Entities.AssignType.AssignedToPrivate;
            CreateQuizEntityAssignList(quizDTO, quiz, type, quizDTO.PrivateAssignedUsers);
            return quiz;
        }

        private static void CreateQuizEntityAssignList(QuizDTO quizDTO, Quiz quiz, Data.Entities.AssignType type, IEnumerable<ApplicationUserDTO> listOfusersDTO)
        {
            foreach (var user in listOfusersDTO)
            {
                var ass = new AssignedUser()
                {
                    ApplicationUserId = user.Id,
                    AssignType = type,
                    QuizId = quizDTO.Id
                };
                quiz.AssignedUsers.Add(ass);
            }
        }

        public void Update(QuizDTO quizDTO)
        {
            var quiz = CreateQuiz(quizDTO);
            quizRepository.Update(quiz);
        }

        public void AddNewQuiz(QuizDTO quizDTO)
        {
            var quiz = CreateQuiz(quizDTO);
            quizRepository.Add(quiz);
        }

        private Quiz GetQuizEntity(QuizDTO quizDTO)
        {
            var quiz = quizRepository.GetById(quizDTO.Id);
            if (quiz == null)
                quiz = new Quiz();
            quiz.Id = quizDTO.Id;
            quiz.ApplicationUserId = quizDTO.ApplicationUserId;
            quiz.FilePath = quizDTO.FilePath;
            quiz.Description = quizDTO.Description;
            quiz.QuizType = QuizTypeExtension.ToEntityQuizType(quizDTO.QuizType);
            quiz.QuizAccessLevel = QuizAccessLevelExtension.ToEntityQuizAccessLevel(quizDTO.QuizAccessLevel);
            quiz.Title = quizDTO.Title;
            return quiz;
        }        
    }
}
