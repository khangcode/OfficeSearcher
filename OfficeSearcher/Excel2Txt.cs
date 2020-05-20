using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace OfficeSearcher
{
    public class Excel2Txt
    {
        private Excel.Application excelApp = null;
        public Excel.Workbook wb = null;
        public Excel.Range range = null;
        public Excel.Worksheet xlWorkSheet = null;
        public Excel2Txt() 
        {
            excelApp = new Excel.Application();
            excelApp.Visible = false;
        }       
        public string[] GetText(string sExcelFile)
        {
            StringBuilder sb = new StringBuilder();
            string err = string.Empty;
            try
            {
                if (excelApp == null)
                {
                    excelApp = new Excel.Application();
                    excelApp.Visible = false;
                }

                wb = excelApp.Workbooks.Open(sExcelFile, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                for (int j = 1; j <= wb.Sheets.Count; j++) //Index bat dau bang 1, thay vi 0
                {
                    xlWorkSheet = (Excel.Worksheet)wb.Sheets[j];
                    range = xlWorkSheet.UsedRange;

                    sb.Append(xlWorkSheet.Name);
                    sb.Append(Environment.NewLine);
                    foreach (Excel.Range row in range.Rows)
                    {
                        for (int i = 0; i < row.Columns.Count; i++)
                        {
                            string item = Convert.ToString(row.Cells[1, i + 1].Value2);
                            sb.Append(item);
                            if (string.IsNullOrEmpty(item))
                            {
                                sb.Append(" ");
                            }
                            sb.Append(" ");
                        }
                        sb.Append(Environment.NewLine);
                    }
                    sb.Append("End Sheet");
                    sb.Append(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                sb.Append(sExcelFile);
                err = sExcelFile + " - " + ex.Message;
            }
            finally
            {
                if (range != null)
                {
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(range);
                    range = null;
                }

                if (xlWorkSheet != null)
                {
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkSheet);
                    xlWorkSheet = null;
                }

                if (wb != null)
                {
                    wb.Close(false, Type.Missing, Type.Missing); //SaveChanges = false
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wb);
                    wb = null;
                }
            }
            return new string[] { sb.ToString(), err};
        }
        public void QuitApp()
        {
            if (excelApp != null)
            {
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}

