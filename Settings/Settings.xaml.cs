using Microsoft.Win32;
using System.Windows;

namespace Settings
{
	/// <summary>
	/// Логика взаимодействия для Settings.xaml
	/// </summary>
	public partial class WindowSettings : Window
	{
		public WindowSettings()
		{
			InitializeComponent();
			passwordBox.Password = Properties.Settings.Default.DBPassword;
		}
		private void ChangeServiceBD(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "База данных access (*.accdb)|*.accdb";
			if (openFileDialog.ShowDialog() == true)
			{
				Properties.Settings.Default.ServiceDBPath = openFileDialog.FileName;
				servicePathBox.Text = openFileDialog.FileName;
				Properties.Settings.Default.Save();
			}
		}
		private void ChangeBD(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "База данных access (*.accdb)|*.accdb";
			if (openFileDialog.ShowDialog() == true)
			{
				Properties.Settings.Default.DBPath = openFileDialog.FileName;
				dbPathBox.Text = openFileDialog.FileName;
				Properties.Settings.Default.Save();
			}
		}

		private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			Properties.Settings.Default.DBPassword = passwordBox.Password;
			Properties.Settings.Default.Save();
		}
	}
}
