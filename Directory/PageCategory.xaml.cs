using APPClasses;
using DBClasses;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace Directory
{
	/// <summary>
	/// Логика взаимодействия для Page1.xaml
	/// </summary>

	public partial class PageCategory : Page
	{
		public PageCategory(AccessLevels access)
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
				db.Categories.ToList();
				DBgrid.ItemsSource = db.Categories.Local.ToBindingList();
				DBgrid.IsEnabled = true;
			}
		}
		private void DeleteB_Click(object sender, RoutedEventArgs e)
		{
			Categories category = DBgrid.SelectedItem as Categories;
			if (category == null)
				MessageBox.Show("Элемент не выбран");
			else
			{
				var result = MessageBox.Show("Удалить элемент " + category.CategoryName + '?', "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
				if (result == MessageBoxResult.Yes)
				{
					DeleteCategory(category);
					Search(null, null);
				}
			}
		}
		private void DeleteCategory(Categories val)
		{
			using (ModelContext db = new ModelContext())
			{
				db.Categories.Remove(val);
				db.SaveChanges();
			}
		}
		private void UpdateData()
		{
			using (ModelContext db = new ModelContext())
			{
				db.Categories.ToList();
				DBgrid.ItemsSource = db.Categories.Local.ToBindingList();
				DBgrid.IsEnabled = true;
			}
		}

		private void CreateB_Click(object sender, RoutedEventArgs e)
		{
			CreateNewCategory window = new CreateNewCategory();
			window.ShowDialog();
			Search(null, null);
		}

		private void EditB_Click(object sender, RoutedEventArgs e)
		{
			Categories category = DBgrid.SelectedItem as Categories;
			if (category == null)
				MessageBox.Show("Элемент не выбран");
			else
			{
				EditCategory window = new EditCategory(category);
				window.ShowDialog();
				Search(null, null);
			}
		}
		private void Search(object sender, TextChangedEventArgs e)
		{
			if (searchBox.Text == "")
				UpdateData();
			else
				using (ModelContext db = new ModelContext())
				{
					db.Categories.ToList();
					DBgrid.ItemsSource = db.Categories.Local.ToBindingList().Where(x => x.CategoryName.StartsWith(searchBox.Text));
				}
		}
	}
}
