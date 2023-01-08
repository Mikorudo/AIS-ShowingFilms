using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBClasses
{
	public partial class FilmScreenings
	{
		public int? FilmScreeningId { get; set; }
		public int? FilmId { get; set; }
		public int? CinemaId { get; set; }
		public DateTime? StartScreeningDate { get; set; }
		public DateTime? EndScreeningDate { get; set; }
		public decimal? RentalPaymentAmount { get; set; }
		public decimal? LateReturnPenalty { get; set; }
		public bool? IsReturned { get; set; }

		public virtual Cinemas Cinema { get; set; }
		public virtual Films Film { get; set; }
	}
}
