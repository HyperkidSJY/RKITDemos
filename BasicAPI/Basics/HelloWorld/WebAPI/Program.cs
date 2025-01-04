using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace WebAPI
{
    public class Program
    {
        #region Main Method
        /// <summary>
        /// This is a console-based application that hosts a simple web API using ASP.NET Core's minimal API features.
        /// It demonstrates how to define and handle basic GET and POST endpoints, as well as send HTTP requests to the API
        /// from within the same application.
        /// </summary>
        public static async Task Main(string[] args)
        {
            #region Web Application Setup
            // Create a builder for the web application
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();
            #endregion

            #region Define Endpoints
            // Define a simple GET endpoint
            app.MapGet("/api/weather", () =>
            {
                return Results.Ok(new { Weather = "Sunny", Temperature = "25°C" });
            });

            // Define a simple POST endpoint
            app.MapPost("/api/weather", async (HttpRequest request) =>
            {
                var data = await request.ReadFromJsonAsync<WeatherRequest>();
                if (data != null)
                {
                    return Results.Ok(new { Message = $"Received data: {data.City}, {data.Temperature}" });
                }
                return Results.BadRequest();
            });
            #endregion

            #region Run Web API in Background
            // Run the web API in the background
            var apiTask = Task.Run(() => app.RunAsync());

            // Wait for the API to be up before sending requests
            await Task.Delay(1000);
            #endregion

            #region Make HTTP Requests
            // Create HttpClient to make requests
            var client = new HttpClient();

            // Call GET /api/weather and display response in console
            var getResponse = await client.GetFromJsonAsync<object>("http://localhost:5000/api/weather");
            Console.WriteLine("GET /api/weather Response:");
            Console.WriteLine(getResponse);

            // Prepare POST request data
            var postData = new WeatherRequest { City = "New York", Temperature = "22°C" };
            var postResponse = await client.PostAsJsonAsync("http://localhost:5000/api/weather", postData);

            // Display POST response in console
            var postResponseBody = await postResponse.Content.ReadAsStringAsync();
            Console.WriteLine("\nPOST /api/weather Response:");
            Console.WriteLine(postResponseBody);
            #endregion

            #region Exit
            // Wait for the user to press a key to exit
            Console.ReadKey();

            // Cancel the API task
            apiTask.Dispose();
            #endregion
        }
        #endregion

        #region Data Model
        // Data model for the POST request
        public class WeatherRequest
        {
            public string City { get; set; }
            public string Temperature { get; set; }
        }
        #endregion
    }
}
