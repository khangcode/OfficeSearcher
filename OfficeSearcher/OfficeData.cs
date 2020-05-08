using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSearcher
{
    public class OfficeData
    {
        public OfficeData() { }
        public string Body { get; set; }

        //Thuoc tinh cho file
        public string FileName { get; set; }
        public DateTime DLastWriteTime { get; set; }
        public string LastWriteTime
        {
            get
            {
                return Lucene.Net.Documents.DateTools.DateToString(DLastWriteTime, Lucene.Net.Documents.DateTools.Resolution.MINUTE);
            }
            set
            {
                DLastWriteTime = Lucene.Net.Documents.DateTools.StringToDate(value);
            }
        }
        public float Score { get; set; }
        public int ScoreDocDoc { get; set; }
    }
}
