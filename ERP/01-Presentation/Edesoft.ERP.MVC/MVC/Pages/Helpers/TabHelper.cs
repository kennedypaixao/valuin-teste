using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace Edesoft.ERP.MVC.MVC.Pages.Helpers
{
	public class TabContentHelper
	{
		public string Id { get; set; }
		public HelperResult Content { get; set; }
	}

	public class TabNavigationHelper
	{
		public string Caption { get; set; }
		public TabContentHelper TabContent { get; set; } = new TabContentHelper();
	}

	public class TabHeper
	{
		public List<TabNavigationHelper> NavigationTabs { get; set; } = new List<TabNavigationHelper>();
	}
}