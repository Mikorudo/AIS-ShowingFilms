using APPClasses;
using DBClasses;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
namespace Cinemas
{
	/// <summary>
	/// Логика взаимодействия для Page1.xaml
	/// </summary>
	public class BankConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((Banks)value)?.BankName;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
	public partial class PageDB : Page
	{
		public PageDB(AccessLevels access)
		{
			InitializeComponent();
			EnableFunction(access);
		}
		private void EnableFunction(AccessLevels access)
		{
			if (access.R == true)
			{
				InitializeDBgrid();

				if (access.W == true)
					CreateB.IsEnabled = true;
				else
					CreateB.IsEnabled = false;

				if (access.E == true)
					EditB.IsEnabled = true;
				else
					EditB.IsEnabled = false;

				if (access.D == true)
					DeleteB.IsEnabled = true;
				else
					DeleteB.IsEnabled = false;
			}
			else
				return;
		}
		private void InitializeDBgrid()
		{
			DBgrid.SelectionMode = DataGridSelectionMode.Single;
			using (ModelContext db = new ModelContext())
			{
				db.Cinemas.ToList();
				DBgrid.ItemsSource = db.Cinemas.Local.ToBindingList();
				foreach (DBClasses.Cinemas item in DBgrid.Items)
				{
					item.Bank = db.Banks.First(x => x.BankId == item.BankId);
				}
				DBgrid.IsEnabled = true;
			}
		}
		private void DeleteB_Click(object sender, RoutedEventArgs e)
		{
			DBClasses.Cinemas cinema = DBgrid.SelectedItem as DBClasses.Cinemas;
			if (cinema == null)
				MessageBox.Show("Элемент не выбран");
			else
			{
				var result = MessageBox.Show("Удалить элемент " + cinema.CinemaName + '?', "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
				if (result == MessageBoxResult.Yes)
				{
					DeleteCinema(cinema);
					Search(null, null);
				}
			}
		}
		private void DeleteCinema(DBClasses.Cinemas val)
		{
			using (ModelContext db = new ModelContext())
			{
				db.Cinemas.Remove(val);
				db.SaveChanges();
			}
		}
		private void UpdateData()
		{
			using (ModelContext db = new ModelContext())
			{
				db.Cinemas.ToList();
				DBgrid.ItemsSource = db.Cinemas.Local.ToBindingList();
				foreach (DBClasses.Cinemas item in DBgrid.Items)
				{
					item.Bank = db.Banks.First(x => x.BankId == item.BankId);
				}
			}
		}

		private void CreateB_Click(object sender, RoutedEventArgs e)
		{
			CreateNew window = new CreateNew();
			window.ShowDialog();
			Search(null, null);
		}

		private void EditB_Click(object sender, RoutedEventArgs e)
		{
			DBClasses.Cinemas cinema = DBgrid.SelectedItem as DBClasses.Cinemas;
			if (cinema == null)
				MessageBox.Show("Элемент не выбран");
			else
			{
				Edit window = new Edit(cinema);
				window.ShowDialog();
				Search(null, null);
			}
		}

		private void Search(object sender, TextChangedEventArgs e)
		{
			if (searchBox.Text == "")
				UpdateData();
			else
				switch (searchMode.SelectedIndex)
				{
					case 0:
						using (ModelContext db = new ModelContext())
						{
							db.Cinemas.ToList();
							DBgrid.ItemsSource = db.Cinemas.Local.ToBindingList().Where(x => x.CinemaName.StartsWith(searchBox.Text));
							foreach (DBClasses.Cinemas item in DBgrid.Items)
							{
								item.Bank = db.Banks.First(x => x.BankId == item.BankId);
							}
						}
						break;
					case 1:
						using (ModelContext db = new ModelContext())
						{
							db.Cinemas.ToList();
							DBgrid.ItemsSource = db.Cinemas.Local.ToBindingList().Where(x => x.CinemaAdress.StartsWith(searchBox.Text));
							foreach (DBClasses.Cinemas item in DBgrid.Items)
							{
								item.Bank = db.Banks.First(x => x.BankId == item.BankId);
							}
						}
						break;
					default:
						break;
				}
		}

		private void searchMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (searchBox != null)
				Search(null, null);
		}
	}
}
