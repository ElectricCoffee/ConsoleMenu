﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model.Menu
{
    class TextMenu : MenuItem
    {
        public override void DisplayContents()
        {
            if (!String.IsNullOrEmpty(Contents))
                Console.WriteLine(Contents);
        }
    }
}