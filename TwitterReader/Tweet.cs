
using System;

namespace Monospace2
{
	/// <summary>
	/// Just the two useful parts of a Tweet Atom entry
	/// </summary>
	public class Tweet
	{
		public Tweet ()
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
	}
}
