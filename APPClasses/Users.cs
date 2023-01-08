using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace APPClasses
{
	public partial class Users
	{
		public Users()
		{
			AccessLevels = new HashSet<AccessLevels>();
		}

		public int? UserId { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		public virtual ICollection<AccessLevels> AccessLevels { get; set; }
	}
}
