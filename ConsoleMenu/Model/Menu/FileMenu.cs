using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model.Menu
{
    class FileMenu : MenuItem
    {
        public override string DisplayContents()
        {
            var file = new FileInfo(Source);

            return String.Format(
                "Name         : {0}\n" +
                "Size         : {1}B\n"+
                "Attributes   : {2}\n" +
                "Read-Only    : {3}\n" +
                "Last Accessed: {4}\n" +
                "Last Written : {5}",
                Source, file.Length, file.Attributes.ToString(), file.IsReadOnly, file.LastAccessTime, file.LastWriteTime);
        }
    }
}
