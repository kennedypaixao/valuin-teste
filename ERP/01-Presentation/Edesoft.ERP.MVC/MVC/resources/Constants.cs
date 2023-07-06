using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Edesoft.ERP.MVC.MVC.resources
{
	public class Meta
	{
		public Meta()
		{
			meta = new Dictionary<string, string>();
			meta.Add("viewport", "width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no");
			meta.Add("description", "");
			meta.Add("author", "");
		}
		public string charset = "UTF-8";
		public Dictionary<string,string> meta { get; set; }
	}

	public static class Constants
	{
		public const string CompanyShortName = "Comp";
		public const string ModuleName = "Backoffice";
		public static Meta meta = new Meta();
	}
}