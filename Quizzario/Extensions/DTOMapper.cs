﻿using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.Models.QuizViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzario.Extensions
{
    public interface IQuizDTOMapperFromViewModel
    {
        QuizDTO Map(CreateQuizViewModel viewmodel, ApplicationUserDTO user);
    }

    public class DTOMapper : IQuizDTOMapperFromViewModel
    {
        private IQuizEntityMapper quizEntityMapper;
        private string dateformat;

        public DTOMapper(IQuizEntityMapper quizEntityMapper)
        {
            this.quizEntityMapper = quizEntityMapper;
        }

        public QuizDTO Map(CreateQuizViewModel viewmodel, ApplicationUserDTO user)
        {
            QuizDTO dto = new QuizDTO(quizEntityMapper.AddNewQuiz)
            {
                Id=viewmodel.id,
                Description = viewmodel.Description,
                Title = viewmodel.Title,
                ApplicationUserId = user.Id,
                ApplicationUser = user,
                QuizType = QuizType.Test,
                CreationDate = DateTime.Today.ToString(QuizDTO.CreationDateFormat),
            };
            foreach (var question in viewmodel.Questions)
            {
                dto.Questions.Add(DTOMapper.Map(question));
            }
            return dto;
        }

        public static QuestionDTO Map(CreateQuestionViewModel viewmodel)
        {
            QuestionDTO dto = new QuestionDTO
            {
                Question = viewmodel.Question
            };
            foreach (var answer in viewmodel.Answers)
            {
                dto.Answers.Add(DTOMapper.Map(answer));
            }
            return dto;
        }

        public static AnswerDTO Map(CreateAnswerViewModel viewModel)
        {
            return new AnswerDTO { isCorrect = viewModel.isCorrect, Answer = viewModel.Answer };
        }
    }
}
