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


        public Pager(int currentPage, int totalItems, int pageSize = 10)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
        }
    }
}