using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSearcher
{
    public class FileMeta
    {
        public string FileName { get; set; }
        public DateTime DLastWriteTime { get; set; }

        public string LastWriteTime
        {
            get
            {
                return Lucene.Net.Documents.DateTools.DateToString(DLastWriteTime, Lucene.Net.Documents.DateTools.Resolution.MINUTE);
            }
        }
        public FileMeta() { }
    }
}
