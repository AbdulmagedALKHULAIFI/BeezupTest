using System.Threading.Tasks;

namespace BeezupApi.Services
{
    public interface IFileConverterService
    {

        public Task<string> ConverterFileToJson(string csvUrl, string delimiter);


        public Task<string> ConverterFileToXml(string csvUrl, string delimiter);
    }
}
