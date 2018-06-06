//using Quizzario.BusinessLogic.Abstract;
//using Quizzario.BusinessLogic.DTOs;
//using Quizzario.Data.Entities;

//namespace Quizzario.BusinessLogic.Mappers
//{
//    public class ApplicationUserEntityMapper : IApplicationUserEntityMapper
//    {
//        public void CreateUserEntity(ApplicationUserDTO userDTO)
//        {
//            ApplicationUser user = new ApplicationUser();
//            var propInfo = userDTO.GetType().GetProperties();
//            foreach (var item in propInfo)
//            {
//                user.GetType().GetProperty(item.Name).SetValue(user, item.GetValue(userDTO, null), null);
//            }
//        }

//        public void Update(ApplicationUserDTO user)
//        {

//        }
//    }
//}
