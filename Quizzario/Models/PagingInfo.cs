using System.Collections.Generic;

namespace Quizzario.Models
{
    public class PagingInfo
    {
        public List<Page> Pages { get; set; }
        public PreviousPage PreviousPage { get; set; }
        public NextPage NextPage { get; set; }

        public PagingInfo()
        {
            Pages = new List<Page>();
        }
    }

    public class PreviousPage
    {
        public bool Display { get; set; }
        public int PageNumber { get; set; }
    }

    public class NextPage
    {
        public bool Display { get; set; }
        public int PageNumber { get; set; }
    }

    public class Page
    {
        public int PageNumber { get; set; }
        public bool IsCurrent { get; set; }
    }
}
