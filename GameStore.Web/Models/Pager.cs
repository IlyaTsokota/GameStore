using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.Models
{
    public class Pager
    {
        public int CurrentPage { get; } // номер текущей страницы
        public int PageSize { get; } // кол-во объектов на странице
        public int TotalItems { get; } // всего объектов
        public int TotalPages { get; } // всего страниц
        public int StartPage { get; }

        public int EndPage { get; }

        public Pager(int currentPage, int totalItems, int pageSize = 10)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
        }

        public Pager(int totalItems, int? page = null, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            var currentPage = page ?? 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= startPage - 1;
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}