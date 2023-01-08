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

	public partial class PageBank : Page
	{
		public PageBank(AccessLevels access)
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
				db.Banks.ToList();
				DBgrid.ItemsSource = db.Banks.Local.ToBindingList();
				DBgrid.IsEnabled = true;
			}
		}
		private void DeleteB_Click(object sender, RoutedEventArgs e)
		{
			Banks bank = DBgrid.SelectedItem as Banks;
			if (bank == null)
				MessageBox.Show("Элемент не выбран");
			else
			{
				var result = MessageBox.Show("Удалить элемент " + bank.BankName + '?', "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
				if (result == MessageBoxResult.Yes)
				{
					DeleteBank(bank);
					Search(null, null);
				}
			}
		}
		private void DeleteBank(Banks val)
		{
			using (ModelContext db = new ModelContext())
			{
				db.Banks.Remove(val);
				db.SaveChanges();
			}
		}
		private void UpdateData()
		{
			using (ModelContext db = new ModelContext())
			{
				db.Banks.ToList();
				DBgrid.ItemsSource = db.Banks.Local.ToBindingList();
			}
		}

		private void CreateB_Click(object sender, RoutedEventArgs e)
		{
			CreateNewBank window = new CreateNewBank();
			window.ShowDialog();
			Search(null, null);
		}

		private void EditB_Click(object sender, RoutedEventArgs e)
		{
			Banks bank = DBgrid.SelectedItem as Banks;
			if (bank == null)
				MessageBox.Show("Элемент не выбран");
			else
			{
				EditBank window = new EditBank(bank);
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
					db.Banks.ToList();
					DBgrid.ItemsSource = db.Banks.Local.ToBindingList().Where(x => x.BankName.StartsWith(searchBox.Text));
				}
		}
	}
}
