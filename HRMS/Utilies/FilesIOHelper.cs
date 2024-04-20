using System;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace HRMS.Utilies
{
	public static class FilesIOHelper
	{
        public static byte[] ExporttoExcel<T>(IList<T> table, string filename)
        {
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1);
            return pack.GetAsByteArray();
        }
    }
}

