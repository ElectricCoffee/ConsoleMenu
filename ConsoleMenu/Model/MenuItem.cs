using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model
{
    /// <summary>
    /// Base-class for all the menu classes
    /// Contains the core functionality and a basic DisplayContents
    /// </summary>
    abstract class MenuItem
    {
        public String Title { get; set; }
        public String Contents { get; set; }
        public List<MenuItem> Submenus { get; set; }
        public String Source { get; set; }
        public String SourceType { get; set; }

        /// <summary>
        /// Handles the formatting of the output for the given menu item
        /// </summary>
        /// <returns></returns>
        public virtual String DisplayContents()
        {
            // default text
            return "This feature is not currently implemented";
        }
    }
}
