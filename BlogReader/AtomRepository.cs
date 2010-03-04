using System;
using System.Linq;
using System.Xml.Linq; // requires System.Xml.Linq added to References
using System.Collections.Generic;

namespace Monospace2
{
	/// <summary>
	/// Read Atom feed off Google/Blogspot via Google/Feedburner
	/// </summary>
	/// <remarks>
	/// Inspired by RSSRepository at
	/// http://softwareandservice.wordpress.com/2009/09/21/building-a-rss-reader-iphone-app-using-monotouch/
	/// </remarks>
	public static class AtomRepository
	{
		public static IList<AtomEntry> GetFeeds(string url)
		{
			var feeds = new List<AtomEntry>();
			MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			  
			try
			{
				XNamespace ns = "http://www.w3.org/2005/Atom";
				XDocument rssFeed = XDocument.Load(url);
				
				//Console.Write(rssFeed.ToString());			// # DEBUG OUTPUT
		    
        			feeds = (from item in rssFeed.Descendants(ns + "entry")
                    select new AtomEntry	
                    {
                        Title = item.Element(ns + "title").Value,
                        Content = item.Element(ns + "content").Value,
						Published = DateTime.Parse(item.Element(ns + "published").Value),
                        Url = item.Element(ns + "link").Value
			
                    }).ToList();
				Console.WriteLine("feeds {0} items", feeds.Count);
			}
			catch(Exception ex)
			{
				Console.WriteLine("OOPS " + ex.Message);
				//Log here
			}
			finally
			{
				MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			}
			return feeds;                  			
		}
	}
}
