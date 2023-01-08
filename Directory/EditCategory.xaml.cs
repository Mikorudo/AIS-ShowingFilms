using DBClasses;
using System.Linq;
using System.Windows;

namespace Directory
{
	/// <summary>
	/// Логика взаимодействия для Window1.xaml
	/// </summary>
	public partial class EditCategory : Window
	{
		Categories category;
		public EditCategory(Categories category)
		{
			this.category = category;
			InitializeComponent();
			FillData();
		}
		private void FillData()
		{
			nameInput.Text = category.CategoryName;
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string name = nameInput.Text;
			if (name.Length < 3)
			{
				MessageBox.Show("Маленькое название");
				return;
			}
			using (ModelContext db = new ModelContext())
			{
				if (db.Categories.Where(b => b.CategoryName == name).Any())
				{
					MessageBox.Show("Такая категория уже есть");
					return;
				}
				db.Categories.Update(category);
				category.CategoryName = name;
				db.SaveChanges();
			}
			this.Close();
		}
	}
}
