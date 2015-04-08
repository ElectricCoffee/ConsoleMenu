using System;
using System.IO;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            var menuPath = Path.Combine(Environment.CurrentDirectory, @"View\Menu.json");

            new Controller.MenuController(menuPath).Run(); // run the UI
        }
    }
}
