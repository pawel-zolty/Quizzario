using Quizzario.Data.Extensions;
using Quizzario.Data.Abstracts;
using Quizzario.Data.DTOs;
using Quizzario.Data.Entities;

namespace Quizzario.Data.Factories
{
    public class QuizDTOFactory : IQuizDTOFactory
    {
        private IRepository<Quiz> repository;
        private IApplicationUserDTOFactory userFactory;

        public QuizDTOFactory(IRepository<Quiz> repository, IApplicationUserDTOFactory userFactory)
        {
            this.repository = repository;
            this.userFactory = userFactory;
        }

        public QuizDTO Create(string id)
        {
            Quiz quiz = repository.GetById(id);
            return CreateQuiz(quiz);
        }

        private QuizDTO CreateQuiz(Quiz quiz)
        {
            string id = quiz.Id;
            string title = quiz.Title;
            string userId = quiz.ApplicationUserId;
            string filePath = quiz.FilePath;
            
            ApplicationUserDTO user = userFactory.Create(userId);
            if (user == null || title == null || filePath == null)
                return null;
            DTOs.QuizType? type = quiz.QuizType.ToDTOQuizType();
            QuizDTO quizDTO = new QuizDTO
            {
                Id = id,
                Title = title,
                ApplicationUserId = "1",
                QuizSettingsId = "1",
                QuizType = type,
                FilePath = filePath,
                ApplicationUser = user,
                //AssignedUsers,
                //Scores,
                //QuizSettings = 
                //TO DO 
            };
            return quizDTO;
        }
    }
}
