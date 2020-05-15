using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.LogViewModels
{
    public class IndexLogViewModel
    {
        public List<LogViewModel> Logs { get; set; }

        public Pager Pager { get; set; }
    }
}