
using System;

namespace Monospace2
{
	/// <summary>
	/// Atom feed item
	/// </summary>
	/// <remarks>
	/// Inspired by the RSSRepository but converted to ATOM
	/// http://softwareandservice.wordpress.com/2009/09/21/building-a-rss-reader-iphone-app-using-monotouch/
	/// </remarks>
	public class FeedItem
	{
		public FeedItem ()
		{
		}
		
		public string Title
		{
			get;set;
		}
		public string Content
		{
			get;set;
		}
		public DateTime Published
		{
			get;set;
		}
		public int NumComments
		{
			get;set;
		}
	    public string Url
		{
			get;set;
		}
	}
}
