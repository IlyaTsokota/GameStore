using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.Models
{
    public class PagerViewModel<T> where T : class
    {
        public List<T> Entities { get; set; }

        public Pager Pager { get; set; }
    }
}