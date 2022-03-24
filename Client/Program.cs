// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var client = new Client.WeatherClient("http://localhost:5003/", new HttpClient());

var weather = await client.GetWeatherForecastAsync();
Console.ReadKey();