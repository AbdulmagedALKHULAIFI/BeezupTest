using System.Threading.Tasks;

namespace BeezupApi.Services
{
    public interface IFileConverterService
    {

        public Task<Object> ConverterFileToJson(string csvUrl, string delimiter);


        public Task<string> ConverterFileToXmlString(string csvUrl, string delimiter);
        Task<string> ConverterCsv(string csvUri, string? separator, string outputType);
    }
}
