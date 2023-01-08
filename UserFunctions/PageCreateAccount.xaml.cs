using APPClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UserFunctions
{
	/// <summary>
	/// Логика взаимодействия для PageCreateAccount.xaml
	/// </summary>
	public class MenuConverter : IValueConverter
	{
		List<AppMenuItems> items;
		public MenuConverter() : base()
		{
			using (AccessContext db = new AccessContext())
			{
				items = db.AppMenuItems.ToList();
			}
		}
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return items.First(x => x.ItemId == (int)value)?.ItemName;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
	public partial class PageCreateAccount : Page
	{
		List<AccessLevels> accesses;
		public PageCreateAccount(AccessLevels accessLevels)
		{
			InitializeComponent();
			accesses = new List<AccessLevels>();
			using (AccessContext db = new AccessContext())
			{
				foreach (AppMenuItems item in db.AppMenuItems.ToList())
				{
					AccessLevels access = new AccessLevels();
					access.MenuItemId = (int)item.ItemId;
					access.R = true;
					access.W = true;
					access.E = true;
					access.D = true;
					accesses.Add(access);
				}
			}
			accessGrid.ItemsSource = accesses;
		}

		private void Create(object sender, RoutedEventArgs e)
		{
			using (AccessContext db = new AccessContext())
			{
				if (db.Users.Count(x => x.Login == loginBox.Text) > 0)
				{
					MessageBox.Show("Такой аккаунт уже существует");
					return;
				}
				if (loginBox.Text.Length < 3)
				{
					MessageBox.Show("Логин слишком короткий");
					return;
				}
				if (passBox.Text.Length < 3)
				{
					MessageBox.Show("Пароль слишком короткий");
					return;
				}
				Users user = new Users();
				user.Login = loginBox.Text;
				user.Password = PasswordEncrypt.Encrypt(passBox.Text);
				db.Users.Add(user);
				db.SaveChanges();
				db.Users.Update(user);
				foreach (AccessLevels item in accesses)
				{
					item.UserId = (int)user.UserId;
					db.AccessLevels.Add(item);
				}
				db.SaveChanges();
				MessageBox.Show("Аккаунт успешно создан");
			}
		}
	}
}
