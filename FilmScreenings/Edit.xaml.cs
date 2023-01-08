using DBClasses;
using System;
using System.Linq;
using System.Windows;

namespace FilmScreenings
{
	/// <summary>
	/// Логика взаимодействия для Window1.xaml
	/// </summary>
	public partial class Edit : Window
	{
		DBClasses.FilmScreenings filmScreening;
		public Edit(DBClasses.FilmScreenings filmScreening)
		{
			this.filmScreening = filmScreening;
			InitializeComponent();
			using (ModelContext db = new ModelContext())
			{
				db.Films.ToList();
				filmInput.ItemsSource = db.Films.Local.ToBindingList();
				filmInput.DisplayMemberPath = "FilmName";

				db.Cinemas.ToList();
				cinemaInput.ItemsSource = db.Cinemas.Local.ToBindingList();
				cinemaInput.DisplayMemberPath = "CinemaName";
			}
			FillData();
		}
		private void FillData()
		{
			int index = -1;
			for (int i = 0; i < filmInput.Items.Count; i++)
			{
				if ((filmInput.Items[i] as Films).FilmId == filmScreening.FilmId)
				{
					index = i;
					break;
				}
			}
			filmInput.SelectedIndex = index;
			index = -1;
			for (int i = 0; i < cinemaInput.Items.Count; i++)
			{
				if ((cinemaInput.Items[i] as Cinemas).CinemaId == filmScreening.CinemaId)
				{
					index = i;
					break;
				}
			}
			cinemaInput.SelectedIndex = index;
			startDateInput.SelectedDate = (System.DateTime)filmScreening.StartScreeningDate;
			endDateInput.SelectedDate = (System.DateTime)filmScreening.EndScreeningDate;
			rentalPaymentInput.Text = filmScreening.RentalPaymentAmount.ToString();
			penaltyInput.Text = filmScreening.LateReturnPenalty.ToString();
			returnedInput.IsChecked = filmScreening.IsReturned;

		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Films film = filmInput.SelectedItem as Films;
			if (film == null)
			{
				MessageBox.Show("Фильм не выбран");
				return;
			}
			Cinemas cinema = cinemaInput.SelectedItem as Cinemas;
			if (cinema == null)
			{
				MessageBox.Show("Кинотеатр не выбран");
				return;
			}
			DateTime? startDate = startDateInput.SelectedDate;
			if (startDate == null)
			{
				MessageBox.Show("Дата начала показа не выбрана");
				return;
			}
			DateTime? endDate = endDateInput.SelectedDate;
			if (endDate == null)
			{
				MessageBox.Show("Дата начала показа не выбрана");
				return;
			}
			if (startDate > endDate)
			{
				MessageBox.Show("Дата начала показа позже даты окончания показа");
				return;
			}
			if (startDate < film.ReleaseDate)
			{
				MessageBox.Show("Дата начала показа раньше даты выхода фильма");
				return;
			}
			decimal penalty = 0;
			try
			{
				penalty = Convert.ToDecimal(penaltyInput.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("Стоимость не число");
				return;
			}
			if (penalty < 0)
			{
				MessageBox.Show("Цена < 0");
				return;
			}
			decimal rentalPayment = 0;
			try
			{
				rentalPayment = Convert.ToDecimal(rentalPaymentInput.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("Стоимость не число");
				return;
			}
			if (rentalPayment < 0)
			{
				MessageBox.Show("Цена < 0");
				return;
			}
			bool? isReturned = returnedInput.IsChecked;

			using (ModelContext db = new ModelContext())
			{
				db.FilmScreenings.Update(filmScreening);
				filmScreening.FilmId = film.FilmId;
				filmScreening.CinemaId = cinema.CinemaId;
				filmScreening.StartScreeningDate = startDate;
				filmScreening.EndScreeningDate = endDate;
				filmScreening.RentalPaymentAmount = rentalPayment;
				filmScreening.LateReturnPenalty = penalty;
				filmScreening.IsReturned = isReturned;
				db.SaveChanges();
			}
			this.Close();
		}
	}
}
