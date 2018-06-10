using Quizzario.BusinessLogic.Extensions;
using Quizzario.Data.Abstracts;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using Quizzario.BusinessLogic.Abstract;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Quizzario.BusinessLogic.Mappers
{
    public class QuizDTOMapper : IQuizDTOMapper
    {
        private IQuizRepository quizRepository;
        private IApplicationUserRepository applicationUserRepository;
        private IApplicationUserDTOMapper userDTOMapper;
        private IQuizEntityMapper quizEntityMapper;
        private IJSONRepository jsonRepository;

        public List<QuizDTO> Quizes => GetAllQuizes();

        public QuizDTOMapper(IQuizRepository quizRepository,
            IApplicationUserRepository applicationUserRepository,
            IApplicationUserDTOMapper userDTOMapper,
            IQuizEntityMapper quizEntityMapper,
            IJSONRepository jsonRepository)
        {
            this.quizRepository = quizRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.userDTOMapper = userDTOMapper;
            this.quizEntityMapper = quizEntityMapper;
            this.jsonRepository = jsonRepository;
        }

        public QuizDTO Create(string id)
        {
            Quiz quiz = quizRepository.GetById(id);
            return CreateQuiz(quiz);
        }

        public List<QuizDTO> CreateUserFavouriteQuizes(string userId)
        {
            var type = Data.Entities.AssignType.Favourite;
            return CreateUserAssignedQuizes(userId, type);
        }

        public List<QuizDTO> CreateUserAssignedToPrivateQuizes(string userId)
        {
            var type = Data.Entities.AssignType.AssignedToPrivate;
            return CreateUserAssignedQuizes(userId, type);
        }

        public List<ApplicationUserDTO> CreateAssignedToPrivateQuizUsers(string quizId)
        {
            var type = Data.Entities.AssignType.AssignedToPrivate;
            return CreateAssignedQuizUsers(quizId, type);
        }

        private List<ApplicationUserDTO> CreateAssignedQuizUsers(string quizId, Data.Entities.AssignType type)
        {
            var users = applicationUserRepository.Users.
                            Where(
                                u => u.AssignedUsers.
                                Any(a => a.AssignType == type && a.QuizId.Equals(quizId))
                            ).ToList();
            List<ApplicationUserDTO> usersDTO = new List<ApplicationUserDTO>();
            if (users.Count == 0)
                return usersDTO;
            foreach (var u in users)
            {
                usersDTO.Add(userDTOMapper.CreateUserWithId(u.Id));
            }
            return usersDTO;
        }

        private List<QuizDTO> CreateUserAssignedQuizes(string userId, Data.Entities.AssignType type)
        {
            var quizes = quizRepository.Quizes.
                            Where(
                                q => q.AssignedUsers.
                                Any(a => a.AssignType == type && a.ApplicationUserId.Equals(userId))
                            ).ToList();
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            if (quizes.Count == 0)
                return quizesDTO;
            foreach (var q in quizes)
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
                }
            }
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
            DTOs.QuizAccessLevel? accessLevel = quiz.QuizAccessLevel.ToDTOQuizAccessLevel();
            ICollection<DTOs.ScoreDTO> scoreDTO = quiz.Scores.ToDTOQuizScore();
            int attemps = 0;
            for (int i = 0; i < scoreDTO.Count(); i++)
            {
                if (quiz.ApplicationUserId == scoreDTO.ElementAt(i).ApplicationUserId)
                {
                    attemps++;
                }
            }
            QuizDTO quizDTO = new QuizDTO(quizEntityMapper.Update)
            {

                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                ApplicationUserId = user.Id,
                //QuizSettingsId = "1",
                QuizType = type,
                QuizAccessLevel = accessLevel,
                FilePath = quiz.FilePath,
                ApplicationUser = user,
                AllScore = scoreDTO,

                CreationDate = creationDate.ToString(QuizDTO.CreationDateFormat)
            };

            List<string> idsList = new List<string>();
            foreach (var ass in quiz.AssignedUsers.
                Where(a => a.AssignType == Data.Entities.AssignType.Favourite))
            {
                idsList.Add(ass.ApplicationUserId);
            }
            FillList(quizDTO.FavouritesUsers, idsList);
            idsList = new List<string>();
            foreach (var ass in quiz.AssignedUsers.
                Where(a => a.AssignType == Data.Entities.AssignType.AssignedToPrivate))
            {
                idsList.Add(ass.ApplicationUserId);
            }
            FillList(quizDTO.PrivateAssignedUsers, idsList);
            quizDTO.Questions = this.LoadQuestions(quizDTO);
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

            var regexname = Regex.Match(q.Title.ToLower(), @".*" + name.ToLower() + ".*");
            if (regexname.Groups[0].Value != q.Title.ToLower() || q.QuizAccessLevel == Data.Entities.QuizAccessLevel.Private)
            {
                return null;
            }
            var quizDTO = CreateQuiz(q);
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
                FilePath = quizDTO.FilePath,
                QuizAccessLevel = QuizAccessLevelExtension.ToEntityQuizAccessLevel(quizDTO.QuizAccessLevel),
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

        private List<QuestionDTO> LoadQuestions(QuizDTO quizDTO)
        {
            string json = this.jsonRepository.LoadWithAbsolutePath(quizDTO.FilePath);
            if (json != null)
            {
                var deserialized = JsonConvert.DeserializeObject<QuizDTO.JSONScheme>(json);
                return deserialized.Questions;
            }
            else
            {
                throw new NullReferenceException("Failed to load quiz questions from json file");
            }
        }
        public List<QuizDTO> GetAllPublicQuizes()
        {
            List<Quiz> quizes = quizRepository.Quizes.ToList();
            List<QuizDTO> quizesDTO = new List<QuizDTO>();
            quizes = quizes.ToList();
            foreach (var q in quizes)
            {
                if (q.QuizAccessLevel == Data.Entities.QuizAccessLevel.Public)
                {
                    quizesDTO.Add(CreateQuiz(q));

                }
               
            }
            return quizesDTO;
            throw new NotImplementedException();
        }
    }
}
