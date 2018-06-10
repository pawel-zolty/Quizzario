using Quizzario.BusinessLogic.Abstract;
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
        CreateQuizViewModel CreateQuizViewModelFromQuizDTO(QuizDTO quiz);
    }

    public class DTOMapper : IQuizDTOMapperFromViewModel
    {
        private IQuizEntityMapper quizEntityMapper;

        public DTOMapper(IQuizEntityMapper quizEntityMapper)
        {
            this.quizEntityMapper = quizEntityMapper;
        }

        public QuizDTO Map(CreateQuizViewModel viewmodel, ApplicationUserDTO user)
        {
            QuizDTO dto = new QuizDTO(quizEntityMapper.AddNewQuiz)
            {
                Id = viewmodel.Id,
                Description = viewmodel.Description,
                Title = viewmodel.Title,
                ApplicationUserId = user.Id,
                ApplicationUser = user,
                FilePath = viewmodel.Path,
                QuizType = viewmodel.QuizType,
                QuizAccessLevel = viewmodel.QuizAccessLevel,
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
        public CreateQuizViewModel CreateQuizViewModelFromQuizDTO(QuizDTO quiz)
        {
            CreateQuizViewModel viewModel = new CreateQuizViewModel();
            viewModel.Id = quiz.Id;
            viewModel.Description = quiz.Description;
            viewModel.Title = quiz.Title;
            viewModel.Path = quiz.FilePath;
            int i = 0;
            foreach (var q in quiz.Questions)
            {
                CreateQuestionViewModel questionViewModel = new CreateQuestionViewModel();
                questionViewModel.Question = q.Question;
                int j = 0;
                foreach (var a in quiz.Questions[i].Answers)
                {
                    CreateAnswerViewModel answerViewModel = new CreateAnswerViewModel();
                    questionViewModel.Answers.Add(answerViewModel);
                    questionViewModel.Answers[j].Answer = a.Answer;
                    questionViewModel.Answers[j].isCorrect = a.isCorrect;
                    j++;
                }
                i++;
            }


            return viewModel;
        }
    }
}
