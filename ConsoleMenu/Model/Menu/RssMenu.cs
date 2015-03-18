using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;

namespace ConsoleMenu.Model.Menu
{
    class RssMenu : MenuItem
    {
        private List<MenuItem> localSubMenus;
        private DateTime lastChecked;

        private List<MenuItem> UpdateFeed()
        {
            // check if a minute has passed before updating the feed
            // this ensures the feed won't get updated every time the screen renders, reducing lag
            if (DateTime.Compare(lastChecked.AddMinutes(1), DateTime.Now) < 0)
            {
                lastChecked = DateTime.Now;
                var reader = XmlReader.Create(Source);
                var feed = SyndicationFeed.Load(reader);
                reader.Close();
                localSubMenus = new List<MenuItem>();

                foreach (var item in feed.Items)
                {
                    var subFeed = new TextMenu();
                    subFeed.Title = item.Title.Text;
                    subFeed.Contents = item.Summary.Text;
                    localSubMenus.Add(subFeed);
                }
            }
            return localSubMenus;
        }

        public override string DisplayContents()
        {
            Submenus = UpdateFeed();
            return "";
        }
    }
}
