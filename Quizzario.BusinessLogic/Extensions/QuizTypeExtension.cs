namespace Quizzario.BusinessLogic.Extensions
{
    public static class QuizTypeExtension
    {
        public static BusinessLogic.DTOs.QuizType? ToDTOQuizType(this Data.Entities.QuizType? value)
        {
            if (value == null)
                return null;
            int val = (int)value;
            DTOs.QuizType quizType = (DTOs.QuizType)val;
            return quizType;
        }

    }
}
