using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GameStore.Web.Extensions
{
    public static class HttpPostedFileBaseExtension
    {
        public static byte[] ToByteArray(this HttpPostedFileBase file)
        {
            var rdr = new BinaryReader(file.InputStream);
            var res = rdr.ReadBytes(file.ContentLength);
            return res;
        }
    }
}