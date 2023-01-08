using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBClasses
{
	public partial class Distributors
	{
		public Distributors()
		{
			Films = new HashSet<Films>();
		}

		public int? DistributorId { get; set; }
		public string DistributorName { get; set; }
		public string LegalAddress { get; set; }
		public int BankId { get; set; }
		public string BankAccount { get; set; }
		public string Inn { get; set; }

		public virtual Banks Bank { get; set; }
		public virtual ICollection<Films> Films { get; set; }
	}
}
