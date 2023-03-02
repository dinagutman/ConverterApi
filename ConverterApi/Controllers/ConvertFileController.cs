using BLConverter.Services;
using DALConverter.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConverterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertFileController : ControllerBase
    {
        IConvertService convertService;
        public ConvertFileController(IConvertService convertService)
        {
            this.convertService = convertService;
        }

        [HttpGet("ConvertToFile/{route}/{format}")]
        [EnableCors("MyAllowOrigins")]

        public async Task<List<DisplayDetails>> Convert(string route, string format)

        {
            try
            {
                return await this.convertService.Convert(route, format);
            }
            catch (Exception Ex)
            {
                Debug.Write("The Error in API Convert functin" + Ex.Message);
                return null;
                
            }

        }
    }
}