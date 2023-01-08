using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBClasses
{
	public partial class Banks
	{
		public Banks()
		{
			Cinemas = new HashSet<Cinemas>();
			Distributors = new HashSet<Distributors>();
		}

		public int? BankId { get; set; }
		public string BankName { get; set; }

		public virtual ICollection<Cinemas> Cinemas { get; set; }
		public virtual ICollection<Distributors> Distributors { get; set; }
	}
}
