using APPClasses;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UserFunctions
{
	/// <summary>
	/// Логика взаимодействия для PagePassChange.xaml
	/// </summary>
	public partial class PagePassChange : Page
	{
		public PagePassChange(AccessLevels access)
		{
			InitializeComponent();
			if (access.E != true)
				ChangePassB.IsEnabled = false;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (newPassBox.Password != newPassRepeatBox.Password)
			{
				MessageBox.Show("Повтор пароля не совпадает");
				return;
			}
			AccessContext db = new AccessContext(); //Получаем доступ к бд приложения

			var users = db.Users.Where(x => x.Login == loginBox.Text).ToList();
			if (users.Count > 1)
			{
				MessageBox.Show("Найдено больше 1 записи такого аккунта, обратитесь в администратору БД");
				db.Dispose();
				return;
			}
			users = users.Where(x => x.Password == PasswordEncrypt.Encrypt(passBox.Password)).ToList();
			if (users.Count == 0)
			{
				MessageBox.Show("Пользователь не найден");
				db.Dispose();
				return;
			}
			users.First().Password = PasswordEncrypt.Encrypt(newPassBox.Password);
			db.SaveChanges();
			MessageBox.Show("Пароль успешно сменён");
			db.Dispose();
			return;
		}
	}
}
