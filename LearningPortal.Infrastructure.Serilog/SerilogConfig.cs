using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;

namespace LearningPortal.Infrastructure.Serilog
{
    class SerilogConfig
    {
        public LoggerConfiguration ConfigForSQLServer(LogEventLevel logEventLevel)
        {
            var columnOpt = new ColumnOptions();
            columnOpt.Store.Remove(StandardColumn.Properties);
            columnOpt.Store.Add(StandardColumn.LogEvent);
            columnOpt.LogEvent.DataLength = -1;
            columnOpt.PrimaryKey = columnOpt.TimeStamp;
            columnOpt.TimeStamp.NonClusteredIndex = true;

            return new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .MinimumLevel.Is(logEventLevel)
                        .WriteTo.MSSqlServer(@"Server=PROMISED\PROMISED_COMING;Database=LogDb;Trusted_Connection=True;", new MSSqlServerSinkOptions
                        {
                            AutoCreateSqlTable = true,
                            TableName = "tblLearningPortalLog",
                            BatchPeriod = new TimeSpan(0, 0, 1),
                        }, columnOptions: columnOpt);
        }
    }
}
