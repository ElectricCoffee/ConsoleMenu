using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model.Menu
{
    /// <summary>
    /// Generates a sub-menu based on the content of the current directory
    /// </summary>
    class FolderMenu : MenuItem
    {
        public override string DisplayContents()
        {
            if (Directory.Exists(Source))
            {
                var directory = new DirectoryInfo(Source);
                Submenus = new List<MenuItem>();
                foreach (var file in directory.GetFiles())
                {
                    var menuItem = new FileMenu();
                    menuItem.Title = "[F] " + file.Name;
                    menuItem.Source = file.FullName;
                    menuItem.SourceType = Model.SourceType.File;

                    Submenus.Add(menuItem);
                }

                foreach (var dir in directory.GetDirectories())
                {
                    var menuItem = new FolderMenu();
                    menuItem.Title = "[D] " + dir.Name;
                    menuItem.Source = dir.FullName;
                    menuItem.SourceType = Model.SourceType.Folder;

                    Submenus.Add(menuItem);
                }

                return "";
            }
            return String.Format("ERROR: The provided source [{0}] does not point to a valid directory", Source);

            
        }
    }
}
