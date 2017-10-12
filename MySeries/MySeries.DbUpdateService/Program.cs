using System.Threading;
using Timer = System.Timers.Timer;

namespace MySeries.DbUpdateService
{
    public class Program
    {
        private static readonly DatabaseUpdater UpdaterService = new DatabaseUpdater();
        private static readonly Timer RunTimer = new Timer( Configuration.RunInterval.TotalMilliseconds );

        static void Main( string[] args )
        {
            RunTimer.Elapsed += UpdaterService.UpdateDatabase;
            RunTimer.Start();
            UpdaterService.UpdateDatabase( null, null );
            Thread.Sleep( Timeout.Infinite );
        }
    }
}
