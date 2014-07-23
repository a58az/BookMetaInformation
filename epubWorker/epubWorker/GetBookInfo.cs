/*
 * Created by SharpDevelop.
 * User: admin
 * Date: 22.07.2014
 * Time: 17:48
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;

namespace epubWorker
{
	/// <summary>
	/// Description of GetBookInfo.
	/// </summary>
	public class GetBookInfo
	{
		XmlDocument xml;
		string extension;
		public GetBookInfo(XmlDocument xmlDoc, string ext)
		{
			xml = xmlDoc;
			extension = ext;
		}

		public BookInfo ReadInfo()
		{
			switch(extension)
			{
					case ".epub": return ReadInfoEpub();
					case ".zip": return ReadInfoFB2();
					default: return null;
			}
		}

		private BookInfo ReadInfoEpub()
		{
			BookInfo bi = new BookInfo();
			XmlNodeList xmlNodes;
			// creator-author
			xmlNodes = xml.GetElementsByTagName("dc:creator");
			foreach(XmlNode node in xmlNodes)
			{
				if (bi.Author != null) bi.Author += "-";
				bi.Author = node.InnerText;
				Console.WriteLine(node.InnerText);
			}
			// title
			xmlNodes = xml.GetElementsByTagName("dc:title");
			string title = xmlNodes.Item(0).InnerText;
			bi.Title = title;

			// language
			xmlNodes = xml.GetElementsByTagName("dc:language");
			string lang = xmlNodes.Item(0).InnerText;
			bi.Language = lang;

			Console.WriteLine(title);
			Console.WriteLine(lang);

			return bi;
		}

		private BookInfo ReadInfoFB2()
		{
			BookInfo bi = new BookInfo();
			XmlNodeList xmlNodes;

			xmlNodes = xml.GetElementsByTagName("author").Item(0).ChildNodes;
			string author = String.Format("{0} {1}", xmlNodes.Item(0).InnerText, xmlNodes.Item(1).InnerText);

			xmlNodes = xml.GetElementsByTagName("book-title");
			string title = xmlNodes.Item(0).InnerText;

			xmlNodes = xml.GetElementsByTagName("lang");
			string lang = xmlNodes.Item(0).InnerText;

			bi.Author = author;
			bi.Title = title;
			bi.Language = lang;

			Console.WriteLine(author);
			Console.WriteLine(title);
			Console.WriteLine(lang);

			return bi;
		}
	}
}
