using DBClasses;
using System.Linq;
using System.Windows;

namespace Directory
{
	/// <summary>
	/// Логика взаимодействия для Window1.xaml
	/// </summary>
	public partial class CreateNewBank : Window
	{
		public CreateNewBank()
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
			Banks bank = new Banks();
			bank.BankName = name;
			bank.BankId = null;
			using (ModelContext db = new ModelContext())
			{
				if (db.Banks.Where(b => b.BankName == name).Any())
				{
					MessageBox.Show("Такой банк уже есть");
					return;
				}
				db.Banks.Add(bank);
				db.SaveChanges();
			}
			this.Close();
		}
	}
}
