using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace Edesoft.ERP.MVC.MVC.Pages.Helpers
{
	public class TitleModalHelper
	{
		public string Text { get; set; }
		public HelperResult Custom { get; set; }
	}
	public class FooterModalHelper
	{
		public List<ButtonHelper> Buttons { get; set; } = new List<ButtonHelper>();
		public HelperResult Custom { get; set; }
	}
	public class ModalHelper
	{
		public string Name { get; set; }
		public string Size { get; set; } = "modal-lg";
		public TitleModalHelper Title { get; set; } = new TitleModalHelper();
		public HelperResult Body { get; set; }
		public FooterModalHelper Footer { get; set; } = new FooterModalHelper();
	}
}