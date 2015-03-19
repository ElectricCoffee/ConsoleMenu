using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model
{
    /// <summary>
    /// Handles polymorphic JSON deserialisation
    /// </summary>
    class MenuItemConverter : JsonConverter
    {
        /// <summary>
        /// Required method
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(MenuItem).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Uses the SourceType field to figure out which menu type we're dealing with
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Required by the inheritance, but not currently being used
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
