﻿using Quizzario.BusinessLogic.Extensions;
using Quizzario.BusinessLogic.Abstracts;
using Quizzario.Data.Abstracts;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System;


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
                Select(q => q.ApplicationUserId);
            
            if (userFavouriteQuizesIds.ToList().Count == 0)
                return quizesDTO;
            foreach(var v in quizRepository.Quizes)
            {
                var a = v.ApplicationUserId;
            }

            IEnumerable<Quiz> quizes = quizRepository.Quizes.
                Where(q => 
                    userFavouriteQuizesIds.
                    Any(qid => qid.Equals(q.ApplicationUserId))
                );            
            quizes = quizes.ToList();
            foreach (var q in quizes)
            {
                quizesDTO.Add(CreateQuiz(q));
            }
            AddStaticMockData(quizesDTO);
            return quizesDTO;
        }

        public IEnumerable<QuizDTO> CreateAllUserQuizes(string userId)
        {
            IEnumerable<Quiz> quizes = quizRepository.Quizes.
                Where(q => q.ApplicationUserId.Equals(userId));
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            quizes = quizes.ToList();
            foreach (var q in quizes)
            {
                quizesDTO.Add(CreateQuiz(q));
            }
            AddStaticMockData(quizesDTO);
            return quizesDTO;
        }

        private static void AddStaticMockData(List<QuizDTO> quizesDTO)
        {
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
