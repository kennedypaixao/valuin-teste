using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Edesoft.ERP.MVC.ViewModel.Global
{
	public class ResultJsonViewModel
	{
		public ResultJsonViewModel() { 
			status_code = HttpStatusCode.OK;
			message = "OK";
		}
		public dynamic data { get; set; }
		public HttpStatusCode status_code { get; set; }
		public string message { get; set; }
	}
}