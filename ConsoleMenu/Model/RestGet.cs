using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model
{
    static class Rest
    {
        public static string Get(MenuItem menu)
        {
            var retStr = "";
            try
            {
                var stream = WebRequest
                    .Create(menu.Source)
                    .GetResponse()
                    .GetResponseStream();

                retStr = new StreamReader(stream).ReadToEnd();
            }
            catch (Exception)
            {
                retStr = menu.Contents;
            }

            return retStr;
        }
    }
}
