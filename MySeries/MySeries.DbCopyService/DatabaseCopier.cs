using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using log4net;
using MySeries.DbCopyService.Model;
using MySeries.DbCopyService.MySeries;
using MySeries.DbCopyService.Tmdb;

namespace MySeries.DbCopyService
{
    public class DatabaseCopier
    {
        private static readonly ILog Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private static readonly TmdbService TmdbService = new TmdbService( Configuration.TmdbApiBaseAddress, Configuration.TmdbApiKey );
        private static readonly MySeriesService MySeriesService = new MySeriesService( Configuration.MySeriesApiBaseAddress );

        public async void CopyDatabase( object sender, ElapsedEventArgs args )
        {
            Log.Info( "Copying Database..." );
            try
            {
                var token = await MySeriesService.GetTokenAsync( Configuration.MySeriesUsername, Configuration.MySeriesPassword );

                var tvShowsInDatabase = await MySeriesService.GetTvShowTmdbIds( token );

                for (int i = 1; i <= 100; i++)
                {
                    try
                    {
                        var tvShowIds = await TmdbService.GetPopularIds( i );

                        Log.Info( $"Got {tvShowIds.Count} Series on page {i} from Tmdb" );

                        foreach (var tvShowId in tvShowIds.Where( t => !tvShowsInDatabase.Contains( t ) ))
                        {
                            var tvShow = await TmdbService.GetTvShow( tvShowId );

                            try
                            {
                                if (tvShow.InProduction == false || tvShow.NumberOfEpisodes > 500 || tvShow.Type == "Talk Show")
                                {
                                    Log.Warn( $"Skipping {tvShow.Title} with {tvShow.Seasons.Count} Seasons and {tvShow.NumberOfEpisodes} Episodes..." );
                                }
                                else
                                {
                                    Log.Debug( $"Adding {tvShow.Title} with {tvShow.Seasons.Count} Seasons and {tvShow.NumberOfEpisodes} Episodes..." );

                                    foreach (var season in tvShow.Seasons)
                                    {
                                        foreach (var episode in season.Episodes.Where( e => e.Overview.Length > 1000 ))
                                        {
                                            episode.Overview = episode.Overview.Substring( 0, 1000 );
                                        }
                                    }

                                    await MySeriesService.AddTvShow( tvShow, token );

                                    Log.Info( $"{tvShow.Title} added successfully" );
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error( $"Error adding {tvShow?.Title ?? "<noname>"} - {ex.Message}" );
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error( $"Error processing page {i}", ex );
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error( "Fatal error when running", ex );
            }
        }
    }
}
