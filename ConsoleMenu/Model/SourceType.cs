using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model
{
    /// <summary>
    /// Exists for the sole purpose of avoiding the use of magic strings as well as reducing the probability of human error
    /// </summary>
    static class SourceType
    {
        public const string
            File = "File",
            Folder = "Folder",
            Text = "Text",
            Rss = "Rss",
            RestGet = "RestGet";
    }
}
