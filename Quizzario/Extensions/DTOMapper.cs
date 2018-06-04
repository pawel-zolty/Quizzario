﻿using Quizzario.BusinessLogic.DTOs;
using Quizzario.Models.QuizViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzario.Extensions
{
    public class DTOMapper
    {
        public static QuizDTO Map(CreateQuizViewModel viewmodel, ApplicationUserDTO user)
        {
            QuizDTO dto = new QuizDTO
            {
                Description = viewmodel.Description,
                Title = viewmodel.Title,
                ApplicationUserId = user.Id,
                QuizType = QuizType.Test,
                CreationDate = DateTime.Today.ToShortDateString(),
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
            foreach(var answer in viewmodel.Answers)
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
