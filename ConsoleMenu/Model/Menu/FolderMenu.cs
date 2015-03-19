using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model.Menu
{
    class FolderMenu : MenuItem
    {


        public override string DisplayContents()
        {
            if (Directory.Exists(Source))
            {
                var dir = new DirectoryInfo(Source);
                Submenus = new List<MenuItem>();
                foreach (var file in dir.GetFiles())
                {
                    var menuItem = new FileMenu();
                    menuItem.Title = file.Name;
                    menuItem.Source = file.FullName;
                    menuItem.SourceType = Model.SourceType.File;

                    Submenus.Add(menuItem);
                }
            }

            return "";
        }
    }
}
