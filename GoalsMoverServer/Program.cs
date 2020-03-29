using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace GoalsMover
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Golaer app started");

            var configuration = ReadConfiguration(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT"));

            InitializeLogger(configuration);

            try
            {
                Log.Information("Starting the host");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
        }

        private static IConfiguration ReadConfiguration(string environment)
        {
            var config = new ConfigurationBuilder();

            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return config.Build();
        }

        private static void InitializeLogger(IConfiguration configuration)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to initialize the logger. Message: {e.Message}. Stack: {e.StackTrace}");
                throw;
            }
        }
    }
}
