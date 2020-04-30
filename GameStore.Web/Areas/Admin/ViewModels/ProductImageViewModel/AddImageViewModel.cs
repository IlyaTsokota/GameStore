
using System.Collections.Generic;

using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.ProductImageViewModel
{
    public class AddImageViewModel
    {
        public int ProductId { get; set; }

        public List<HttpPostedFileBase> PostedFiles { get; set; }
    }
}