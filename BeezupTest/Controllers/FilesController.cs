using Microsoft.AspNetCore.Mvc;

namespace BeezupTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;

        public FilesController(ILogger<FilesController> logger)
        {
            _logger = logger;
        }

        [HttpGet,Route("Files")]
        public string GetFile()
        {
            return "returned value";
        }
    }
}
