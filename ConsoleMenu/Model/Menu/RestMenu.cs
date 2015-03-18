using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model.Menu
{
    class RestMenu : MenuItem
    {
        public override void DisplayContents()
        {
            var response = JsonConvert.DeserializeObject<GitHubStatus>(Rest.Get(this));
            Console.WriteLine(response);
        }
    }
}
