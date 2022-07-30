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
        public async Task<string> ConvertFile(string csvUri, string? separator = ",", string? outputType = "json")
        {
            string result = string.Empty;

            try
            {
                if(outputType.ToLower() == "xml")
                {
                    result = await FileConverterService.ConverterFileToXml(csvUri, separator);
                }
                else
                {
                    result = await FileConverterService.ConverterFileToJson(csvUri, separator);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return result;
        }
    }
}
