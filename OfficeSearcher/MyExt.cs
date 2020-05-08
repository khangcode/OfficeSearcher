using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSearcher
{
    public static class MyExt
    {
        public static string TruncateAtWord(string value)
        {
            int length = MyConfig.GetShortHtml();
            if (value == null || value.Length < length || value.IndexOf(" ", length) == -1)
                return value;
                        
            string shortText = value.Substring(0, value.IndexOf(" ", length));
            string[] aArr = MyConfig.GetRemoveText();
            foreach (var item in aArr)
            {
                shortText = shortText.Replace(item, " ");
            }
            return shortText;
        }
        public static string ToShortHtml(List<OfficeData> lst)
        {
            if (lst.Count == 0) return "Found nothing";
            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lst.Count; i++)
            {
                OfficeData item = lst[i];
                sb.Append("<b><a id='-1' href='");
                sb.Append(item.FileName);
                sb.Append("'>");
                sb.Append(item.FileName);
                sb.Append("</a></b><br>");
                sb.Append(TruncateAtWord(item.Body));
                sb.Append(" <a id=");
                sb.Append(i.ToString());
                sb.Append(" href='0'>");
                sb.Append("read more");
                sb.Append("</a><br>");
            }
            return sb.ToString();
        }
        public static string ToHtml(OfficeData item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<b>File Name: </b>");
            sb.Append("<a id='0' href='");
            sb.Append(item.FileName);
            sb.Append("'>");
            sb.Append(item.FileName);
            sb.Append("</a><br>");

            sb.Append("<b>Folder: </b>");
            sb.Append(" <a id='1' href='");
            sb.Append(item.FileName);
            sb.Append("'>");
            sb.Append(System.IO.Path.GetDirectoryName(item.FileName));
            sb.Append("</a><br>");

            sb.Append("<b>LastWriteTime: </b>");
            sb.Append(item.DLastWriteTime.ToString("yyyy-MM-dd hh:ss"));
            sb.Append("<br><b>Body: </b>");
            sb.Append(item.Body);

            return sb.ToString();
        }
    }
}
