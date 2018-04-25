using Quizzario.BusinessLogic.DTOs;
using System.Collections.Generic;

namespace Quizzario.Models.QuizViewModels
{
    public class QuizListViewModel
    {
        public IEnumerable<QuizDTO> Quizes { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
