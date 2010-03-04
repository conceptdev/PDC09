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
		/// <summary>
		/// For debugging, I really like the 'default' implementation suggested by MonoDevelop
		/// </summary>
		public override string ToString ()
		{
			return string.Format("[Session: Date={0}, StartTime={1}, EndTime={2}, Title={3}, Brief={4}, Location={5}, Type={6}, Url={7}]", Date, StartTime, EndTime, Title, Brief, Location, Type, Url);
		}

	}

	[MonoTouch.Foundation.Preserve(AllMembers=true)]
	public class Speaker
	{
		public int Id {get;set;}
		public string Name {get;set;}
		public string Position {get;set;}
		public string Company {get;set;}
		public string Bio {get;set;}
		public string TwitterName {get;set;}
		public string BlogUrl {get;set;}
		/// <summary>
		/// For debugging, I really like the 'default' implementation suggested by MonoDevelop
		/// </summary>
		public override string ToString ()
		{
			return string.Format("[Speaker: Id={0}, Name={1}, Position={2}, Company={3}, Bio={4}, TwitterName={5}, BlogUrl={6}]", Id, Name, Position, Company, Bio, TwitterName, BlogUrl);
		}

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
		public IEnumerable<Session> GetSessions (string date)
        {
                return Query<Session> ("select * from Session where date=? order by date, starttime, type", date);
        }
		public IEnumerable<Stream> GetStreams ()
		{
		        return Query<Stream> ("select distinct type AS [Name] from Session where type like 'S%'");
		}
		public IEnumerable<Speaker> GetSpeakers ()
		{
			    return Query<Speaker> ("select * from Speaker order by Id");
		}
	}
}
