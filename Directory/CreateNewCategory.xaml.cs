using DBClasses;
using System.Linq;
using System.Windows;

namespace Directory
{
	/// <summary>
	/// Логика взаимодействия для Window1.xaml
	/// </summary>
	public partial class CreateNewCategory : Window
	{
		public CreateNewCategory()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string name = nameInput.Text;
			if (name.Length < 3)
			{
				MessageBox.Show("Маленькое название");
				return;
			}
			Categories category = new Categories();
			category.CategoryName = name;
			using (ModelContext db = new ModelContext())
			{
				if (db.Categories.Where(b => b.CategoryName == name).Any())
				{
					MessageBox.Show("Такая категория уже есть");
					return;
				}
				db.Categories.Add(category);
				db.SaveChanges();
			}
			this.Close();
		}
	}
}
