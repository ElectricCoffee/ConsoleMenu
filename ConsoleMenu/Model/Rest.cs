using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model
{
    /// <summary>
    /// Handles REST requests
    /// </summary>
    static class Rest
    {
        /// <summary>
        /// Sends a GET request to a given website and spits out the string
        /// </summary>
        /// <param name="menu">The given menuItem of which the source is gotten</param>
        /// <returns></returns>
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
