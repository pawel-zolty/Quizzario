namespace Quizzario.BusinessLogic.Extensions
{
    public static class AssignedTypeExtension
    {
        public static BusinessLogic.DTOs.AssignType? ToDTOQuizType(this Data.Entities.AssignType? value)
        {
            if (value == null)
                return null;
            int val = (int)value;
            DTOs.AssignType quizType = (DTOs.AssignType)val;
            return quizType;
        }

        public static Data.Entities.AssignType? ToEntityQuizType(this BusinessLogic.DTOs.AssignType? value)
        {
            if (value == null)
                return null;
            int val = (int)value;
            Data.Entities.AssignType quizType = (Data.Entities.AssignType)val;
            return quizType;
        }
    }
}
