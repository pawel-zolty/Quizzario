﻿using System.Collections.Generic;

namespace Quizzario.BusinessLogic.DTOs
{
    public class QuestionDTO
    {
        public QuestionDTO()
        {
            Answers = new List<AnswerDTO>();
        }
        public string Question { get; set; }
        public List<AnswerDTO> Answers { get; set; }
        public bool Multiple { get; set; }
    }
}