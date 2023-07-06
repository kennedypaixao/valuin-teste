using Edesoft.ERP.Domain.DataBase;
using Edesoft.ERP.Tools.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Edesoft.ERP.MVC.MVC
{

	public class Modulo
	{
		public Modulo()
		{
			Menus = new List<Menu>();
		}
		public string Descricao { get; set; }
		public string Icon { get; set; }

		public List<Menu> Menus { get; set; }
		public Menu AddNewMenu(string Descricao, string Link, string Icon = "", string ClassIcon = "")
		{
			var menu = new Menu
			{
				Descricao = Descricao,
				Link = Link,
				Icon = Icon,
				ClassIcon = ClassIcon != "" ? ClassIcon : "fa-duotone",
			};
			Menus.Add(menu);
			return menu;
		}
	}
	public class Menu
	{
		public Menu()
		{
			Itens = new List<Menu>();
			Link = "";
			ClassIcon = "fa-duotone";
		}

		public string Descricao { get; set; }
		public string Icon { get; set; }
		public string ClassIcon { get; set; }
		public string Link { get; set; }
		public List<Menu> Itens { get; set; }
		public Menu AddNewItem(string Descricao, string Link, string Icon = "", string ClassIcon = "")
		{
			var item = Itens.FirstOrDefault(p => p.Descricao == Descricao);
			if (item == null)
			{
				item = new Menu();
				item.Descricao = Descricao;
				item.Link = Link;
				item.Icon = Icon;
				item.ClassIcon = ClassIcon != "" ? ClassIcon : "fa-duotone";
				Itens.Add(item);
			}
			return item;
		}

	}
	public class MenuControl
	{
		private MenuControl()
		{
			Modulos = new List<Modulo>();
		}
		private static MenuControl _instance;
		public static MenuControl Instance
		{
			get
			{
				if (_instance == null)
					NewInstance();

				return _instance;
			}
		}
		public static void NewInstance()
		{
			_instance = new MenuControl();
		}
		public List<Modulo> Modulos { get; set; }
		public Modulo AddNewModulo(string Descricao, string Icon)
		{
			var menu = new Modulo()
			{
				Descricao = Descricao,
				Icon = Icon
			};

			Modulos.Add(menu);

			return menu;
		}
		private Modulo Modulo(string Descricao)
		{
			return Modulos.FirstOrDefault(p => p.Descricao == Descricao);
		}
	}

	static public class SideMenu
	{
		public static List<Modulo> GetModulos()
		{

			MenuControl.NewInstance();

			var sideBarModulos = SessionPersister.User.SideBarItens;

			Modulo addModulo(MenuSideBarPersister SideBarModulo)
			{
				return MenuControl.Instance.AddNewModulo(SideBarModulo.Name, SideBarModulo.Icon);
			}
			void addSideMenu(Menu menu, MenuSideBarPersister sideBarItem)
			{
				foreach (var item in sideBarItem.Itens)
				{
					var sideMenu = menu.AddNewItem(item.Name, item.Hash ?? "", item.Icon);
					addSideMenu(sideMenu, item);
				}
			}
			void addMenusModulo(Modulo modulo, MenuSideBarPersister sideBarItem)
			{
				foreach (var item in sideBarItem.Itens)
				{
					var sideMenu = modulo.AddNewMenu(item.Name, item.Hash ?? "", item.Icon);
					addSideMenu(sideMenu, item);
				}
			}
			foreach (var sideBarModulo in sideBarModulos)
			{
				var modulo = addModulo(sideBarModulo);
				addMenusModulo(modulo, sideBarModulo);
			}

			return MenuControl.Instance.Modulos;
		}
		
	}
}
