using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model
{
    class GitHubStatus
    {
        [JsonProperty(PropertyName = "status")]
        public String Status { get; set; }
        [JsonProperty(PropertyName = "body")]
        public String Body { get; set; }
        [JsonProperty(PropertyName = "created_on")]
        public DateTime CreationTime { get; set; }

        public override String ToString()
        {
            return String.Format(
                "Status : {0}\n" +
                "Body   : {1}\n" +
                "Created: {2}", Status, Body, CreationTime);
        }
    }
}
