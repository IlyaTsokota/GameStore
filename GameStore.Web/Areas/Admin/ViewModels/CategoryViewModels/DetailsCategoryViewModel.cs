using GameStore.Web.Areas.Admin.ViewModels.AttributeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.CategoryViewModels
{
    public class DetailsCategoryViewModel : CategoryViewModel
    {
        public List<AttributeViewModel> Attributes { get; set; }
    }
}