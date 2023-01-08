// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace APPClasses
{
	public partial class AccessLevels
	{
		public int? AccessId { get; set; }
		public int UserId { get; set; }
		public int MenuItemId { get; set; }
		public bool? R { get; set; }
		public bool? W { get; set; }
		public bool? E { get; set; }
		public bool? D { get; set; }

		public virtual Users User { get; set; }
	}
}
