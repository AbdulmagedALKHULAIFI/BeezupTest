using BeezupApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BeezupTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConverterController : ControllerBase
    {
        private readonly ILogger<ConverterController> _logger;
        private IFileConverterService FileConverterService;

        public ConverterController(ILogger<ConverterController> logger, IFileConverterService fileConverterService)
        {
            _logger = logger;
            FileConverterService = fileConverterService;
        }

        [HttpGet, Route("{csvUri}")]
        public async Task<string> ReadFileAsync(string csvUri, string? separator = ",")
        {
            csvUri = FileConverterService.ReplaceAsciiCharacter(csvUri);

            string json = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(csvUri);
                    var response = await client.GetAsync("");


                    if (response.IsSuccessStatusCode)
                    {
                        var contentString = await response.Content.ReadAsStringAsync();

                        json = FileConverterService.CsvToJson(contentString, separator);
                        var xml = FileConverterService.ConvertCsvToXml(contentString);
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
    }
}
