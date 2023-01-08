using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBClasses
{
	public partial class Films
	{
		public Films()
		{
			FilmScreenings = new HashSet<FilmScreenings>();
		}

		public int? FilmId { get; set; }
		public string FilmName { get; set; }
		public int? CategoryId { get; set; }
		public string ScriptwriterName { get; set; }
		public string ProducerName { get; set; }
		public string ProductionСompaniesName { get; set; }
		public DateTime? ReleaseDate { get; set; }
		public int? DistributorId { get; set; }
		public decimal? FilmCost { get; set; }

		public virtual Categories Category { get; set; }
		public virtual Distributors Distributor { get; set; }
		public virtual ICollection<FilmScreenings> FilmScreenings { get; set; }
	}
}
