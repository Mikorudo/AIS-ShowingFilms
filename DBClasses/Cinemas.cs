using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBClasses
{
	public partial class Cinemas
	{
		public Cinemas()
		{
			FilmScreenings = new HashSet<FilmScreenings>();
		}

		public int? CinemaId { get; set; }
		public string CinemaName { get; set; }
		public string CinemaAdress { get; set; }
		public string PhoneNumber { get; set; }
		public int? SeatsNumber { get; set; }
		public string DirectorName { get; set; }
		public string OwnerName { get; set; }
		public int? BankId { get; set; }
		public string BankAccount { get; set; }
		public string Inn { get; set; }

		public virtual Banks Bank { get; set; }
		public virtual ICollection<FilmScreenings> FilmScreenings { get; set; }
	}
}
