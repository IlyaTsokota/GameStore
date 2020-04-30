using GameStore.Web.Models;

using System.Collections.Generic;


namespace GameStore.Web.Areas.Admin.ViewModels.CategoryViewModels
{
    public class IndexCategoryViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }

        public Pager Pager { get; set; }
    }
}