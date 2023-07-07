using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;

namespace NhonOJT_elk.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        try
        {
            Log.Information("Đang lấy thông tin thời tiết");

            Log.Information("Getting weather forecast");

            // Rest of your code...

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while getting the weather forecast");
            Log.Error(ex, "Gặp lỗi khi lầy thông tin thời tiết");

            throw;
        }
    }

}
