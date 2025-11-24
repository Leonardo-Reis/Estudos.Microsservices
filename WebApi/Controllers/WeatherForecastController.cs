using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        [Route("teste")]
        public string Get()
        {
            Console.WriteLine("teste1");
            return "Teste Controller";
        }
    }
}
