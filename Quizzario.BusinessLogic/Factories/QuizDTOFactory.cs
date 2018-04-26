using Quizzario.BusinessLogic.Extensions;
using Quizzario.BusinessLogic.Abstracts;
using Quizzario.Data.Abstracts;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using Quizzario.Data;
using Quizzario.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Quizzario.BusinessLogic.Factories
{
    public class QuizDTOFactory : IQuizDTOFactory
    {
        private IQuizRepository quizRepository;
        IAssignedRepository assignedRepository;
        private IApplicationUserDTOFactory userFactory;
 
       
        public QuizDTOFactory(IQuizRepository quizRepository,
            IAssignedRepository assignedRepository,
            IApplicationUserDTOFactory userFactory)
        {
            this.quizRepository = quizRepository;
            this.assignedRepository = assignedRepository;
            this.userFactory = userFactory;
        }

        public QuizDTO Create(string id)
        {
            Quiz quiz = quizRepository.GetById(id);
            return CreateQuiz(quiz);
        }

        public IEnumerable<QuizDTO> CreateUserFavouriteQuizes(string userId)
        {
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            IEnumerable<string> userFavouriteQuizesIds =
                assignedRepository.GetUserAssigns(userId).
                Where(q => q.AssignType == Data.Entities.AssignType.Favourite).
                Select(q => q.Id);
            
            if (userFavouriteQuizesIds.ToList().Count == 0)
                return quizesDTO;            
            
            IEnumerable <Quiz> quizes = quizRepository.Quizes.
                Where(
                    q => userFavouriteQuizesIds.
                    Contains(q.Id)
                );

            quizes = quizes.ToList();
            foreach (var q in quizes)
            {
                quizesDTO.Add(CreateQuiz(q));
            }
            //AddStaticMockData(quizesDTO);
            return quizesDTO;
        }

        public IEnumerable<QuizDTO> CreateAllUserQuizes(string userId)
        {
            
            IEnumerable<Quiz> quizes = quizRepository.Quizes.
                Where(q => q.ApplicationUserId.Equals(userId));
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            if (quizes != null)
            {
                quizes = quizes.ToList();
                foreach (var q in quizes)
                {
                    quizesDTO.Add(CreateQuiz(q));
                }
            }
            AddStaticMockData(quizesDTO);
            return quizesDTO;
        }

        private static void AddStaticMockData(List<QuizDTO> quizesDTO)
        {
            var x = new QuizDTO
            {
                Id = "3",
                Title = "Quiz1",
                Description = "1 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean mollis justo orci, eget vulputate orci malesuada nec...",
                CreationDate = "1996.01.11",
                ApplicationUserId = "8ded9b6f-e8a8-425b-a115-c280885e92c1"
            };
            var y = new QuizDTO
            {
                Id = "4",
                Title = "Quiz2",
                Description = "2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean mollis justo orci, eget vulputate orci malesuada nec...",
                CreationDate = "2000.01.11"
            };
            var z = new QuizDTO
            {
                Id = "5",
                Title = "Quiz3",
                Description = "3 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean mollis justo orci, eget vulputate orci malesuada nec...",
                CreationDate = "2011.01.11"
            };
            var o = new QuizDTO
            {
                Id = "6",
                Title = "Quiz4",
                Description = "4 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean mollis justo orci, eget vulputate orci malesuada nec...",
                CreationDate = "2016.01.11"
            };
            var a = new QuizDTO
            {
                Id = "7",
                Title = "Quiz5",
                Description = "5 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean mollis justo orci, eget vulputate orci malesuada nec...",
                CreationDate = "1990.01.11"
            };
            var b = new QuizDTO
            {
                Id = "8",
                Title = "Quiz6",
                Description = "6 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean mollis justo orci, eget vulputate orci malesuada nec...",
                CreationDate = "2006.01.11"
            };
            quizesDTO.Add(x);
            quizesDTO.Add(y);
            quizesDTO.Add(z);
            quizesDTO.Add(o);
            quizesDTO.Add(a);
            quizesDTO.Add(b);
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
                CreationDate = creationDate.ToString()
                //AssignedUsers,
                //Scores,
                //QuizSettings = 
                //TO DO 
            };
            return quizDTO;
        }

        public IEnumerable<QuizDTO> SearchByName(string name)
        {
            IEnumerable<Quiz> quizes = quizRepository.Quizes;
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            quizes = quizes.ToList();
            foreach (var q in quizes)
            {
                quizesDTO.Add(CreateQuizByName(q, name));

            }
            for (int i = 0; i < quizesDTO.Count; i++)
            {
                if (quizesDTO[i] == null)
                {
                    quizesDTO.RemoveAt(i);
                }
            }
            return quizesDTO;
        }

        private QuizDTO CreateQuizByName(Quiz q, string name)
        {
            if (q == null)
                return null;
            if (name != q.Title)
                return null;
            string id = q.Id;
            string title = q.Title;
            string userId = q.ApplicationUserId;
            string filePath = q.FilePath;

            ApplicationUserDTO user = userFactory.Create(userId);
            //if (user == null || title == null || filePath == null)
            //  return null;
            DTOs.QuizType? type = q.QuizType.ToDTOQuizType();
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

        public IEnumerable<QuizDTO> GetAllQuizes()
        {
            IEnumerable<Quiz> quizes = quizRepository.Quizes;
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            quizes = quizes.ToList();
            foreach (var q in quizes)
            {
                quizesDTO.Add(CreateQuiz(q));
            }
            AddStaticMockData(quizesDTO);
            return quizesDTO;
        }

        public void SaveQuiz(QuizDTO quizDTO)
        {


            Quiz quiz = new Quiz();
            quiz.Id = quizDTO.Id;
            quiz.QuizSettingsId = quizDTO.QuizSettingsId;
            quiz.Title = quizDTO.Title;
            quiz.Description = quizDTO.Description;
            quiz.ApplicationUserId = quizDTO.ApplicationUserId;
            quizRepository.SaveQuiz(quiz);

           
           
        }

        public IEnumerable<QuizDTO> Quizes => GetAllQuizes();
    }
}
