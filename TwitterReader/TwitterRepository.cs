using System;
using System.Linq;
using System.Xml.Linq; // requires System.Xml.Linq added to References
using System.Collections.Generic;

namespace Monospace2
{
	/// <summary>
	/// Load a twitter Atom feed
	/// </summary>
	/// <remarks>
	/// Inspired by the RSSRepository from 
	/// http://softwareandservice.wordpress.com/2009/09/21/building-a-rss-reader-iphone-app-using-monotouch/
	/// </remarks>
	public static class TwitterRepository
	{
		public static IList<Tweet> GetFeeds(string url)
		{
			var tweets = new List<Tweet>();
			MonoTouch.UIKit.UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			
            try
			{
				XNamespace ns = "http://www.w3.org/2005/Atom";
				XDocument rssFeed = XDocument.Load(url);
				
				Console.Write(rssFeed.ToString());			// # DEBUG OUTPUT
		    
        			tweets = (from item in rssFeed.Descendants(ns + "entry")
                    select new Tweet	
                    {
						Title  = item.Element(ns + "title").Value,
                        Content = item.Element(ns + "content").Value
						
                    }).ToList();
				Console.WriteLine("feeds {0} items", tweets.Count);
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
			return tweets;                  			
		}
	}
}
