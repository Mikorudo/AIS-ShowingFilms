namespace Settings
{
	public static class Settings
	{
		public static string ServicePath { get { return Properties.Settings.Default.ServiceDBPath; } }
		public static string DBPath { get { return Properties.Settings.Default.DBPath; } }
		public static string ServicePassword { get { return Properties.Settings.Default.DBPassword; } }
	}
}
