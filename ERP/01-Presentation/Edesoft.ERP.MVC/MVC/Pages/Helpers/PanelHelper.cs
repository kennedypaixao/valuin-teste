using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace Edesoft.ERP.MVC.MVC.Pages.Helpers
{
	public class PanelHelper
	{
		public string HeadingText { get; set; }
		public List<ButtonHelper> HeadingButtons { get; set; } = new List<ButtonHelper>();
		public bool ShowDefaultButtonsHeading { get; set; } = true;
		public HelperResult Body { get; set; }
		public bool Inverse { get; set; } = false;
	}
}