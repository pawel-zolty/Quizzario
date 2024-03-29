﻿using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;
using System.Linq;

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

        public List<QuizDTO> GetUserAssignedToPrivateQuizes(string userId)
        {
            List<QuizDTO> quizes = quizDTOMapper.CreateUserAssignedToPrivateQuizes(userId);
            return quizes;
        }

        public List<ApplicationUserDTO> GetAssignedToPrivateQuizUsers(string quizId)
        {
            List<ApplicationUserDTO> users = quizDTOMapper.CreateAssignedToPrivateQuizUsers(quizId);
            return users;
        }

        public void AddQuizToFavourite(string userId, string quizId)
        {
            var user = userFactory.CreateUserWithId(userId);
            var quiz = quizDTOMapper.Create(quizId);
            quiz.AddToFavouritesUsers(user);
        }

        public void AddQuizToPrivateAssigned(string userId, string quizId)
        {
            var user = userFactory.CreateUserWithId(userId);
            var quiz = quizDTOMapper.Create(quizId);
            quiz.AddToPrivateAssignedUsers(user);
        }

        public void RemoveQuizFromFavourite(string userId, string quizId)
        {
            var user = userFactory.CreateUserWithId(userId);
            var quiz = quizDTOMapper.Create(quizId);
            quiz.RemoveFromFavouritesUsers(user);
        }

        public void RemoveQuizFromPrivateAssigned(string userId, string quizId)
        {
            var user = userFactory.CreateUserWithId(userId);
            var quiz = quizDTOMapper.Create(quizId);
            quiz.RemoveFromPrivateAssignedUsers(user);            
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
            List<QuizDTO> quizes;
            if (name == null)
            {
                quizes = quizDTOMapper.GetAllPublicQuizes();
            }
            else
            {
                quizes = quizDTOMapper.SearchByName(name);
            }
            return quizes;
        }
       
        private List<QuizDTO> GetAllQuizes()
        {
            List<QuizDTO> quizes = quizDTOMapper.GetAllQuizes();
            return quizes;
        }

        public void CreateQuiz(QuizDTO quiz)
        {
            quiz.Update();
            //quizEntityMapper.Update(quiz);
            //ZMIANA PO MERGU
        }

        public float? GetBestScore(string userId, string quizId)
        {
            QuizDTO quiz = quizDTOMapper.Create(quizId);
            ICollection<ScoreDTO> scoreDTO;
            scoreDTO = quiz.AllScore;
            float? bestScore=float.MinValue;
            foreach(ScoreDTO score in scoreDTO)
                {
                    if(score.ApplicationUserId==userId)
                    {
                    if (bestScore < score.Result)
                        bestScore = score.Result;
                    }
                }
            return bestScore;
        }

        public float? GetLastScore(string userId, string quizId)
        {

            throw new System.NotImplementedException();
        }

        public int GetUserAttemps(string userId, string quizId)
        {

            QuizDTO quiz = quizDTOMapper.Create(quizId);
            ICollection<ScoreDTO> scoreDTO;
            scoreDTO = quiz.AllScore;
            int attemps = 0;
            foreach (ScoreDTO score in scoreDTO)
            {
                if (score.ApplicationUserId == userId)
                {
                    attemps++;
                }
            }
            return attemps;
        }

        public QuizDTO GetQuiz(string quizId)
        {
            return quizDTOMapper.Create(quizId);
        }

        public float? AddResult(SolveDTO solvingDTO)
        {
            var quizId = solvingDTO.quizID;
            var quiz = quizDTOMapper.Create(quizId);
            var answers = solvingDTO.Answers;
            var score = new ScoreDTO
            {
                ApplicationUserId = solvingDTO.UserId,
                QuizId = quizId,
                Result = 0
            };
            int i = 0;
            foreach(var A in answers)//A is whole user answer
            {
                var pytanie = quiz.Questions.ElementAt(i);
                var j = 0;
                foreach(var a in A.userAnswers)//just one from multiple answer
                {
                    var odp = pytanie.Answers.ElementAt(j);
                }
            }
            i = 0;
            foreach(var q in quiz.Questions)
            {
                var j = 0;
                foreach(var a in q.Answers)
                {                    
                    bool isCorrect = a.isCorrect;
                    var c = answers.ElementAt(i).userAnswers;
                    if ((!isCorrect && !c.Any(o => o == j)) ||
                            (isCorrect && c.Any(o => o == j)))
                    {
                        ++j;
                    }
                    else
                    {
                        answers.ElementAt(i).isCorrect = false;
                        break;
                    }
                    answers.ElementAt(i).isCorrect = true;
                }                
                ++i;
            }

            foreach (var a in answers)
            {

                score.Result += a.isCorrect ? 1 : 0;                                
            }
            quiz.AddScore(score);
            return score.Result;
        }
    }
}
