using System;
using log4net;
using log4net.Core;

namespace MySeries.Api.GlobalHandlers.Logging
{
    public static class TraceExtension
    {
        private static readonly Type DeclaringType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

        public static void Trace(this ILog log, object message)
        {
            log.Trace(message, null);
        }
        public static void Trace(this ILog log, object message, Exception exception)
        {
            log.Logger.Log(DeclaringType, Level.Trace, message, exception);
        }
        public static void TraceFormat(this ILog log, object message, object arg0)
        {
            log.TraceFormat(null, message, arg0);
        }
        public static void TraceFormat(this ILog log, object message, object arg0, object arg1)
        {
            log.TraceFormat(null, message, arg0, arg1);
        }
        public static void TraceFormat(this ILog log, object message, object arg0, object arg1, object arg2)
        {
            log.TraceFormat(null, message, arg0, arg1, arg2);
        }
        public static void TraceFormat(this ILog log, object message, params object[] args)
        {
            log.TraceFormat(null, message, args);
        }
        public static void TraceFormat(this ILog log, IFormatProvider provider, object message, params object[] args)
        {
            string formattedMessage = string.Format(provider, message.ToString(), args);
            log.Logger.Log(DeclaringType, Level.Trace, formattedMessage, null);
        }
    }
}