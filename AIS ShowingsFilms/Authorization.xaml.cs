using APPClasses;
using DBClasses;
using Settings;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AIS_ShowingFilms
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class Authorization : Window
	{
		private AISwindow window;
		public Authorization()
		{
			InitializeComponent();
			loginBox.Focus();
		}

		private void signinButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				AccessContext test1 = new AccessContext(); //Пробуем подключиться к сервисной бд
				test1.AppMenuItems.ToList();
				test1.AccessLevels.ToList();
				test1.Users.ToList();
				test1.Dispose();

				ModelContext test2 = new ModelContext(); //Пробуем подключиться к основной бд
				test2.Cinemas.ToList();
				test2.Banks.ToList();
				test2.Films.ToList();
				test2.Distributors.ToList();
				test2.Categories.ToList();
				test2.FilmScreenings.ToList();
				test2.Dispose();

			}
			catch (System.Exception exception)
			{
				var result = MessageBox.Show("Не удалось подключиться к базам данным. Рекомендуем проверить указанные данные в настройках. Показать информацию о ошибке?", "Ошибка!", MessageBoxButton.YesNoCancel, MessageBoxImage.Error);
				if (result == MessageBoxResult.Yes)
					MessageBox.Show(exception.Message);
				return;
			}

			AccessContext db = new AccessContext(); //Получаем доступ к сервисной бд

			if (loginBox.Text.Length == 0) // проверяем введён ли логин     
			{
				MessageBox.Show("Введите логин");
				db.Dispose();
				return;
			}
			if (passwordBox.Password.Length == 0) // проверяем введён ли пароль         
			{
				MessageBox.Show("Введите пароль");
				db.Dispose();
				return;
			}
			string login = loginBox.Text;
			string password = PasswordEncrypt.Encrypt(passwordBox.Password);
			var users = db.Users.Where(x => x.Login == login).ToList(); //Находим подхоящий аккаунт
			if (users.Count > 1)
			{
				MessageBox.Show("Найдено больше 1 записи такого аккунта, обратитесь в администратору БД");
				db.Dispose();
				return;
			}
			users = users.Where(x => x.Password == password).ToList();
			if (users.Count == 0)
			{
				MessageBox.Show("Пользователь не найден");
				db.Dispose();
				return;
			}
			MessageBox.Show("Пользователь авторизовался");
			Users user = users.First();
			db.Dispose();
			OpenAISWindow(user);
		}
		private void OpenAISWindow(Users user)
		{
			window = new AISwindow(user);
			window.Show();
			this.Close();
		}

		private void enter_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				signinButton_Click(null, null);
		}

		private void OpenSettings(object sender, RoutedEventArgs e)
		{
			WindowSettings settings = new WindowSettings();
			settings.ShowDialog();
		}
	}
}
