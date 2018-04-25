using Quizzario.Data.Extensions;
using Quizzario.Data.Abstracts;
using Quizzario.Data.DTOs;
using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public IEnumerable<QuizDTO> CreateAllUserQuizes(string userId)
        {
            IEnumerable<Quiz> quizes = repository.All.
                Where(q => q.ApplicationUserId.Equals(userId));
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            quizes = quizes.ToList();
            foreach (var q in quizes)
            {
                quizesDTO.Add(CreateQuiz(q));
            }
            var x = new QuizDTO
            {
                Title = "Cos1",
                Id = "1",
                FilePath = "xd"
            };
            var y = new QuizDTO
            {
                Title = "Cos2",
                Id = "1",
                FilePath = "xd"
            };
            var z = new QuizDTO
            {
                Title = "Cos3",
                Id = "1",
                FilePath = "xd"
            };
            var o = new QuizDTO
            {
                Title = "Cos4",
                Id = "1",
                FilePath = "xd"
            };
            var a = new QuizDTO
            {
                Title = "Cos5",
                Id = "1",
                FilePath = "xd"
            };
            var b = new QuizDTO
            {
                Title = "Cos6",
                Id = "1",
                FilePath = "xd"
            };
            quizesDTO.Add(x);
            quizesDTO.Add(y);
            quizesDTO.Add(z);
            quizesDTO.Add(o);
            quizesDTO.Add(a);
            quizesDTO.Add(b);
            return quizesDTO;
        }

        private QuizDTO CreateQuiz(Quiz quiz)
        {
            if (quiz == null)
                return null;
            string id = quiz.Id;
            string title = quiz.Title;
            string description = quiz.Description;
            string userId = quiz.ApplicationUserId;
            string filePath = quiz.FilePath;
            DateTime creationDate = quiz.CreationDate;

            ApplicationUserDTO user = userFactory.Create(userId);
            //if (user == null || title == null || filePath == null)
              //  return null;
            DTOs.QuizType? type = quiz.QuizType.ToDTOQuizType();
            QuizDTO quizDTO = new QuizDTO
            {
                Id = id,
                Title = title,
                Description = description,
                ApplicationUserId = "1",
                QuizSettingsId = "1",
                QuizType = type,
                FilePath = filePath,
                ApplicationUser = user,
                CreationDate = creationDate
                //AssignedUsers,
                //Scores,
                //QuizSettings = 
                //TO DO 
            };
            return quizDTO;
        }
    }
}
