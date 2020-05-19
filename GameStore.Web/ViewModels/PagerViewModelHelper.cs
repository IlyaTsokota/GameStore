using AutoMapper;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels
{
    public static class PagerViewModelHelper<T> where T : class
    {
        public static PagerViewModel<T> ToPagerViewModel<TY>(List<TY> entities, int page = 1, int pageSize = 10)
        {
            var viewModels = entities.Skip((page - 1) * pageSize).Take(pageSize)
                .Select(Mapper.Map<TY, T>).ToList();
            var pager = new Pager(entities.Count, page, pageSize);
            var productListModel = new PagerViewModel<T> { Pager = pager, Entities = viewModels };
            return productListModel;
        }
    }
}