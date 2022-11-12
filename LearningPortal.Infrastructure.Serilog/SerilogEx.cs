using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;

namespace LearningPortal.Infrastructure.Serilog
{
    public static class SerilogEx
    {
        public static void UseSerilog_SQLServer(this ConfigureHostBuilder webHostBuilder)
        {
            webHostBuilder.UseSerilog((builder, logger) =>
            {
                logger = new SerilogConfig().ConfigForSQLServer(LogEventLevel.Warning);
                logger.CreateLogger();
            });
        }
    }
}
