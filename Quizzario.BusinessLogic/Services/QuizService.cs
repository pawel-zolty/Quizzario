using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.Abstracts;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.BusinessLogic.Factories;
using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Quizzario.BusinessLogic.Services
{
    public class QuizService : IQuizService
    {
        private IQuizDTOMapper quizDTOMapper;
        private IApplicationUserDTOFactory userFactory;
        private IApplicationUserEntityFactory userEnFactory;
        private IQuizEntityMapper quizEntityMapper;

        public QuizService(IQuizDTOMapper quizDTOMapper,
            IApplicationUserDTOFactory userFactory,
            IApplicationUserEntityFactory userEnFactory,
            IQuizEntityMapper quizEntityFactory)
        {
            this.quizDTOMapper = quizDTOMapper;
            this.quizEntityMapper = quizEntityFactory;
            this.userFactory = userFactory;
            this.userEnFactory = userEnFactory;
        }

        public List<QuizDTO> Quizes => quizDTOMapper.Quizes;

        public List<QuizDTO> GetUserFavouriteQuizes(string userId)
        {
            List<QuizDTO> quizes = quizDTOMapper.CreateUserFavouriteQuizes(userId);
            return quizes;
        }

        public List<QuizDTO> GetAllUserQuizes(string userId)
        {
            List<QuizDTO> quizes = quizDTOMapper.CreateAllUserQuizes(userId);
            return quizes;
        }

        public void AddQuizToFavourite(string userId, string quizId)
        {
            var user = userFactory.CreateUserWithId(userId);
            var quiz = quizDTOMapper.Create(quizId);
            quiz.AddToFavouritesUsers(user);
            quizEntityMapper.SaveQuiz(quiz);
            //quizFactory.AddQuizToFavourite(userId, quizId);
        }

        public void RemoveQuizFromFavourite(string userId, string quizId)
        {
            quizDTOMapper.RemoveQuizFromFavourite(userId, quizId);
        }

        public List<QuizDTO> SearchByName(string name)
        {
            List<QuizDTO> quizes = quizDTOMapper.SearchByName(name);
            return quizes;
        }

        public List<QuizDTO> GetAllQuizes()
        {
            List<QuizDTO> quizes = quizDTOMapper.GetAllQuizes();
            return quizes;
        }

        public void SaveQuiz(QuizDTO quiz)
        {
            quizDTOMapper.SaveQuiz(quiz);
        }
    }
}
