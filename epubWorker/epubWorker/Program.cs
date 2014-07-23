/*
 * Created by SharpDevelop.
 * User: admin
 * Date: 22.07.2014
 * Time: 17:14
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Xml;

namespace epubWorker
{
	class Program
	{
		public static void Main(string[] args)
		{

			//worker("test.epub");

			DirectoryInfo di = new DirectoryInfo("./");
			GetFiles(di);

			if (Directory.Exists("./temp"))
			{
				Directory.Delete("./temp", true);
			}

			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}

		static void GetFiles(DirectoryInfo di)
		{
			foreach(FileInfo file in di.GetFiles())
			{
				if(file.Extension.ToLower() == ".epub" || file.Extension.ToLower() == ".zip")
				{
					worker(file.FullName);
				}
			}

			foreach(DirectoryInfo dir in di.GetDirectories())
			{
				if (dir.Name != "library")
					GetFiles(dir);
			}
		}

		private static char[] reservedChars = {'<', '>', '|', '\\', '/', ':', '?', '*', '"', '_'};
		private static string path;

		static void worker(string filename)
		{
			string ext = Path.GetExtension(filename);
			Unpack unp = new Unpack(filename);
			XmlDocument file = unp.getFileInfo();
			GetBookInfo gbi = new GetBookInfo(file, ext);

			BookInfo bi = gbi.ReadInfo();

			path = Path.Combine(Directory.GetCurrentDirectory(), "library");
			path = Path.Combine(path, bi.Author);

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			if (bi.Language != null)
			{
				path = Path.Combine(path, bi.Language);
			}
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			var name = bi.Title;
			foreach(var c in reservedChars)
			{
				name = name.Replace(c, '_');
			}
			if (name.Length > 50)
			{
				name = name.Substring(0, 50);
				name = name.Substring(0, name.LastIndexOf(" "));

				path = Path.Combine(path, name);
			}
			else
			{
				path = Path.Combine(path, name);
			}
			path += Path.GetExtension(filename);

			File.Copy(filename, path, true);

		}
	}
}