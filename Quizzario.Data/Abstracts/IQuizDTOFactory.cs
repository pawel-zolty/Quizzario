﻿using Quizzario.Data.DTOs;
using System.Collections.Generic;

namespace Quizzario.Data.Abstracts
{
    public interface IQuizDTOFactory
    {
        QuizDTO Create(string id);
        IEnumerable<QuizDTO> CreateAllQuizes();
        IEnumerable<QuizDTO> SearchByName(string name);
    }
}
