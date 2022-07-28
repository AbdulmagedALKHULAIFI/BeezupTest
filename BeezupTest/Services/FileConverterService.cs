using ChoETL;
using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BeezupApi.Services
{
    public class FileConverterService : IFileConverterService
    {
        public string ConverterFile(string filePath)
        {
            HttpWebRequest request = WebRequest.Create(filePath) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;


            var stream = response.GetResponseStream();
            //using (CsvReader csvReader = new CsvReader()
            //    //response.GetResponseStream(), true))
            //{
            //    int fieldCount = csvReader.FieldCount;
            //    string[] headers = csvReader.GetFieldHeaders();

            //    while (csvReader.ReadNextRecord())
            //    {
            //        //Do work with CSV file data here
            //    }
            //}

            return string.Empty;
        }

        public Task<string> ConverterFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public string? CsvToJson(string input, string delimiter)
        {
            using (TextFieldParser parser = new TextFieldParser(new MemoryStream(Encoding.UTF8.GetBytes(input))))
            {
                parser.Delimiters = new string[] { delimiter };
                string[]? headers = parser.ReadFields();
                if (headers == null) return null;
                string[]? row;
                string comma = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder((int)(input.Length * 1.1));
                sb.Append("[");
                while ((row = parser.ReadFields()) != null)
                {
                    var dict = new Dictionary<string, string>();
                    for (int i = 0; row != null && i < row.Length; i++)
                    {
                        dict[headers[i]] = row[i];
                    }
                    var obj = JsonConvert.SerializeObject(dict);
                    sb.Append(comma + obj);
                    comma = ",";
                }
                return sb.Append("]").ToString();
            }
        }

        public string ConvertCsvToXml(string csv)
        {
            StringBuilder sb = new StringBuilder();

            using (var p = ChoCSVReader.LoadText(csv)
                .WithFirstLineHeader()
                )
            {
                using (var w = new ChoXmlWriter(sb)
                    .Configure(c => c.RootName = "root")
                    .Configure(c => c.NodeName = "row")
                    )
                    w.Write(p);
            }

            return sb.ToString();
        } 
    }
}
