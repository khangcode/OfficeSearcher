using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Text;

namespace OfficeSearcher
{
    public class Word2Txt
    {
        private Microsoft.Office.Interop.Word.Application app = null;
        private object missing = System.Reflection.Missing.Value;
        private object doNotSaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
        public Word2Txt()
        {
            app = new Microsoft.Office.Interop.Word.Application();
            app.Visible = false;
        }

        public void QuitApp()
        {
            if (app != null)
            {
                app.Quit(ref doNotSaveChanges, ref missing, ref missing);
                Marshal.ReleaseComObject(app);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public string[] GetText(string sFileName)
        {
            string result;
            string err = string.Empty;
            Microsoft.Office.Interop.Word.Document doc = null;
            object fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatUnicodeText;
            object insertLineBreaks = true;
            object allowSubstitutions = true;
            object readOnly = false;
            object visible = false;
            object fileToOpen = sFileName;
            try
            {
                if (app == null)
                {
                    app = new Microsoft.Office.Interop.Word.Application();
                    app.Visible = false;
                }
                doc = app.Documents.Open(
                                         ref fileToOpen,
                                         ref missing,
                                         ref readOnly,
                                         ref missing,
                                         //ref _Password,
                                         ref missing,
                                         ref missing,
                                         ref missing,
                                         ref missing,
                                         ref missing,
                                         ref missing,
                                         ref missing,
                                         ref visible,
                                         ref visible,
                                         ref missing,
                                         ref missing,
                                         ref missing);

               // doc.Activate();
                result = doc.Content.Text;
            }
            catch (System.Exception ex)
            {
                result = sFileName;
                err = sFileName + " - " + ex.Message;
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close(ref doNotSaveChanges, ref missing, ref missing);
                    Marshal.ReleaseComObject(doc);
                    doc = null;
                }
            }
            return new string[] { result, err };
        }
    }
}
