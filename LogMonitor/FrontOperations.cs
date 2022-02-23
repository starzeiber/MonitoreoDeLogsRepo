using BusinessLayer;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace LogMonitor
{
    internal static class FrontOperations
    {
        internal static bool Initialize()
        {
            return GetLog() != false &&
                GetParametersMonitor() != false;
        }
        private static bool GetLog()
        {
            try
            {
                IConfigurationBuilder? builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfigurationRoot? configuration = builder.Build();

                Helpers.logName = configuration["Logging:EventLog"];
                return true;
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry("Error al crear el log del WsTae, error: " + ex.Message, EventLogEntryType.Error);
                }
                return false;
            }

        }

        private static bool GetParametersMonitor()
        {
            try
            {
                IConfigurationBuilder? builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfigurationRoot? configuration = builder.Build();

                Helpers.monitorInterval = int.Parse(configuration["ParametersMonitor:Interval"]);
                Helpers.hourInitialMonitor = int.Parse(configuration["ParametersMonitor:HourInitialMonitor"]);
                Helpers.hourFinalMonitor = int.Parse(configuration["ParametersMonitor:HourFinalMonitor"]);
                Helpers.numTrxError = int.Parse(configuration["ParametersMonitor:NumTrxError"]);
                Helpers.maxTimeNotSales = Helpers.monitorInterval;
                Helpers.validateNotSales = configuration["ValidateNotSales"].ToString().ToUpper() == "SI";
                Helpers.clientName = configuration["ClientName"].ToString();
                return true;
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
                }
                return false;
            }
        }

        //private static bool GetMailConfig()
        //{
        //    try
        //    {
        //        IConfigurationBuilder? builder = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        //        IConfigurationRoot? configuration = builder.Build();

        //        Helpers.monitorInterval = int.Parse(configuration["Email:User"]);
        //        Helpers.hourInitialMonitor = int.Parse(configuration["Email:Pass"]);
        //        Helpers.hourFinalMonitor = int.Parse(configuration["Email:Smtp"]);
        //        Helpers.numTrxError = int.Parse(configuration["Email:Port"]);
        //        Helpers.withCertificate = configuration["Email:Port"].ToString().ToUpper() == "SI" ? true : false;
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        using (EventLog eventLog = new EventLog("Application"))
        //        {
        //            eventLog.Source = "Application";
        //            eventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
        //        }
        //        return false;
        //    }
        //}
    }
}
