﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzario.Data.Entities
{
    public class QuizSettings
    {
        [Key, ForeignKey("Quiz"), Required]
        public string QuizId { get; set; }
        public int AttemptLimit { get; set; }
        public TimeSpan? TimeLimit { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}