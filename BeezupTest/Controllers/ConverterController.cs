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

        [HttpGet]
        public async Task<ActionResult> ConvertFile(string csvUri, string? separator = ",", string? outputType = "json")
        {
            string result = string.Empty;
            object jsonObject = null;
            ContentResult formattedResult = new ContentResult();
            try
            {
                result = await FileConverterService.ConverterCsv(csvUri, separator, outputType);

                if (outputType.ToLower() == "xml")
                {

                    formattedResult = new ContentResult { Content = result, ContentType = "application/xml" , StatusCode = 200 };
                }
                else
                {
                    jsonObject = JsonConvert.DeserializeObject(result);

                    formattedResult = new ContentResult { Content = result, ContentType = "application/json", StatusCode = 200 };
                }
            }
            catch (Exception ex)
            {
                return new ContentResult { Content = "Internal server error", ContentType= "string", StatusCode = 400 };
            }

            return formattedResult;
        }
    }
}
