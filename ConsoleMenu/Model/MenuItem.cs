using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model
{
    abstract class MenuItem
    {
        public String Title { get; set; }
        public String Contents { get; set; }
        public List<MenuItem> Submenus { get; set; }
        public String Source { get; set; }
        public String SourceType { get; set; }
        public abstract void DisplayContents();
    }
}
