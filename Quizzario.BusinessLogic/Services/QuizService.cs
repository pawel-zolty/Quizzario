﻿using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.Models;
using Quizzario.Data.Abstracts;
using Quizzario.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzario.BusinessLogic.Services
{
    public class QuizService : IQuizService
    {
        private IQuizDTOFactory factory;

        public QuizService(IQuizDTOFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<QuizDTO> GetAllUserQuizes(string userId)
        {         
            IEnumerable<QuizDTO> quizes = factory.CreateAllUserQuizes(userId);
            return quizes;
        }
    }
}
