using Quizzario.BusinessLogic.Extensions;
using Quizzario.Data.Abstracts;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using Quizzario.BusinessLogic.Abstract;

namespace Quizzario.BusinessLogic.Mappers
{
    public class QuizDTOMapper : IQuizDTOMapper
    {
        private IQuizRepository quizRepository;
        //private IAssignedRepository assignedRepository;
        private IApplicationUserDTOMapper userDTOMapper;
        private IQuizEntityMapper quizEntityMapper;

        public List<QuizDTO> Quizes => GetAllQuizes();

        public QuizDTOMapper(IQuizRepository quizRepository,
            //IAssignedRepository assignedRepository,
            IApplicationUserDTOMapper userDTOMapper,
            IQuizEntityMapper quizEntityMapper)
        {
            this.quizRepository = quizRepository;
            //this.assignedRepository = assignedRepository;
            this.userDTOMapper = userDTOMapper;
            this.quizEntityMapper = quizEntityMapper;
        }

        public QuizDTO Create(string id)
        {
            Quiz quiz = quizRepository.GetById(id);
            return CreateQuiz(quiz);
        }

        public List<QuizDTO> CreateUserFavouriteQuizes(string userId)
        {
            var favouriteQuizes = quizRepository.Quizes.
                Where(
                    q => q.AssignedUsers.
                    Any(a => a.AssignType == Data.Entities.AssignType.Favourite && a.ApplicationUserId.Equals(userId))
                ).ToList();
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            if (favouriteQuizes.Count == 0)
                return quizesDTO;
            foreach (var q in favouriteQuizes)
            {
                quizesDTO.Add(CreateQuiz(q));
            }
            return quizesDTO;
        }

        public List<QuizDTO> CreateAllUserQuizes(string userId)
        {

            List<Quiz> quizes = quizRepository.Quizes.
                Where(q => q.ApplicationUserId.Equals(userId)).ToList();
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            if (quizes != null)
            {
                quizes = quizes.ToList();
                foreach (var q in quizes)
                {
                    quizesDTO.Add(CreateQuiz(q));
                    //var v = q.AssignedUsers;
                    //foreach (var a in v)
                    //    System.Console.WriteLine("CONSOLE22: " + a.QuizId);
                }
            }
            //AddStaticMockData(quizesDTO);
            return quizesDTO;
        }

        private QuizDTO CreateQuiz(Quiz quiz)
        {
            if (quiz == null)
                return null;
            string userId = quiz.ApplicationUserId;

            DateTime creationDate = quiz.CreationDate;

            ApplicationUserDTO user = userDTOMapper.CreateUserWithId(userId);
            DTOs.QuizType? type = quiz.QuizType.ToDTOQuizType();

            QuizDTO quizDTO = new QuizDTO(quizEntityMapper.Update)
            {

                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                ApplicationUserId = user.Id,
                //QuizSettingsId = "1",
                QuizType = type,
                FilePath = quiz.FilePath,
                ApplicationUser = user,
                CreationDate = creationDate.ToString(),
                FavouritesUsers = new List<ApplicationUserDTO>()
            };

            List<string> idsList = new List<string>();
            foreach (var ass in quiz.AssignedUsers)
            {
                idsList.Add(ass.ApplicationUserId);
            }
            //foreach (var id in idsList)
            //{
            //    var userr = userFactory.CreateUserWithId(id);
            //    if (userr != null)
            //        quizDTO.AddToFavouritesUsers(userr);
            //        //list.Add(user);
            //}
            FillList(quizDTO.FavouritesUsers, idsList);
            //quizDTO.AddToFavouritesUsers(user);

            return quizDTO;
        }

        public List<QuizDTO> SearchByName(string name)
        {
            List<Quiz> quizes = quizRepository.Quizes.ToList();
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            quizes = quizes.ToList();
            foreach (var q in quizes)
            {
                quizesDTO.Add(CreateQuizByName(q, name));

            }

            for (int j = 0; j < quizesDTO.Count; j++)
            {

                if (quizesDTO[j] == null)
                {
                    quizesDTO.RemoveAt(j);
                    j--;

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

            ApplicationUserDTO user = userDTOMapper.CreateUserWithId(userId);
            //if (user == null || title == null || filePath == null)
            //  return null;
            DTOs.QuizType? type = q.QuizType.ToDTOQuizType();
            QuizDTO quizDTO = new QuizDTO(quizEntityMapper.Update)
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

        public List<QuizDTO> GetAllQuizes()
        {
            List<Quiz> quizes = quizRepository.Quizes.ToList();
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            quizes = quizes.ToList();
            foreach (var q in quizes)
            {
                quizesDTO.Add(CreateQuiz(q));
            }
            return quizesDTO;
        }

        public void SaveQuiz(QuizDTO quizDTO)
        {
            Quiz quiz = new Quiz
            {
                Id = quizDTO.Id,
                QuizSettingsId = quizDTO.QuizSettingsId,
                Title = quizDTO.Title,
                Description = quizDTO.Description,
                ApplicationUserId = quizDTO.ApplicationUserId,
                QuizType = QuizTypeExtension.ToEntityQuizType(quizDTO.QuizType)
            };
            quizRepository.Update(quiz);
        }

        private void FillList(List<ApplicationUserDTO> list, IEnumerable<string> usersIds)
        {
            foreach (var id in usersIds)
            {
                var user = userDTOMapper.CreateUserWithId(id);
                if (user != null)
                    list.Add(user);
            }
        }
    }
}
