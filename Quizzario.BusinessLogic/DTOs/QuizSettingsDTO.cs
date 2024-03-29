﻿using System;

namespace Quizzario.BusinessLogic.DTOs
{
    public class QuizSettingsDTO
    {
        public string QuizId { get; set; }
        public int AttemptLimit { get; set; }
        public TimeSpan? TimeLimit { get; set; }

        public QuizDTO Quiz { get; set; }
    }
}