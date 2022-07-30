using BeezupApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
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
        public async Task<ActionResult> ConvertFile(string csvUri, string? separator = ",", string? outputType = "json")
        {
            string result = string.Empty;
            object jsonObject = null;
            try
            {
                result = await FileConverterService.ConverterCsv(csvUri, separator, outputType);
                jsonObject = JsonConvert.DeserializeObject(result);
            }
            catch (Exception ex)
            {
               
            }

            return new ContentResult { Content = result, ContentType = "application/json" }; 
        }
    }
}
