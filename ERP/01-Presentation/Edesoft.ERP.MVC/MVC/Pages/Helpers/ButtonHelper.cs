using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edesoft.ERP.MVC.MVC.Pages.Helpers
{
	public class ButtonHelper
	{
		public string Name { get; set; }
		public string Type { get; set; } = "btn-default";
		public string Text { get; set; }
		public string Icon { get; set; } = string.Empty;
		public bool Dismiss { get; set; }

	}
}