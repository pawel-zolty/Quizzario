using Quizzario.Data.Abstracts;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using Quizzario.BusinessLogic.Abstract;
using Quizzario.BusinessLogic.Extensions;
using System.Collections.Generic;

namespace Quizzario.BusinessLogic.Mappers
{
    public class ApplicationUserDTOMapper : IApplicationUserDTOMapper
    {
        private IApplicationUserRepository repository;

        public ApplicationUserDTOMapper(IApplicationUserRepository repository)
        {
            this.repository = repository;
        }

        public ApplicationUserDTO CreateUserWithId(string id)
        {
            ApplicationUser user = repository.GetById(id);
            return CreateUser(user);
        }

        public ApplicationUserDTO GetByName(string userName)
        {
            ApplicationUser user = repository.GetByName(userName);
            return CreateUser(user);
        }

        private ApplicationUserDTO CreateUser(ApplicationUser user)
        {
            if (user == null)
                return null;
            ICollection<DTOs.ScoreDTO> scoreDTO = user.Scores.ToDTOQuizScore();
            ApplicationUserDTO userDTO = new ApplicationUserDTO(user.UserName)
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Origin = user.Origin,
                //Quizes = new List<QuizDTO>()
                Scores = scoreDTO
                //TO DO 
            };
            //var favouriteQuizesIds = user.AssignedUsers.
            //    Where(a => a.AssignType == 0).
            //    Select(a => a.QuizId);
            //Fill(userDTO.Favourites, favouriteQuizesIds);
            return userDTO;
        }

        //private void Fill(List<QuizDTO> favourites, IEnumerable<string> favouriteQuizesIds)
        //{
        //    foreach (var id in favouriteQuizesIds)
        //    {
        //        var quiz = CreateQuiz(id);
        //        if (quiz != null)
        //            favourites.Add(quiz);
        //    }
        //}
    }
}
