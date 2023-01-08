using DBClasses;
using System;
using System.Linq;
using System.Windows;

namespace Distributors
{
	/// <summary>
	/// Логика взаимодействия для Window1.xaml
	/// </summary>
	public partial class Edit : Window
	{
		DBClasses.Distributors distributor;
		public Edit(DBClasses.Distributors distributor)
		{
			this.distributor = distributor;
			InitializeComponent();
			using (ModelContext db = new ModelContext())
			{
				db.Banks.ToList();
				bankInput.ItemsSource = db.Banks.Local.ToBindingList();
				bankInput.DisplayMemberPath = "BankName";
			}
			FillData();
		}
		private void FillData()
		{
			nameInput.Text = distributor.DistributorName;
			addressInput.Text = distributor.LegalAddress;
			int index = -1;
			for (int i = 0; i < bankInput.Items.Count; i++)
			{
				if ((bankInput.Items[i] as Banks).BankId == distributor.BankId)
				{
					index = i;
					break;
				}
			}
			bankInput.SelectedIndex = index;
			accountInput.Text = distributor.BankAccount;
			innInput.Text = distributor.Inn;
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string name = nameInput.Text;
			if (name.Length < 3)
			{
				MessageBox.Show("Короткое имя");
				return;
			}
			string address = addressInput.Text;
			if (address.Length < 10)
			{
				MessageBox.Show("Короткий адрес");
				return;
			}
			Banks bank = bankInput.SelectedItem as Banks;
			if (bank == null)
			{
				MessageBox.Show("Банк не выбран");
				return;
			}
			string account = accountInput.Text;
			if (account.Length != 20 || !account.All(Char.IsDigit))
			{
				MessageBox.Show("Счёт в банке содержит 20 цифр");
				return;
			}
			string inn = innInput.Text;
			if ((inn.Length != 10 && inn.Length != 12) || !inn.All(Char.IsDigit))
			{
				MessageBox.Show("ИНН содержит 10/12 цифр");
				return;
			}
			using (ModelContext db = new ModelContext())
			{
				db.Distributors.Update(distributor);
				distributor.DistributorName = name;
				distributor.LegalAddress = address;
				distributor.BankId = (int)bank.BankId;
				distributor.BankAccount = account;
				distributor.Inn = inn;
				db.SaveChanges();
			}
			this.Close();
		}
	}
}
