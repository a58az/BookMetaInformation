/*
 * Created by SharpDevelop.
 * User: admin
 * Date: 22.07.2014
 * Time: 17:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace epubWorker
{
	/// <summary>
	/// Description of BookInfo.
	/// </summary>
	public class BookInfo
	{
		public string Author { get; set; }
		public string Title { get; set; }
		public string Language { get; set; }
		
		public BookInfo() { }
		
		public BookInfo(string auth, string title, string language)
		{
			Author = auth;
			Title = title;
			Language = language;
		}
	}
}
