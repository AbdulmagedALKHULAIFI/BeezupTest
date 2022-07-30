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
        public async Task<Object> ConvertFile(string csvUri, string? separator = ",", string? outputType = "json")
        {
            //string result = string.Empty;
            Object result = null;
            try
            {
                result = await FileConverterService.ConverterCsv(csvUri, separator, outputType);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return result;
        }
    }
}
