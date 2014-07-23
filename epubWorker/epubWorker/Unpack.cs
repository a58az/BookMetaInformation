/*
 * Created by SharpDevelop.
 * User: admin
 * Date: 22.07.2014
 * Time: 17:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Threading;
using System.IO;
using Ionic.Zip;

namespace epubWorker
{
	/// <summary>
	/// Description of Unpack.
	/// </summary>
	public class Unpack
	{
		string fileName;
		string extension;
		
		public Unpack(string fn)
		{
			fileName = fn;
			extension = Path.GetExtension(fileName);
		}
		
		public XmlDocument getFileInfo()
		{
			if (Directory.Exists("./temp"))
			{
				Directory.Delete("./temp", true);
			}
			using (ZipFile zip = ZipFile.Read(fileName))
			{
				zip.ExtractAll("./temp");
			}
			
			string name = null;
			DirectoryInfo di = new DirectoryInfo("./temp");
			XmlDocument xml = new XmlDocument();
			if (extension == ".epub")
			{
				name = getOpfPath(di);
			}
			else if (extension == ".zip")
			{
				name = di.GetFiles()[0].FullName;
			}
			
			xml.Load(name);
			
			return xml;
		}
		
		private string getOpfPath(DirectoryInfo di)
		{
			foreach(var file in di.GetFiles())
			{
				if (file.Extension.ToLower() == ".opf")
				{
					return file.FullName;
				}
			}
			
			var x = di.GetDirectories();
			foreach(var dir in di.GetDirectories())
			{
				var fname = getOpfPath(dir);
				if (fname != null)
					return fname;
			}
			
			return null;
		}
	}
}
