using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model.Menu
{
    class FileMenu : MenuItem
    {
        //public String 
        public override string DisplayContents()
        {
            return Contents;
        }
    }
}
