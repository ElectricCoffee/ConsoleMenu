using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model
{
    static class SourceType // because enums don't work well with strings
    {
        public const string
            File = "File",
            Folder = "Folder",
            Text = "Text",
            Rss = "Rss",
            RestGet = "RestGet";
    }
}
