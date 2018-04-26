﻿using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Abstracts
{
    public interface IQuizDTOFactory
    {
        QuizDTO Create(string id);
        IEnumerable<QuizDTO> CreateAllUserQuizes(string userId);
        IEnumerable<QuizDTO> CreateUserFavouriteQuizes(string userId);
        void AddQuizToFavourite(string userId, string quizId);
        void RemoveQuizFromFavourite(string userId, string quizId);
    }
}
