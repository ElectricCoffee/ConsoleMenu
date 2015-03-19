﻿using Newtonsoft.Json;
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

        private static ConsoleColor[] bgColours = { ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta, ConsoleColor.DarkYellow };
        private static String[] colNames = { "[CYN]", "[MAG]", "[YEL]" };

        private static String menuPath = Path.Combine(Environment.CurrentDirectory, @"View\Menu.json");
        private MenuItem currentMenu;
        private Stack<MenuItem> menuHistory;
        private Int32 
            menuIndex   = 0,
            colourIndex = 0;

        public MenuController()
        {
            menuHistory = new Stack<MenuItem>();
            var json = File.ReadAllText(menuPath);
            currentMenu = JsonConvert.DeserializeObject<MenuItem>(json, new MenuItemConverter());
        }

        /// <summary>
        /// Draws the contents of the screen, and then waits for user input
        /// The user input determines how the UI will update.
        /// </summary>
        public void Run()
        {
            var alive = true;

            while (alive)
            {
                Draw();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow: // decrease index
                        if (currentMenu.Submenus != null)
                            menuIndex = menuIndex > 0 ? menuIndex - 1 : menuIndex;
                        break;
                    case ConsoleKey.DownArrow: // increase index
                        if (currentMenu.Submenus != null)
                            menuIndex = menuIndex < currentMenu.Submenus.Count - 1 ? menuIndex + 1 : menuIndex;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (colourIndex > 0)
                            colourIndex--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (colourIndex < bgColours.Length - 1)
                            colourIndex++;
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

                highlightBGColour = bgColours[colourIndex];
            }
        }

        /// <summary>
        /// Re-draws the screen based on the current content
        /// </summary>
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
                    Console.WriteLine("\nDisplaying item {0} out of {1}", buffer - 1, count - 1);
                }
                else Console.WriteLine();
            }

            Console.WriteLine(
                "--------------------------------------------------------" +
                "\nUse [UP] and [DN] to navigate" +
                "\nUse [RET] to select, [BSP] to go back, and [ESC] to quit"
            );
            
            // allows the colour switching, it's a useless feature, but it's fun.
            Console.Write("Use [RGHT] and [LEFT] to select the highlight colour: ");
            for (int i = 0; i < colNames.Length; i++)
            {
                if (colourIndex == i)
                    Console.BackgroundColor = highlightBGColour;
                Console.Write(colNames[i]);
                Console.BackgroundColor = normalBGColour;
                Console.Write(" "); // it's there to get the spacing right
            }
            Console.WriteLine("\nCopyright 2015 @ Nikolaj Lepka");
        }
    }
}
