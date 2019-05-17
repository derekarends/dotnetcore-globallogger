using Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api
{
  public class Startup
  {
    private readonly ILoggerFactory _loggerFactory;
    
    public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
      Configuration = configuration;
      _loggerFactory = loggerFactory;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.ConfigureExceptionHandler(_loggerFactory);
      app.UseMvc();
    }
  }
}