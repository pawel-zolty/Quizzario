namespace Quizzario.BusinessLogic.Extensions
{
    public static class QuizAccessLevelExtension
    {
        public static BusinessLogic.DTOs.QuizAccessLevel? ToDTOQuizAccessLevel(this Data.Entities.QuizAccessLevel? value)
        {
            if (value == null)
                return null;
            int val = (int)value;
            DTOs.QuizAccessLevel quizAccessLevel = (DTOs.QuizAccessLevel)val;
            return quizAccessLevel;
        }

        public static Data.Entities.QuizAccessLevel? ToEntityQuizAccessLevel(this BusinessLogic.DTOs.QuizAccessLevel? value)
        {
            if (value == null)
                return null;
            int val = (int)value;
            Data.Entities.QuizAccessLevel quizAccessLevel = (Data.Entities.QuizAccessLevel)val;
            return quizAccessLevel;
        }
    }
}
