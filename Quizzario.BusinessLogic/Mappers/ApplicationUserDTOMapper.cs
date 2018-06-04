﻿using Quizzario.Data.Abstracts;
using Quizzario.BusinessLogic.DTOs;
using Quizzario.Data.Entities;
using Quizzario.BusinessLogic.Abstract;

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

        private ApplicationUserDTO CreateUser(ApplicationUser user)
        {
            if (user == null)
                return null;

            ApplicationUserDTO userDTO = new ApplicationUserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Origin = user.Origin,
                //Quizes = new List<QuizDTO>()
                //Scores
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
