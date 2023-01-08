// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace APPClasses
{
	public partial class AppMenuItems
	{
		public int? ItemId { get; set; }
		public byte ParentItemId { get; set; }
		public string ItemName { get; set; }
		public string Dllname { get; set; }
		public string FunctionName { get; set; }
		public int SequenceNumber { get; set; }
	}
}
