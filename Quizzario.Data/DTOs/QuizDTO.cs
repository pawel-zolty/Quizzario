﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzario.Data.DTOs
{
    public enum QuizType
    {
        Quiz, Exam, Test
    }

    public class QuizDTO
    {    
        public string Id { get; set; }
        public string Title { get; set; }
        public string ApplicationUserId { get; set; }        
        public string QuizSettingsId { get; set; }
        public QuizType? QuizType { get; set; }
        public string FilePath { get; set; }

        public virtual ApplicationUserDTO ApplicationUser { get; set; }
        public virtual ICollection<AssignedUserDTO> AssignedUsers { get; set; }
        public virtual ICollection<ScoreDTO> Scores { get; set; }
        public virtual QuizSettingsDTO QuizSettings { get; set; }
    }
}
