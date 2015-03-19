using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model.Menu
{
    /// <summary>
    /// Deserialises the response from the Rest class (unfortunately only supports github deserialisation)
    /// </summary>
    class RestMenu : MenuItem
    {
        public override String DisplayContents()
        {
            return JsonConvert.DeserializeObject<GitHubStatus>(Rest.Get(this)).ToString();
        }
    }
}
