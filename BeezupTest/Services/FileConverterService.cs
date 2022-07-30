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
        public async Task<string> ConverterFileToJson(string csvUrl , string delimiter)
        {
            csvUrl = ReplaceAsciiCharacter(csvUrl);

            string json = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(csvUrl);
                    var response = await client.GetAsync("");


                    if (response.IsSuccessStatusCode)
                    {
                        var contentString = await response.Content.ReadAsStringAsync();

                        json = ConvertCsvToJson(contentString, delimiter);
                    }
                    else
                    {
                        //do your error logging and/or retry logic
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return json;
        }

        public async Task<string> ConverterFileToXml(string csvUrl, string delimiter)
        {
            csvUrl = ReplaceAsciiCharacter(csvUrl);

            string xml = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(csvUrl);
                    var response = await client.GetAsync("");


                    if (response.IsSuccessStatusCode)
                    {
                        var contentString = await response.Content.ReadAsStringAsync();

                        xml = ConvertCsvToXml(contentString, delimiter);
                    }
                    else
                    {
                        //do your error logging and/or retry logic
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return xml;
        }

        public string? ConvertCsvToJson(string csv, string delimiter)
        {
            using (TextFieldParser parser = new TextFieldParser(new MemoryStream(Encoding.UTF8.GetBytes(csv))))
            {
                parser.Delimiters = new string[] { delimiter };
                string[]? headers = parser.ReadFields();
                if (headers == null) return null;
                string[]? row;
                string comma = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder((int)(csv.Length * 1.1));
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

        public string ConvertCsvToXml(string csv, string delimiter)
        {
            StringBuilder sb = new StringBuilder();

            using (var p = ChoCSVReader.LoadText(csv).WithFirstLineHeader().WithDelimiter(delimiter))
            {
                using (var w = new ChoXmlWriter(sb).Configure(c => c.RootName = "root").Configure(c => c.NodeName = "row"))
                    w.Write(p);
            }

            return sb.ToString();
        }

        public string ReplaceAsciiCharacter(string text)
        {
            text = text.Replace("%2F", "/");
            text = text.Replace("%3A", ":");

            return text;
        }
    }
}
