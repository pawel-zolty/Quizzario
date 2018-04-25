using System;
using Quizzario.Data.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzario.Models.QuizViewModels
{
    public class SearchingByNameModel
    {
        public IEnumerable<QuizDTO> Quizes { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
