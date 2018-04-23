using Quizzario.BusinessLogic.Models;
using Quizzario.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzario.BusinessLogic.Abstract
{
    public interface IQuizService
    {
        IEnumerable<QuizDTO> GetAllQuizes();
    }
}
