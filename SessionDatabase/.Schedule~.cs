using System;
using System.Collections.Generic;
using System.Linq;
using Path = System.IO.Path;
using SQLite;

namespace Monospace2
{
	[MonoTouch.Foundation.Preserve(AllMembers=true)]
	public class Session
	{
		public string Date {get;set;}
		public string StartTime {get;set;}
		public string EndTime {get;set;}
		public string Title {get;set;}
		public string Brief {get;set;}
		public string Location {get;set;}
		public string Type {get;set;}
		public string Url {get;set;}
	}

	[MonoTouch.Foundation.Preserve(AllMembers=true)]
	public class Speaker
	{
		public int Id {get;set;}
		public string Name {get;set;}
		public string Position {get;set;}
		public string Company {get;set;}
		public string Bio {get;set;}
		public string Twittername {get;set;}
		public string BlogUrl {get;set;}
	}
	
	[MonoTouch.Foundation.Preserve(AllMembers=true)]
	public class Stream
	{
		public string Name {get;set;}
	}
	
	[MonoTouch.Foundation.Preserve(AllMembers=true)]
	public class SessionDate
	{
		public string Date {get;set;}
	}
	
	public class Database : SQLiteConnection
	{
		public Database (string path) : base (path)
		{
			CreateTable<Session>();
		}
		public IEnumerable<SessionDate> GetDates ()
        {
                return Query<SessionDate> ("select distinct Date AS [Date] from Session order by date");
        }
		public IEnumerable<Session> GetSessions (string stream)
        {
                return Query<Session> ("select * from Session where type in ('B',?) order by date, starttime, type",stream);
        }
		public IEnumerable<Stream> GetStreams ()
		{
		        return Query<Stream> ("select distinct type AS [Name] from Session where type like 'S%'");
		}
	}
}
