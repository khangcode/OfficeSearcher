using Lucene.Net.Documents;
using Lucene.Net.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfficeSearcher
{

    public class LuceneIndex
    {

        private Lucene.Net.Store.Directory direc = null;
        private Lucene.Net.Analysis.Standard.StandardAnalyzer analyzer = null;
        //private Lucene.Net.Documents.Document doc = null;
        private Lucene.Net.Index.IndexWriter writer = null;
        public LuceneIndex()
        {
            string indexPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "LuceneData");
            Lucene.Net.Store.Directory direc = Lucene.Net.Store.FSDirectory.Open(indexPath);

            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
           // doc = new Lucene.Net.Documents.Document();
            writer = new Lucene.Net.Index.IndexWriter(direc, analyzer, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);
        }
        public void Save()
        {
            writer.Optimize();
            writer.Commit();
        }
        public void SaveAndDispose()
        {
            //if (doc != null)
            //{
            //    doc = null;
            //}
            if (analyzer != null)
            {
                analyzer.Dispose();
                analyzer = null;
            }
            if (writer != null)
            {
                writer.Optimize();
                writer.Commit();
                writer.Dispose();
                writer = null;
            }
            if (direc != null)
            {
                direc.Dispose();
                direc = null;
            }
        }
        public void DeleteAllDocuments()
        {
            writer.DeleteAll();
        }
        public void DeleteDocumentsFrom(DateTime fromDate)
        {
            string sFrom = DateTools.DateToString(fromDate, DateTools.Resolution.MINUTE);
            Lucene.Net.Search.TermRangeQuery queryFrom = new Lucene.Net.Search.TermRangeQuery("LastWriteTime", sFrom, null, false, false);
            writer.DeleteDocuments(queryFrom);
        }
        public void BuildIndex(string sBody, string sFileName, string sLastWriteTime)
        {
            Lucene.Net.Documents.Document doc = new Document();
            doc.Add(new Field("Body", sBody + " " + sFileName.Replace(@"\", " ").Replace(".", " "), Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("FileName", sFileName + string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("LastWriteTime", sLastWriteTime, Field.Store.YES, Field.Index.NOT_ANALYZED));
            writer.AddDocument(doc);
        }
    }

    public class LuceneSearch
    {
        private Lucene.Net.Store.Directory direc = null;
        //private Lucene.Net.Index.IndexReader reader = null;
        private Lucene.Net.Search.IndexSearcher searcher = null;
        private IndexReader reader = null; 
        private Lucene.Net.Analysis.Standard.StandardAnalyzer analyzer = null;
        public LuceneSearch()
        {
            string indexPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "LuceneData");
            Lucene.Net.Store.Directory direc = Lucene.Net.Store.FSDirectory.Open(indexPath);
            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            //reader = Lucene.Net.Index.IndexReader.Open(direc, true);
            searcher = new Lucene.Net.Search.IndexSearcher(direc, true);
            reader = DirectoryReader.Open(direc, true);

        }
        public void LuceneDispose()
        {
            if (searcher != null)
            {
                searcher.Dispose();
                searcher = null;
            }
            if (reader != null)
            {
                reader.Dispose();
                reader = null;
            }
            if (analyzer != null)
            {
                analyzer.Dispose();
                analyzer = null;
            }

            if (direc != null)
            {
                direc.Dispose();
                direc = null;
            }
        }
        public bool CheckDocExist(OfficeData officeData)
        {
            Lucene.Net.Search.Query query1 = new Lucene.Net.Search.TermQuery(new Lucene.Net.Index.Term("FileName", officeData.FileName));
            Lucene.Net.Search.Query query2 = new Lucene.Net.Search.TermQuery(new Lucene.Net.Index.Term("LastWriteTime", officeData.LastWriteTime));
            Lucene.Net.Search.BooleanQuery query3 = new Lucene.Net.Search.BooleanQuery();
            query3.Add(query1, Lucene.Net.Search.Occur.MUST);
            query3.Add(query2, Lucene.Net.Search.Occur.MUST);
            Lucene.Net.Search.TopDocs topDocs = searcher.Search(query3, 2);
            if (topDocs.TotalHits == 0)
            {
                return false;
            }
            return true;
        }

        public List<OfficeData> Search(Lucene.Net.Search.Query query, int maxResult)
        {
            List<OfficeData> results = new List<OfficeData>();

            Lucene.Net.Search.ScoreDoc[] hitsFound = searcher.Search(query, maxResult).ScoreDocs;
            OfficeData data = null;

            foreach (var hit in hitsFound)
            {
                data = new OfficeData();
                Document doc = searcher.Doc(hit.Doc);
                data.Body = doc.Get("Body");
                data.LastWriteTime = doc.Get("LastWriteTime");
                data.FileName = doc.Get("FileName");
                        
                float score = hit.Score;
                data.Score = score;
                data.ScoreDocDoc = hit.Doc; //vi tri co the xoa document (IndexReader)

                results.Add(data);
            }
            return results;
        }
        public Lucene.Net.Search.Query GetTextQuery(string sValue)
        {
            Lucene.Net.Search.Query query = null;
            try
            {
                Lucene.Net.QueryParsers.QueryParser parser = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Body", analyzer);
                query = parser.Parse(sValue);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Can not parse: " + sValue);
            }
            return query;
        }

        public string GetAllDoc()
        {
            StringBuilder sb = new StringBuilder();
            int totalDoc = reader.NumDocs();
            sb.Append("Total indexed files: ");
            sb.Append(totalDoc.ToString());
            sb.Append("<br><br>");
            Document doc = new Document();
            for (int i = 0; i < reader.MaxDoc; i++)
            {
                doc = reader[i];
                string fileName = doc.Get("FileName");
                sb.Append("<b><a id='-1' href='");
                sb.Append(fileName);
                sb.Append("'>");
                sb.Append(fileName);
                sb.Append("</a></b><br><br>");
            }
            return sb.ToString();
        }
    }

}
