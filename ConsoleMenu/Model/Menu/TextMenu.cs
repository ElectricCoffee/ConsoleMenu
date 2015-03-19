using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model.Menu
{
    /// <summary>
    /// Basic menu item, displays the contents if there are any
    /// </summary>
    class TextMenu : MenuItem
    {
        public override String DisplayContents()
        {
            if (!String.IsNullOrEmpty(Contents))
                return Contents;
            else return "";
        }
    }
}
