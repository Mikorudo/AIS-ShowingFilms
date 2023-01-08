using DBClasses;
using Microsoft.Win32;
using System;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace Documents
{
	public static class SaveExcel
	{
		public static void Func()
		{
			SaveFileDialog saveFile = new SaveFileDialog();
			saveFile.Filter = "Excel файл(*.xlsx)|*.xlsx";
			if (saveFile.ShowDialog() != true)
				return;
			Excel.Application app = new Excel.Application
			{
				Visible = false,
				SheetsInNewWorkbook = 1
			};
			Excel.Workbook workBook = app.Workbooks.Add(Type.Missing);
			app.DisplayAlerts = false;
			Excel.Worksheet sheet = (Excel.Worksheet)app.Worksheets.get_Item(1);
			sheet.Name = "Демонстрация фильмов";
			sheet.Range["A1"].Value = "Название фильма";
			sheet.Range["B1"].Value = "Кинотеатр";
			sheet.Range["C1"].Value = "Дата начала показа";
			sheet.Range["D1"].Value = "Дата окончания показа";
			sheet.Range["E1"].Value = "Сумма оплаты аренды за ленту";
			sheet.Range["F1"].Value = "Пени за несвоевременный возврат";
			sheet.Range["G1"].Value = "Возврат ленты";
			Excel.Range range = sheet.Columns["A:G"];
			range.EntireColumn.AutoFit(); //авторазмер
			int row = 2;
			using (ModelContext db = new ModelContext())
			{
				foreach (FilmScreenings item in db.FilmScreenings)
				{
					sheet.Cells[row, 1] = db.Films.First(x => x.FilmId == item.FilmId).FilmName;
					sheet.Cells[row, 2] = db.Cinemas.First(x => x.CinemaId == item.CinemaId).CinemaName;
					sheet.Cells[row, 3] = item.StartScreeningDate;
					sheet.Cells[row, 4] = item.EndScreeningDate;
					sheet.Cells[row, 5] = item.RentalPaymentAmount;
					sheet.Cells[row, 6] = item.LateReturnPenalty;
					if (item.IsReturned != null)
						sheet.Cells[row, 7] = (bool)item.IsReturned ? "Возвращена" : "Не возвращена";
					row++;
				}
			}
			app.Application.ActiveWorkbook.SaveAs(saveFile.FileName, Type.Missing,
			Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
			Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
			app.Quit();
			System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
		}
	}
}
