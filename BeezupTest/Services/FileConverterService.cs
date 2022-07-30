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

        public async Task<string> ConverterCsv(string csvUrl, string? separator, string outputType)
        {
            object result = null;
            string resultString = string.Empty;

            csvUrl = ReplaceAsciiCharacter(csvUrl);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(csvUrl);
                    var response = await client.GetAsync("");

                    if (response.IsSuccessStatusCode)
                    {
                        var contentString = await response.Content.ReadAsStringAsync();

                        if (outputType.ToLower() == "xml")
                        {
                            resultString = await ConverterFileToXmlString(contentString, separator);
                        }
                        else
                        {
                            resultString = ConvertCsvToJsonString(contentString, separator);
                        }
                    }
                    else
                    {
                        //do your error logging and/or retry logic
                    }

                }
            }catch(Exception ex)
            {
                return ex.Message;
            }


            return resultString;
        }


        public async Task<Object> ConverterFileToJson(string content , string delimiter)
        {

            string json = string.Empty;
            object? jsonObject = null;

            try
            {
                json = ConvertCsvToJsonString(content, delimiter);
                jsonObject = JsonConvert.DeserializeObject<object>(json);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return jsonObject;
        }

        public async Task<string> ConverterFileToXmlString(string contentString, string delimiter)
        {
            StringBuilder sb = new StringBuilder();

            using (var p = ChoCSVReader.LoadText(contentString).WithFirstLineHeader().WithDelimiter(delimiter))
            {
                using (var w = new ChoXmlWriter(sb).Configure(c => c.RootName = "root").Configure(c => c.NodeName = "row"))
                    w.Write(p);
            }

            return sb.ToString();
        }

        public string? ConvertCsvToJsonString(string csv, string delimiter)
        {
            using (TextFieldParser parser = new TextFieldParser(new MemoryStream(Encoding.UTF8.GetBytes(csv))))
            {
                parser.Delimiters = new string[] { delimiter };
                string[]? headers = parser.ReadFields();
                if (headers == null) return null;
                string[]? row;
                string comma = "";
                
                StringBuilder sb = new StringBuilder((int)(csv.Length * 1.1));
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

        public string ReplaceAsciiCharacter(string text)
        {
            text = text.Replace("%2F", "/");
            text = text.Replace("%3A", ":");

            return text;
        }
    }
}
