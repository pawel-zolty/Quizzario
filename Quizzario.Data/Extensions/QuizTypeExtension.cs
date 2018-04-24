using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzario.Data.Extensions
{
    public static class QuizTypeExtension
    {
        public static DTOs.QuizType? ToDTOQuizType(this Entities.QuizType? value)
        {
            if (value == null)
                return null;
            int val = (int)value;
            DTOs.QuizType quizType = (DTOs.QuizType)val;
            return quizType;
        }

    }
}
