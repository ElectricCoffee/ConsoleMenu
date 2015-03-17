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
        private static ConsoleColor
            highlightBGColour = ConsoleColor.DarkCyan,
            normalBGColour = ConsoleColor.Black;

        private static String menuPath = Path.Combine(Environment.CurrentDirectory, @"View\Menu.json");
        private Model.MenuItem menus;
        private Int32 menuIndex = 0;

        public MenuController()
        {
            menus = JsonConvert.DeserializeObject<Model.MenuItem>(File.ReadAllText(menuPath));
        }

        public void Run()
        {
            int sentinel = 1;
            do
            {
                Draw();

                var keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow: // decrease index
                        menuIndex = menuIndex > 0 ? menuIndex - 1 : menuIndex;
                        break;
                    case ConsoleKey.DownArrow: // increase index
                        menuIndex = menuIndex < menus.Submenus.Count - 1 ? menuIndex + 1 : menuIndex;
                        break;
                    case ConsoleKey.Enter:
                        
                        break;
                    case ConsoleKey.Escape:
                        sentinel = 0;
                        break;
                    default:
                        break;
                }
            } while (sentinel > 0);
        }

        public void Draw()
        {
            Console.Clear(); // clear the screen 
            Console.WriteLine(menus.Title); // write the current menu's title

            for (int i = 0; i < menus.Submenus.Count; i++)
            {
                ColourIfSelected(i, menuIndex, () => Console.WriteLine("  [{0}] {1}", i, menus.Submenus[i].Title));
            }
        }

        public void ColourIfSelected(int currentIndex, int requiredIndex, Action print)
        {
            if (currentIndex == requiredIndex) Console.BackgroundColor = highlightBGColour;
                
            print();
            Console.BackgroundColor = normalBGColour;
        }
    }
}
