using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quizzario.BusinessLogic.DTOs;

namespace Quizzario.Models.QuizViewModels
{
    public class SearchingByNameModel
    {
        public IEnumerable<QuizDTO> Quizes { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int allQuizes { get; set; }
    }
}
