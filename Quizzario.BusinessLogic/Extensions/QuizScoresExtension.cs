

using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Extensions
{
    public static class QuizScoresExtension
    {

        public static ICollection<BusinessLogic.DTOs.ScoreDTO> ToDTOQuizScore(this ICollection<Data.Entities.Score> value)
        {
            if (value == null)
                return null;
            ICollection<DTOs.ScoreDTO> scores = new List<DTOs.ScoreDTO>();
            foreach (Data.Entities.Score s in value)
            {
                BusinessLogic.DTOs.ScoreDTO scoreDTO = new DTOs.ScoreDTO();
                scoreDTO.Id = s.Id;
                scoreDTO.QuizId = s.QuizId;
                scoreDTO.Result = s.Result;
                scoreDTO.ApplicationUserId = s.ApplicationUserId;
                scores.Add(scoreDTO);

            }

            return scores;
        }
        public static ICollection<Data.Entities.Score> ToQuizScore(this ICollection<BusinessLogic.DTOs.ScoreDTO> value)
        {
            if (value == null)
                return null;
            ICollection<Data.Entities.Score> scores = new List<Data.Entities.Score>();
            foreach (BusinessLogic.DTOs.ScoreDTO s in value)
            {
                Data.Entities.Score score = new Data.Entities.Score();
                score.Id = s.Id;
                score.QuizId = s.QuizId;
                score.Result = s.Result;
                score.ApplicationUserId = s.ApplicationUserId;
                scores.Add(score);

            }

            return scores;
        }
    }
}
