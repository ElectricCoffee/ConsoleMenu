﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctlr = new Controller.MenuController();
            ctlr.Run();
            Console.ReadKey();
        }
    }
}
