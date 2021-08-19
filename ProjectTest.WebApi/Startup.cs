using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProjectTest.Models;
using ProjectTest.Models.DAL;
using ProjectTest.WebApi.Interfaces;
using ProjectTest.WebApi.Services;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.WebApi
{
    public class Startup
    {
        public IConfiguration _configuration;
        private string _aspnetcoreENV = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            string ConnectionString = _configuration.GetConnectionString("BloggingDatabase");
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddDataProtection();
            services.AddSession();
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API WSVAP (WebSmartView)", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });

            services.AddSingleton<IConfiguration>(_configuration);
            services.AddTransient<IProductServices, PersonsServices>();
            services.AddTransient<IStockServices, StockServices>();
            services.AddTransient<IUserServices, UserServices>();

            services.AddDbContext<DBtestContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
                options.EnableSensitiveDataLogging();
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                       .AllowAnyMethod()
                                       .AllowAnyHeader());
            });
            services.AddOptions();
            services.AddMvcCore(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));

            })
            .AddApiExplorer()
            .AddFormatterMappings();
            
            services.AddMemoryCache();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddApplicationInsightsTelemetry();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory logger)
        {
            var appPath = Directory.GetCurrentDirectory();

            var dirLog = Path.Combine(appPath, "upload", "APILogs", DateTime.Now.ToString("yyyyMMdd"));
            if (String.IsNullOrEmpty(dirLog) == false)
            {
                if (Directory.Exists(dirLog) == false)
                {
                    Directory.CreateDirectory(dirLog);
                }
                var logName = Path.Combine(dirLog, "TERT-{Date}.log");
                var logINF = Path.Combine(dirLog, "TERT-INF-{Date}.log");
                var logDEB = Path.Combine(dirLog, "TERT-DEB-{Date}.log");
                var logERR = Path.Combine(dirLog, "TERT-ERR-{Date}.log");
                //logger.AddFile(logName);
                Log.Logger = new LoggerConfiguration()                        
                                  .MinimumLevel.Information()
                                  .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                                  .MinimumLevel.Override("System", LogEventLevel.Error)
                                  .Enrich.FromLogContext()
                                  .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo.RollingFile(logINF))
                                  .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug).WriteTo.RollingFile(logDEB))
                                  .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error).WriteTo.RollingFile(logERR))
                                  .CreateLogger();
                //logger.AddDebug();
                logger.AddSerilog();
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseHsts();
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "My API V1");
            });
            //DummyData.Initialize(app);

            var path = Path.Combine(appPath, "upload", "APILogs");
            var dir = Directory.Exists(path);
            if (!dir)
            {
                Directory.CreateDirectory(path);
            }

        }
    }
}
