using Quizzario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzario.Services
{
    public interface IPagingInfoService
    {
        PagingInfo GetMetaData(int collectionSize, int selectedPageNumber, int itemsPerPage);
    }

    public class PagingInfoService : IPagingInfoService
    {        
        private const int NumberOfNodesInPaginatedList = 9;
        private List<Page> pages;

        public PagingInfo GetMetaData(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            if (collectionSize == 0)
            {
                return GetCollectionSizeZeroModel();
            }

            pages = BuildPageNodes(collectionSize, selectedPageNumber, itemsPerPage);
            return new PagingInfo
            {
                PreviousPage = BuildPreviousPage(collectionSize, selectedPageNumber, itemsPerPage),
                Pages = pages,
                NextPage = BuildNextPage(collectionSize, selectedPageNumber, itemsPerPage)
            };
        }
        
        private static PagingInfo GetCollectionSizeZeroModel()
        {
            return new PagingInfo
            {
                PreviousPage = new PreviousPage
                {
                    Display = false
                },
                Pages = new List<Page>(),
                NextPage = new NextPage
                {
                    Display = false
                }
            };
        }

        private PreviousPage BuildPreviousPage(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            var display = selectedPageNumber > 1 && collectionSize >= itemsPerPage;
            return new PreviousPage
            {
                Display = display,
                PageNumber = display ? pages.First(x => x.IsCurrent).PageNumber - 1 : 1
            };
        }     

        private NextPage BuildNextPage(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            var display = selectedPageNumber < GetLastPageInCollection(collectionSize, itemsPerPage);
            return new NextPage
            {
                Display = display,
                PageNumber = display ? pages.First(x => x.IsCurrent).PageNumber + 1 : NumberOfNodesInPaginatedList + 1
            };
        }
        
        #region Page Nodes

        private List<Page> BuildPageNodes(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            var lastPage = GetLastPageInCollection(collectionSize, itemsPerPage);
            //Partial List is not full list
            if (NumberOfNodesInPaginatedList * itemsPerPage > collectionSize)
            {
                return BuildPartialList(collectionSize, selectedPageNumber, itemsPerPage);
            }
            int half = NumberOfNodesInPaginatedList / 2;
            //Full list which start at page1
            if (selectedPageNumber <= NumberOfNodesInPaginatedList - half)
            {
                return BuildStartList(selectedPageNumber);
            }
            //Full list which end at page{lastPage}
            if (selectedPageNumber > lastPage - (NumberOfNodesInPaginatedList - half))
            {
                return BuildEndList(collectionSize, selectedPageNumber, itemsPerPage);
            }
            // We are at an in between collection of node in paginated list
            return BuildFullList(selectedPageNumber);
        }

        /// <summary>
        /// Build a full (NumberOfNodesInPaginatedList) collection of page nodes
        /// [ ][ ][ ][x][x][x][x][x][x][x][x][x][x][ ][ ][ ]
        /// </summary>
        private List<Page> BuildFullList(int selectedPageNumber)
        {
            var pages = new List<Page>();
            int half = NumberOfNodesInPaginatedList / 2;
            for (int i = NumberOfNodesInPaginatedList - 1; i >= 0; i--)
            {
                pages.Add(BuildNode(selectedPageNumber + half - i));
            };
            int index = NumberOfNodesInPaginatedList % 2 == 0 ?
                (NumberOfNodesInPaginatedList / 2) - 1 : NumberOfNodesInPaginatedList / 2;
            pages[index].IsCurrent = true;

            return pages;
        }

        /// <summary>
        /// Build a full (NumberOfNodesInPaginatedList) collection of page nodes
        /// [x][x][ ][ ][ ][ ][ ][ ][ ][ ] ][ ][ ][ ][ ][ ]
        /// </summary>
        private List<Page> BuildPartialList(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            var pages = new List<Page>();
            for (var i = 0; i < GetLastPageInCollection(collectionSize, itemsPerPage); i++)
            {
                pages.Add(BuildNode(i + 1));
            }

            pages[selectedPageNumber - 1].IsCurrent = true;

            return pages;
        }

        /// <summary>
        /// Build list when selected page falls into first collection of node list
        /// Start shifting after three
        /// [*][*][*][x][x][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ]
        /// </summary>
        private List<Page> BuildStartList(int selectedPageNumber)
        {
            var pages = new List<Page>();
            for (int i = 0; i < NumberOfNodesInPaginatedList; i++)
            {
                pages.Add(BuildNode(i + 1));
            }
            pages[selectedPageNumber - 1].IsCurrent = true;
            return pages;
        }

        /// <summary>
        /// Build list when selected page falls into last collection of nodes
        /// Stop shifting after three from end
        /// [ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][x][x][*][*][*]
        /// </summary>
        private List<Page> BuildEndList(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            var lastPage = GetLastPageInCollection(collectionSize, itemsPerPage);

            var pages = new List<Page>();
            for (int i = NumberOfNodesInPaginatedList - 1; i >= 0; i--)
            {
                pages.Add(BuildNode(lastPage - i));
            }

            var unshiftedIndex = lastPage - selectedPageNumber;
            pages[NumberOfNodesInPaginatedList - 1 - unshiftedIndex].IsCurrent = true;

            return pages;
        }

        private Page BuildNode(int pageNumber)
        {
            return new Page
            {
                IsCurrent = false,
                PageNumber = pageNumber
            };
        }

        #endregion

        private int GetLastPageInCollection(int collectionSize, int itemsPerPage)
        {
            var lastPage = (double)collectionSize / itemsPerPage;
            if (lastPage % 1 == 0)
                return (int)lastPage;
            return (int)lastPage + 1;
        }
    }
}
