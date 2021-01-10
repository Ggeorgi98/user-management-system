using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagementSystem.Business.Models
{
    public class PagedListModel<TModel> where TModel : BaseResponseModel
    {
        public List<TModel> Content { get; }

        public PagerModel Pager { get; }

        public PagedListModel(List<TModel> items, int sourceCount, int pageNumber, int pageSize)
        {
            if (pageSize == 0)
                pageSize = 1;

            int pagesCount = (int)Math.Ceiling(sourceCount / (double)pageSize);

            if (pagesCount == 0)
                pagesCount = 1;

            if (pageNumber == 0 || (pageNumber > pagesCount && pageNumber != 1))
                pageNumber = pagesCount;

            Pager = new PagerModel
            {
                CurrentPageNumber = pageNumber,
                ItemsPerPage = pageSize,
                TotalPages = pagesCount,
                TotalItemsCount = sourceCount
            };

            Content = items;
        }
    }
}
