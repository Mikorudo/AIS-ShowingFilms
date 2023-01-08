using DBClasses;
using System.Linq;
using System.Windows;

namespace Directory
{
	/// <summary>
	/// Логика взаимодействия для Window1.xaml
	/// </summary>
	public partial class EditBank : Window
	{
		Banks bank;
		public EditBank(Banks bank)
		{
			this.bank = bank;
			InitializeComponent();
			FillData();
		}
		private void FillData()
		{
			nameInput.Text = bank.BankName;
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
				if (db.Banks.Where(b => b.BankName == name).Any())
				{
					MessageBox.Show("Такой банк уже есть");
					return;
				}
				db.Banks.Update(bank);
				bank.BankName = name;
				db.SaveChanges();
			}
			this.Close();
		}
	}
}
