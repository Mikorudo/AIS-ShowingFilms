using DBClasses;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Cinemas
{
	/// <summary>
	/// Логика взаимодействия для Window1.xaml
	/// </summary>
	public partial class CreateNew : Window
	{
		public CreateNew()
		{
			InitializeComponent();
			using (ModelContext db = new ModelContext())
			{
				db.Banks.ToList();
				bankInput.ItemsSource = db.Banks.Local.ToBindingList();
				bankInput.DisplayMemberPath = "BankName";
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string motif = @"^\(?\+[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})?$";
			string name = nameInput.Text;
			if (name.Length < 3)
			{
				MessageBox.Show("Маленькое название");
				return;
			}
			string address = addressInput.Text;
			if (address.Length < 10)
			{
				MessageBox.Show("Короткий адрес");
				return;
			}
			string phoneNumber = phoneInput.Text;
			if (!Regex.IsMatch(phoneNumber, motif))
			{
				MessageBox.Show("Не номер");
				return;
			}
			int seats = 0;
			try
			{
				seats = Convert.ToInt32(seatsInput.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("Количество мест не число");
				return;
			}
			if (seats < 0)
			{
				MessageBox.Show("Число мест < 0");
				return;
			}
			string director = directorInput.Text;
			if (director.Length < 3)
			{
				MessageBox.Show("Имя директора короткое");
				return;
			}
			string owner = ownerInput.Text;
			if (owner.Length < 3)
			{
				MessageBox.Show("Имя владельца короткое");
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

			DBClasses.Cinemas cinema = new DBClasses.Cinemas();
			//cinema.Bank = bank;
			cinema.CinemaName = name;
			cinema.CinemaAdress = address;
			cinema.PhoneNumber = phoneNumber;
			cinema.DirectorName = director;
			cinema.OwnerName = owner;
			cinema.BankId = bank.BankId;
			cinema.BankAccount = account;
			cinema.Inn = inn;
			cinema.SeatsNumber = seats;
			using (ModelContext db = new ModelContext())
			{
				db.Cinemas.Add(cinema);
				db.SaveChanges();
			}
			this.Close();
		}
	}
}
