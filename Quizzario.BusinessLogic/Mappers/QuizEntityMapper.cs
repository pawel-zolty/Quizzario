using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.BusinessLogic.Extensions;
using Quizzario.Data.Abstracts;
using Quizzario.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Quizzario.BusinessLogic.Mappers
{
    public class QuizEntityMapper : IQuizEntityMapper
    {
        private IQuizRepository quizRepository;
        private IJSONRepository jsonRepository;

        public QuizEntityMapper(IQuizRepository quizRepository, IJSONRepository jsonRepository)
        {
            this.quizRepository = quizRepository;
            this.jsonRepository = jsonRepository;
        }

        private Quiz CreateQuiz(QuizDTO quizDTO)
        {
            Quiz quiz = GetQuizEntity(quizDTO);
            quiz.AssignedUsers = new List<AssignedUser>();
            var type = Data.Entities.AssignType.Favourite;
            CreateQuizEntityAssignList(quizDTO, quiz, type, quizDTO.FavouritesUsers);
            type = Data.Entities.AssignType.AssignedToPrivate;
            CreateQuizEntityAssignList(quizDTO, quiz, type, quizDTO.PrivateAssignedUsers);
            quiz.Scores = new List<Score>();
            foreach(var s in quizDTO.AllScore)
            {
                Score score = new Score
                {
                    ApplicationUserId = s.ApplicationUserId,
                    QuizId = s.QuizId,
                    Result = s.Result
                };
                quiz.Scores.Add(score);
            }
            //quiz.CreationDate = DateTime.ParseExact(quizDTO.CreationDate, QuizDTO.CreationDateFormat, null);
            quiz.CreationDate = quizDTO.CreationDate;
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
            //var kolekcja pytan json lub xml = json mapper
            quizRepository.Update(quiz);
            this.jsonRepository.SaveWithAbsolutePath(quizDTO.FilePath, quizDTO.JSON);
        }

        public void AddNewQuiz(QuizDTO quizDTO)
        {
            var quiz = CreateQuiz(quizDTO);
            //var kolekcja pytan json lub xml = json save = / quiz.jsonFile pewnie bd trzeba cos dodac do tego jsona.
            quizRepository.Add(quiz);
            quizDTO.Id = quiz.Id;
            quizDTO.FilePath = this.jsonRepository.BuildAbsolutePath(quizDTO.Id);
            this.jsonRepository.SaveWithAbsolutePath(quizDTO.FilePath, quizDTO.JSON);
            this.Update(quizDTO);
        }
        
        private Quiz GetQuizEntity(QuizDTO quizDTO)
        {
            var quiz = quizRepository.GetById(quizDTO.Id);
            if (quiz == null)
                quiz = new Quiz();
            else 
                quiz.Id = quizDTO.Id;
            quiz.ApplicationUserId = quizDTO.ApplicationUserId;
            quiz.FilePath = quizDTO.FilePath;
            quiz.Description = quizDTO.Description;
            quiz.QuizType = QuizTypeExtension.ToEntityQuizType(quizDTO.QuizType);
            quiz.QuizAccessLevel = QuizAccessLevelExtension.ToEntityQuizAccessLevel(quizDTO.QuizAccessLevel);
            quiz.Title = quizDTO.Title;

            //quiz.CreationDate = System.DateTime.ParseExact(quizDTO.CreationDate, QuizDTO.CreationDateFormat, null);
            quiz.CreationDate =quizDTO.CreationDate;
            return quiz;
        }        
    }
}
