using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Controller
{
    enum SourceType
    {
        Json, Url, File, Folder, Text, Rss, RestGet
    }

    class MenuController
    {
        private static String menuPath = Path.Combine(Environment.CurrentDirectory, @"View\Menu.json");
        private Model.MenuItem menus;

        public MenuController()
        {
            var data = File.ReadAllText(menuPath);
            menus = JsonConvert.DeserializeObject<Model.MenuItem>(data);
        }

        public void Draw()
        {
            Console.WriteLine(menus.Title);
            menus.Submenus.ForEach(m => Console.WriteLine("|> {0}", m.Title));
        }
    }
}
