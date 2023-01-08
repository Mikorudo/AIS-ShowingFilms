using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBClasses
{
	public partial class Categories
	{
		public Categories()
		{
			Films = new HashSet<Films>();
		}

		public int? CategoryId { get; set; }
		public string CategoryName { get; set; }

		public virtual ICollection<Films> Films { get; set; }
	}
}
