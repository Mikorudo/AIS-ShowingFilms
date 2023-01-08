using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help
{
	public static class AboutProgram
	{
		public static void Func()
		{
			AboutBox aboutBox = new AboutBox();
			aboutBox.ShowDialog();
		}
	}
}
