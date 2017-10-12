using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Timer = System.Timers.Timer;

namespace MySeries.DbCopyService
{
    public class Program
    {
        private static readonly DatabaseCopier CopyService = new DatabaseCopier();
        private static readonly Timer RunTimer = new Timer( Configuration.RunInterval.TotalMilliseconds );

        static void Main( string[] args )
        {
            RunTimer.Elapsed += CopyService.CopyDatabase;
            RunTimer.Start();
            CopyService.CopyDatabase(null, null);
            Thread.Sleep( Timeout.Infinite );
        }
    }
}
