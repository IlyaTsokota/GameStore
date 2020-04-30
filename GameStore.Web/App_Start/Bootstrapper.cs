using GameStore.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            AutoMapperConfiguration.Configure();
        }
    }
}