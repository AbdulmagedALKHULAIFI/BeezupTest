using System.Threading.Tasks;

namespace BeezupApi.Services
{
    public interface IFileConverterService
    {

        string ConverterFile(string filePath);
        Task<string> ConverterFileAsync(string filePath);

        string? CsvToJson(string input, string delimiter);

        string ConvertCsvToXml(string csv);
    }
}
