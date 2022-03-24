using Microsoft.AspNetCore.Mvc;

namespace NSwagPresentationTips.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("meh")]
    public IActionResult Get()
    {
        var weather =  GetWeathers().FirstOrDefault();
        if (weather == null)
        {
            return NotFound();
        }

        return Ok(weather);
    }


    [HttpGet("better")]
    public ActionResult<WeatherForecast> GetMuchBetter()
    {
        var weatherForecast = GetWeathers().FirstOrDefault();

        if (weatherForecast == null)
        {
            return NotFound();
        }

        if (weatherForecast.TemperatureF > 100)
        {
            return StatusCode(StatusCodes.Status418ImATeapot);
        }

        return weatherForecast;
    }


    [HttpGet("best")]
    [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status418ImATeapot)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<WeatherForecast> GetBest()
    {
        var weatherForecast = GetWeathers().FirstOrDefault();

        if (weatherForecast == null)
        {
            return NotFound();
        }

        if (weatherForecast.TemperatureF > 100)
        {
            return StatusCode(StatusCodes.Status418ImATeapot);
        }

        return weatherForecast;
    }

    private static WeatherForecast[] GetWeathers() =>
        Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
}



