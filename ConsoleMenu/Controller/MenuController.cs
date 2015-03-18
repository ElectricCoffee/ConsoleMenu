using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ConsoleMenu.Model;

namespace ConsoleMenu.Controller
{
    class MenuController // use stack for history
    {
        private static ConsoleColor
            highlightBGColour = ConsoleColor.DarkCyan,
            normalBGColour = ConsoleColor.Black;

        private static String menuPath = Path.Combine(Environment.CurrentDirectory, @"View\Menu.json");
        private MenuItem currentMenu;
        private Stack<MenuItem> menuHistory;
        private Int32 menuIndex = 0;

        public MenuController()
        {
            menuHistory = new Stack<MenuItem>();
            currentMenu = JsonConvert.DeserializeObject<MenuItem>(File.ReadAllText(menuPath));
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
                        menuIndex = menuIndex < currentMenu.Submenus.Count - 1 ? menuIndex + 1 : menuIndex;
                        break;
                    case ConsoleKey.Enter: // enter menu item
                        if (currentMenu.Submenus != null)
                        {
                            menuHistory.Push(currentMenu);
                            currentMenu = currentMenu.Submenus[menuIndex];
                            menuIndex = 0;
                        }
                        break;
                    case ConsoleKey.Backspace: // go back one level
                        if (menuHistory.Count >= 1)
                        {
                            currentMenu = menuHistory.Pop();
                            menuIndex = 0;
                        }
                        break;
                    case ConsoleKey.Escape: // go back all levels
                        sentinel = 0;
                        break;
                    default:
                        break;
                }
            } while (sentinel > 0);
        }

        private void Draw()
        {
            Console.Clear(); // clear the screen 
            Console.WriteLine(currentMenu.Title); // write the current menu's title

            DisplayContents(currentMenu);

            for (Int32 i = 0; currentMenu.Submenus != null && i < currentMenu.Submenus.Count; i++)
            {
                ColourIfSelected(i, menuIndex, () => Console.WriteLine("  [{0}] {1}", i, currentMenu.Submenus[i].Title));
            }

            Console.WriteLine(
                "\n----------------------------------------" +
                "\nUse [UP] and [DOWN] to navigate" +
                "\nUse [RET] to select and [ESC] to go back"
            );
        }

        private void ColourIfSelected(Int32 currentIndex, Int32 requiredIndex, Action print)
        {
            if (currentIndex == requiredIndex) Console.BackgroundColor = highlightBGColour;

            print();
            Console.BackgroundColor = normalBGColour;
        }

        private void DisplayContents(MenuItem menu)
        {
            switch (menu.SourceType)
            {
                case SourceType.File:
                    break;
                case SourceType.Folder:
                    break;
                case SourceType.Json:
                    break;
                case SourceType.RestGet:
                    var response = JsonConvert.DeserializeObject<GitHubStatus>(Rest.Get(menu));
                    Console.WriteLine(response);
                    break;
                case SourceType.Rss:
                    break;
                case SourceType.Text:
                    if (!String.IsNullOrEmpty(menu.Contents))
                        Console.WriteLine(menu.Contents);
                    break;
                case SourceType.Url:
                    break;
                default:
                    break;
            }
        }
    }
}
