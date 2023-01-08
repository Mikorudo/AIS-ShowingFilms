using DBClasses;
using System;
using System.Linq;
using System.Windows;

namespace Films
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
				db.Categories.ToList();
				categoryInput.ItemsSource = db.Categories.Local.ToBindingList();
				categoryInput.DisplayMemberPath = "CategoryName";

				db.Distributors.ToList();
				distributorInput.ItemsSource = db.Distributors.Local.ToBindingList();
				distributorInput.DisplayMemberPath = "DistributorName";
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string name = nameInput.Text;
			if (name.Length < 3)
			{
				MessageBox.Show("Короткое имя");
				return;
			}
			Categories category = categoryInput.SelectedItem as Categories;
			if (category == null)
			{
				MessageBox.Show("Категория не выбрана");
				return;
			}
			string scriptwriter = scriptwriterInput.Text;
			if (name.Length < 3)
			{
				MessageBox.Show("Короткое имя сценариста");
				return;
			}
			string producer = producerInput.Text;
			if (name.Length < 3)
			{
				MessageBox.Show("Короткое имя продюсера");
				return;
			}
			string company = companyInput.Text;
			if (name.Length < 3)
			{
				MessageBox.Show("Короткое имя компании");
				return;
			}
			DateTime? release = releaseInput.SelectedDate;
			if (release == null)
			{
				MessageBox.Show("Дата не выбрана");
				return;
			}
			Distributors distributor = distributorInput.SelectedItem as Distributors;
			if (distributor == null)
			{
				MessageBox.Show("Поставщик не выбран");
				return;
			}
			decimal cost = 0;
			try
			{
				cost = Convert.ToDecimal(costInput.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("Стоимость не число");
				return;
			}
			if (cost < 0)
			{
				MessageBox.Show("Цена < 0");
				return;
			}
			DBClasses.Films film = new DBClasses.Films();
			film.FilmName = name;
			film.CategoryId = category.CategoryId;
			film.ScriptwriterName = scriptwriter;
			film.ProducerName = producer;
			film.ProductionСompaniesName = company;
			film.ReleaseDate = release;
			film.DistributorId = distributor.DistributorId;
			film.FilmCost = cost;


			using (ModelContext db = new ModelContext())
			{
				db.Films.Add(film);
				db.SaveChanges();
			}
			this.Close();
		}
	}
}
