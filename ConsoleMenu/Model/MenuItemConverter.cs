using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model
{
    class MenuItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(MenuItem).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var item = JObject.Load(reader);
            var srcType = item["SourceType"].Value<String>();

            object target = null;

            switch (srcType)
            {
                case SourceType.File:    
                    target = new Menu.FileMenu();
                    break;
                case SourceType.Folder:
                    target = new Menu.FolderMenu();
                    break;
                case SourceType.RestGet: 
                    target = new Menu.RestMenu();
                    break;
                case SourceType.Rss:     
                    target = new Menu.RssMenu();
                    break;
                case SourceType.Text:    
                    target = new Menu.TextMenu();
                    break;
                default: throw new ArgumentException("Invalid source type");
            }

            serializer.Populate(item.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
