using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Api
{
  public class Program
  {
    private static IConfiguration Configuration
    {
      get
      {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile($"appsettings.{env}.json", false, true);

        builder.AddEnvironmentVariables();
        return builder.Build();
      }
    }
    
    public static void Main(string[] args)
    {
      // CreateWebHostBuilder(args).Build().Run();
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .CreateLogger();

      try
      {
        Log.Information("Starting web host");
        CreateWebHostBuilder(args)
          .UseSerilog()
          .Build()
          .Run();
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Host terminated unexpectedly");
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
  }
}