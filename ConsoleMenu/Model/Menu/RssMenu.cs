using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;

namespace ConsoleMenu.Model.Menu
{
    /// <summary>
    /// Downloads the contents of an RSS feed, and formats it so it gets displayed nicely
    /// </summary>
    class RssMenu : MenuItem
    {
        private List<MenuItem> localSubMenus;
        private DateTime lastChecked;

        /// <summary>
        /// Checks if a minute has passed before updating the feed
        /// this ensures the feed won't get updated every time the screen renders, reducing lag
        /// </summary>
        /// <returns>A new feed if the minute has passed, otherwise it returns the old one only</returns>
        private List<MenuItem> UpdateFeed()
        {
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
                    subFeed.SourceType = Model.SourceType.Text;
                    localSubMenus.Add(subFeed);
                }
            }
            return localSubMenus;
        }

        public override string DisplayContents()
        {
            try
            {
                Submenus = UpdateFeed();
                return "";
            }
            catch (Exception e)
            {
                return String.Format("{0}\n{1}", Contents, e.Message);
            }
        }
    }
}
