using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File($"logs/brainStormSessions.log", rollingInterval: RollingInterval.Hour)
                .WriteTo.Email(
                    fromEmail: "fedor_seliankin@epam.com",
                    toEmail: "fedor_seliankin@epam.com",
                    mailServer: "mailserver.com",                    
                    restrictedToMinimumLevel: LogEventLevel.Error,
                    mailSubject: "Log Notification",
                    batchPostingLimit: 1
                )
                .CreateLogger();

            try
            {
                Log.Information("Application startup");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
