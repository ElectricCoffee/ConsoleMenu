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
            var json = File.ReadAllText(menuPath);
            currentMenu = JsonConvert.DeserializeObject<MenuItem>(json, new MenuItemConverter());
        }

        public void Run()
        {
            var alive = true;

            while (alive)
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
                        alive = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Draw()
        {
            Console.Clear(); // clear the screen 
            Console.WriteLine(currentMenu.Title); // write the current menu's title

            // indent the contents
            var contents = "  " + currentMenu.DisplayContents().Replace("\n", "\n  ");

            Console.WriteLine(contents);

            if (currentMenu.Submenus != null)
            {
                // helper variables that define the rendering behaviour.
                var count = currentMenu.Submenus.Count; // gets max-number of sub-menus

                /* set the for-loop's starting value to be the currently selected item's position - 1
                 * if and only if the menu selector is on a menu item whose index is > 1 AND there are more than 10 items in the menu
                 * otherwise re-draw the menu at the first element; this is done to allow scrolling menus */
                var startingValue = menuIndex > 1 && count > 10 ? menuIndex - 1 : 0;

                /* never render more than 10 elements past the starting value, 
                 * this is so we don't overflow the screen with menu items */
                var buffer = startingValue + 10;

                for (Int32 i = startingValue; i < count && i < buffer; i++)
                {
                    if (i == menuIndex)
                        Console.BackgroundColor = highlightBGColour;

                    Console.WriteLine("  [{0,3}] {1}", i, currentMenu.Submenus[i].Title);
                    Console.BackgroundColor = normalBGColour;
                }

                if (buffer < count)
                {
                    Console.WriteLine("\nDisplaying item {0,3} out of {1,3}", buffer - 1, count - 1);
                }
                else Console.WriteLine();
            }

            Console.WriteLine(
                "--------------------------------------------------------" +
                "\nUse [UP] and [DN] to navigate" +
                "\nUse [RET] to select, [BSP] to go back, and [ESC] to quit" + 
                "\nCopyright 2015 @ Nikolaj Lepka"
            );
        }
    }
}
