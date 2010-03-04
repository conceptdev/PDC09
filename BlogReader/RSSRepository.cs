
using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
namespace Monospace2
{
	/// <summary>
	/// http://softwareandservice.wordpress.com/2009/09/21/building-a-rss-reader-iphone-app-using-monotouch/
	/// </summary>
	public static class RSSRepository
	{
		public static IList<RSSEntry> GetFeeds(string url)
		{
			XDocument rssFeed = XDocument.Load(url);
			Console.Write(rssFeed.ToString());
			
		    var feeds = new List<RSSEntry>();
            try
			{
        			feeds = (from item in rssFeed.Descendants("item")
                    select new RSSEntry	
                    {
                        Title = item.Element("title").Value,
                        Content = item.Element("description").Value,
						Published = DateTime.Parse(item.Element("pubDate").Value),
                        
                        Url = item.Element("link").Value
			
                    }).ToList();
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				//Log here
			}
			return feeds;                  			
		}
	}
}
