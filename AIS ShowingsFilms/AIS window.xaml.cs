using APPClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;


namespace AIS_ShowingFilms
{
	/// <summary>
	/// Логика взаимодействия для Window1.xaml
	/// </summary>
	public partial class AISwindow : Window
	{
		private List<AccessLevels> accessLevels;
		private List<AppMenuItems> menuItems;
		private Dictionary<string, Assembly> dllAsms;
		public AISwindow(Users user)
		{
			InitializeComponent();
			GetAccess(user);
			ConnectDLL();
			CreateMenu();
		}
		public void GetAccess(Users user)
		{
			using (AccessContext db = new AccessContext())
			{
				//Получаем список доступа к пунктам меню для данного пользователя
				accessLevels = db.AccessLevels.Where(x => x.UserId == user.UserId).ToList();
				//Находим существующие пункты меню из списка доступа
				menuItems = db.AppMenuItems.ToList();
			}
		}
		private void ConnectDLL()
		{
			dllAsms = new Dictionary<string, Assembly>();
			foreach (var item in menuItems)
			{
				if (item.Dllname != "Null" && !dllAsms.ContainsKey(item.Dllname))
				{
					if (item.Dllname == "builtIn")
						continue;
					Assembly asm = Assembly.LoadFrom(item.Dllname);
					dllAsms.Add(item.Dllname, asm);
				}
			}
		}
		private void CreateMenu()
		{
			using (AccessContext db = new AccessContext())
			{
				List<AppMenuItems> parents = FindOnlyParents();
				parents = parents.OrderBy(x => x.SequenceNumber).ToList();
				foreach (var parent in parents)
				{
					MenuItem item = ConnectParentWithChilds(parent);
					mainMenu.Items.Add(item);
				}
			}
		}
		private List<AppMenuItems> FindOnlyParents()
		{
			List<AppMenuItems> parents = new List<AppMenuItems>();
			foreach (AppMenuItems item in menuItems)
			{
				if (item.ParentItemId == 0)
					parents.Add(item);
			}
			return parents;
		}
		private List<AppMenuItems> FindChilds(AppMenuItems parent)
		{
			List<AppMenuItems> childs = new List<AppMenuItems>();
			childs.OrderBy(x => x.SequenceNumber);
			foreach (AppMenuItems item in menuItems)
			{
				if (item.ParentItemId == parent.ItemId)
					childs.Add(item);
			}
			if (childs.Count > 0)
				return childs;
			else
				return null;
		}
		private MenuItem ConnectParentWithChilds(AppMenuItems parent)
		{
			MenuItem item = CreateMenuItem(parent);
			List<AppMenuItems> childs = FindChilds(parent);
			if (childs == null)
				return item;
			foreach (AppMenuItems child in childs)
			{
				item.Items.Add(ConnectParentWithChilds(child));
			}
			return item;
		}
		private MenuItem CreateMenuItem(AppMenuItems menuItems)
		{
			MenuItem item = new MenuItem();
			item.Header = menuItems.ItemName;
			AccessLevels access = accessLevels.Find(x => x.MenuItemId == menuItems.ItemId);
			item.IsEnabled = access?.R ?? false;
			if (menuItems.Dllname != "Null" && menuItems.FunctionName != "Null")
			{
				if (menuItems.FunctionName == "Exit")
				{
					item.Click += (object sender, RoutedEventArgs e) =>
					{
						Authorization authorization = new Authorization();
						authorization.Show();
						this.Close();
					};
				}
				
				else if (menuItems.FunctionName.StartsWith("Window") || menuItems.FunctionName.StartsWith("window"))
				{
					Type type = dllAsms[menuItems.Dllname].GetTypes().First(x => x.Name == menuItems.FunctionName);
					ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
					item.Click += (object sender, RoutedEventArgs e) =>
					{
						Window window = constructorInfo.Invoke(null) as Window;
						window.Show();
					};
				}
				else if (menuItems.FunctionName.StartsWith("Page") || menuItems.FunctionName.StartsWith("page"))
				{
					Type type = dllAsms[menuItems.Dllname].GetTypes().First(x => x.Name == menuItems.FunctionName);
					ConstructorInfo constructorInfo = type.GetConstructor(new[] { typeof(AccessLevels) });
					item.Click += (object sender, RoutedEventArgs e) =>
					{
						object[] objA = new object[1] { access };
						Page page = constructorInfo.Invoke(objA) as Page;
						SP.Children.Remove(MainFrame);
						Frame frame = new Frame();
						Grid.SetRow(frame, 1);
						MainFrame = frame;
						SP.Children.Add(MainFrame);
						MainFrame.NavigationService.Navigate(page);
						MainFrame.Refresh();
					};
				}
				else
				{
					Type type = dllAsms[menuItems.Dllname].GetTypes().First(x => x.Name == menuItems.FunctionName);
					MethodInfo func = type.GetMethod("Func");
					item.Click += (object sender, RoutedEventArgs e) =>
					{
						func.Invoke(null, null);
					};
				}
			}
			return item;
		}

	}
}
