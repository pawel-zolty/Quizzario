using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizDTOMapper quizDTOMapper;
        private readonly IApplicationUserDTOMapper userFactory;
        private readonly IQuizEntityMapper quizEntityMapper;

        public QuizService(IQuizDTOMapper quizDTOMapper,
            IApplicationUserDTOMapper userFactory,
            IQuizEntityMapper quizEntityFactory)
        {
            this.quizDTOMapper = quizDTOMapper;
            this.quizEntityMapper = quizEntityFactory;
            this.userFactory = userFactory;
        }

        public List<QuizDTO> Quizes => GetAllQuizes();

        public List<QuizDTO> GetAllUserQuizes(string userId)
        {
            List<QuizDTO> quizes = quizDTOMapper.CreateAllUserQuizes(userId);
            return quizes;
        }

        public List<QuizDTO> GetUserFavouriteQuizes(string userId)
        {
            List<QuizDTO> quizes = quizDTOMapper.CreateUserFavouriteQuizes(userId);
            return quizes;
        }

        public void AddQuizToFavourite(string userId, string quizId)
        {
            var user = userFactory.CreateUserWithId(userId);
            var quiz = quizDTOMapper.Create(quizId);
            quiz.AddToFavouritesUsers(user);
        }

        public void RemoveQuizFromFavourite(string userId, string quizId)
        {
            var user = userFactory.CreateUserWithId(userId);
            var quiz = quizDTOMapper.Create(quizId);
            quiz.RemoveFromFavouritesUsers(user);
        }

        public bool IsQuizFavourite(string userId, string quizId)
        {
            var user = userFactory.CreateUserWithId(userId);
            var quiz = quizDTOMapper.Create(quizId);
            var i = quiz.IsFavouritesUsers(user);
            return i;
        }

        public void SaveQuiz(QuizDTO quiz)
        {
            quizDTOMapper.SaveQuiz(quiz);
        }

        public List<QuizDTO> SearchByName(string name)
        {
            List<QuizDTO> quizes = quizDTOMapper.SearchByName(name);
            return quizes;
        }

        private List<QuizDTO> GetAllQuizes()
        {
            List<QuizDTO> quizes = quizDTOMapper.GetAllQuizes();
            return quizes;
        }
    }
}
