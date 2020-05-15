using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web
{
    public static class Logger
    {
        public static ILog Log { get; } = LogManager.GetLogger("LOGGER");
    }
}