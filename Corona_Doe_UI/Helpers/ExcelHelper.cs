using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Corona_Doe_UI.Helpers
{
    public class ExcelHelper
    {
        public static async Task<DataTable> ReadDataAsync(Stream stream)
        {
            using var package = new ExcelPackage();
            await package.LoadAsync(stream);
            var ws = package.Workbook.Worksheets[0];

            DataTable dataTable = new DataTable();
            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
                //Get colummn details
                if (!string.IsNullOrEmpty(firstRowCell.Text))
                {
                    dataTable.Columns.Add(firstRowCell.Text);
                }
            }
            var startRow = 2;
            //Get row details
            for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, dataTable.Columns.Count];
                DataRow row = dataTable.Rows.Add();
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                }
            }

            return dataTable;
        }
    }
}
