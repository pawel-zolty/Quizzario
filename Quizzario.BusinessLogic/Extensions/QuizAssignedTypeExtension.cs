namespace Quizzario.BusinessLogic.Extensions
{
    public static class QuizAssignedTypeExtension
    {
        public static BusinessLogic.DTOs.AssignType? ToDTOQuizAssignedType(this Data.Entities.AssignType? value)
        {
            if (value == null)
                return null;
            int val = (int)value;
            DTOs.AssignType quizType = (DTOs.AssignType)val;
            return quizType;
        }

        public static Data.Entities.AssignType? ToEntityQuizAssignedType(this BusinessLogic.DTOs.AssignType? value)
        {
            if (value == null)
                return null;
            int val = (int)value;
            Data.Entities.AssignType quizType = (Data.Entities.AssignType)val;
            return quizType;
        }
    }
}
