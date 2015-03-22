using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UtilityFramework
{
    public class LogHelper
    {
        private static string m_loggerName = "DefaultLogger";

        static LogHelper()
        {
            log4net.Config.XmlConfigurator.Configure();

            if (!string.IsNullOrEmpty(
                System.Configuration.ConfigurationManager.AppSettings["Log4NetLoggerName"]))
            {
                m_loggerName = System.Configuration.ConfigurationManager.AppSettings["Log4NetLoggerName"];
            }
        }

        public static void Error(string p, Exception e)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            if (e != null && e is AggregateException)
            {
                AggregateException aggr = e as AggregateException;
                log.Warn(p + "\tAggregationExceptions: ");
                if (aggr != null && aggr.InnerExceptions != null &&
                    aggr.InnerExceptions.Count > 0)
                {
                    foreach (var er in aggr.InnerExceptions)
                    {
                        log.Error(p, er);
                    }
                }
                else if (aggr != null && aggr.InnerException != null)
                {
                    log.Error(p, aggr.InnerException);
                }
                log.Warn(p + "\tAggregationExceptions Ended. ");
            }
            log.Error(p, e);
        }

        public static void Error(string p)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            log.Error(p);
        }

        public static void Info(string p)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            log.Info(p);
        }

        public static void Debug(string p)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            log.Debug(p);
        }

        public static void Fatal(string p, Exception e)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            log.Fatal(p, e);
        }

        public static void Warn(string p)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            log.Warn(p);
        }

        public static void Warn(string p, Exception e)
        {
            ILog log = LogManager.GetLogger(m_loggerName);
            log.Warn(p, e);
        }
    }
}