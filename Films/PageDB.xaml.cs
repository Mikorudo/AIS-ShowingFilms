using APPClasses;
using DBClasses;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace Films
{
	/// <summary>
	/// Логика взаимодействия для Page1.xaml
	/// </summary>
	public class CategoryConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((Categories)value)?.CategoryName;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
	public class DistributorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((Distributors)value)?.DistributorName;
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
				db.Films.ToList();
				DBgrid.ItemsSource = db.Films.Local.ToBindingList();
				foreach (DBClasses.Films item in DBgrid.Items)
				{
					item.Category = db.Categories.First(x => x.CategoryId == item.CategoryId);
				}
				foreach (DBClasses.Films item in DBgrid.Items)
				{
					item.Distributor = db.Distributors.First(x => x.DistributorId == item.DistributorId);
				}
				DBgrid.IsEnabled = true;
			}
		}
		private void DeleteB_Click(object sender, RoutedEventArgs e)
		{
			DBClasses.Films film = DBgrid.SelectedItem as DBClasses.Films;
			if (film == null)
				MessageBox.Show("Элемент не выбран");
			else
			{
				var result = MessageBox.Show("Удалить элемент " + film.FilmName + '?', "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
				if (result == MessageBoxResult.Yes)
				{
					DeleteFilm(film);
					Search(null, null);
				}
			}
		}
		private void DeleteFilm(DBClasses.Films val)
		{
			using (ModelContext db = new ModelContext())
			{
				db.Films.Remove(val);
				db.SaveChanges();
			}
		}
		private void UpdateData()
		{
			using (ModelContext db = new ModelContext())
			{
				db.Films.ToList();
				DBgrid.ItemsSource = db.Films.Local.ToBindingList();
				foreach (DBClasses.Films item in DBgrid.Items)
				{
					item.Category = db.Categories.First(x => x.CategoryId == item.CategoryId);
				}
				foreach (DBClasses.Films item in DBgrid.Items)
				{
					item.Distributor = db.Distributors.First(x => x.DistributorId == item.DistributorId);
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
			DBClasses.Films film = DBgrid.SelectedItem as DBClasses.Films;
			if (film == null)
				MessageBox.Show("Элемент не выбран");
			else
			{
				Edit window = new Edit(film);
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
							db.Films.ToList();
							DBgrid.ItemsSource = db.Films.Local.ToBindingList().Where(x => x.FilmName.StartsWith(searchBox.Text));
							foreach (DBClasses.Films item in DBgrid.Items)
							{
								item.Category = db.Categories.First(x => x.CategoryId == item.CategoryId);
							}
							foreach (DBClasses.Films item in DBgrid.Items)
							{
								item.Distributor = db.Distributors.First(x => x.DistributorId == item.DistributorId);
							}
						}
						break;
					case 1:
						using (ModelContext db = new ModelContext())
						{
							db.Films.ToList();
							DBgrid.ItemsSource = db.Films.Local.ToBindingList().Where(x => x.ScriptwriterName.StartsWith(searchBox.Text));
							foreach (DBClasses.Films item in DBgrid.Items)
							{
								item.Category = db.Categories.First(x => x.CategoryId == item.CategoryId);
							}
							foreach (DBClasses.Films item in DBgrid.Items)
							{
								item.Distributor = db.Distributors.First(x => x.DistributorId == item.DistributorId);
							}
						}
						break;
					case 2:
						using (ModelContext db = new ModelContext())
						{
							db.Films.ToList();
							DBgrid.ItemsSource = db.Films.Local.ToBindingList().Where(x => x.ProducerName.StartsWith(searchBox.Text));
							foreach (DBClasses.Films item in DBgrid.Items)
							{
								item.Category = db.Categories.First(x => x.CategoryId == item.CategoryId);
							}
							foreach (DBClasses.Films item in DBgrid.Items)
							{
								item.Distributor = db.Distributors.First(x => x.DistributorId == item.DistributorId);
							}
						}
						break;
					case 3:
						using (ModelContext db = new ModelContext())
						{
							db.Films.ToList();
							DBgrid.ItemsSource = db.Films.Local.ToBindingList().Where(x => x.ProductionСompaniesName.StartsWith(searchBox.Text));
							foreach (DBClasses.Films item in DBgrid.Items)
							{
								item.Category = db.Categories.First(x => x.CategoryId == item.CategoryId);
							}
							foreach (DBClasses.Films item in DBgrid.Items)
							{
								item.Distributor = db.Distributors.First(x => x.DistributorId == item.DistributorId);
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
