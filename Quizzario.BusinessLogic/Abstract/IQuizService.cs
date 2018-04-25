﻿using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IQuizService
    {
        IEnumerable<QuizDTO> GetAllUserQuizes(string userId);
    }
}
