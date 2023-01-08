using APPClasses;
using DBClasses;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace FilmScreenings
{
	public class BoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((bool?)value == true)
				return "Возвращена";
			else if ((bool?)value == false)
				return "Не возвращена";
			else
				return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
	public class FilmConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((Films)value)?.FilmName;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
	public class CinemaConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((Cinemas)value)?.CinemaName;
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
				db.FilmScreenings.ToList();
				DBgrid.ItemsSource = db.FilmScreenings.Local.ToBindingList();
				foreach (DBClasses.FilmScreenings item in DBgrid.Items)
				{
					item.Film = db.Films.First(x => x.FilmId == item.FilmId);
				}
				foreach (DBClasses.FilmScreenings item in DBgrid.Items)
				{
					item.Cinema = db.Cinemas.First(x => x.CinemaId == item.CinemaId);
				}
				DBgrid.IsEnabled = true;
			}
		}
		private void DeleteB_Click(object sender, RoutedEventArgs e)
		{
			DBClasses.FilmScreenings filmScreening = DBgrid.SelectedItem as DBClasses.FilmScreenings;
			if (filmScreening == null)
				MessageBox.Show("Элемент не выбран");
			else
			{
				var result = MessageBox.Show("Удалить элемент " + filmScreening.Film.FilmName + '?', "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
				if (result == MessageBoxResult.Yes)
				{
					DeleteFilm(filmScreening);
					Search(null, null);
				}
			}
		}
		private void DeleteFilm(DBClasses.FilmScreenings val)
		{
			using (ModelContext db = new ModelContext())
			{
				db.FilmScreenings.Remove(val);
				db.SaveChanges();
			}
		}
		private void UpdateData()
		{
			using (ModelContext db = new ModelContext())
			{
				db.FilmScreenings.ToList();
				DBgrid.ItemsSource = db.FilmScreenings.Local.ToBindingList();
				foreach (DBClasses.FilmScreenings item in DBgrid.Items)
				{
					item.Film = db.Films.First(x => x.FilmId == item.FilmId);
				}
				foreach (DBClasses.FilmScreenings item in DBgrid.Items)
				{
					item.Cinema = db.Cinemas.First(x => x.CinemaId == item.CinemaId);
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
			DBClasses.FilmScreenings filmScreening = DBgrid.SelectedItem as DBClasses.FilmScreenings;
			if (filmScreening == null)
				MessageBox.Show("Элемент не выбран");
			else
			{
				Edit window = new Edit(filmScreening);
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
							db.FilmScreenings.ToList();
							var list = db.FilmScreenings.Local.ToBindingList();
							foreach (DBClasses.FilmScreenings item in list)
							{
								item.Film = db.Films.First(x => x.FilmId == item.FilmId);
							}
							foreach (DBClasses.FilmScreenings item in list)
							{
								item.Cinema = db.Cinemas.First(x => x.CinemaId == item.CinemaId);
							}
							DBgrid.ItemsSource = list.Where(x => x.Film.FilmName.StartsWith(searchBox.Text));
						}
						break;
					case 1:
						using (ModelContext db = new ModelContext())
						{
							db.FilmScreenings.ToList();
							var list = db.FilmScreenings.Local.ToBindingList();
							foreach (DBClasses.FilmScreenings item in list)
							{
								item.Film = db.Films.First(x => x.FilmId == item.FilmId);
							}
							foreach (DBClasses.FilmScreenings item in list)
							{
								item.Cinema = db.Cinemas.First(x => x.CinemaId == item.CinemaId);
							}
							DBgrid.ItemsSource = list.Where(x => x.Cinema.CinemaName.StartsWith(searchBox.Text));
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
