using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using log4net;
using MoreLinq;
using MySeries.DbUpdateService.MySeries;
using MySeries.DbUpdateService.Tmdb;
using MySeries.DbUpdateService.Tmdb.Model;
using Newtonsoft.Json;

namespace MySeries.DbUpdateService
{
    public class DatabaseUpdater
    {
        private static readonly ILog Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private static readonly TmdbService TmdbService = new TmdbService( Configuration.TmdbApiBaseAddress, Configuration.TmdbApiKey );
        private static readonly MySeriesService MySeriesService = new MySeriesService( Configuration.MySeriesApiBaseAddress );

        public async void UpdateDatabase( object sender, ElapsedEventArgs e )
        {
            //Log.Info( "Updating Database..." );
            //try
            //{
            //    var token = await MySeriesService.GetTokenAsync( Configuration.MySeriesUsername, Configuration.MySeriesPassword );

            //    var seriesTmdbIds = await MySeriesService.GetTvShowTmdbIds( token );

            //    foreach (var seriesTmdbId in seriesTmdbIds)
            //    {
            //        var seriesChanges = await TmdbService.GetSeriesChanges( seriesTmdbId );
            //        if (seriesChanges.Any())
            //        {
            //            var changes = HandleSeriesChanges( seriesChanges );
            //            //await MySeriesService.UpdateTvShow( changes, token );
            //        }

            //    }

            //    //TODO
            //    Console.ReadKey();

            //}
            //catch (Exception ex)
            //{
            //    Log.Error( "Fatal error when running", ex );
            //}
        }

        public async Task<SeriesChanges> HandleSeriesChanges( List<TmdbChange> tmdbChanges )
        {
            var seriesChanges = new SeriesChanges();

            foreach (var tmdbChange in tmdbChanges)
            {
                var changeItemEnglish = tmdbChange.items.FirstOrDefault( i => i.iso_639_1 == "en" );
                var changeItemSingle = tmdbChange.items.SingleOrDefault();

                switch (tmdbChange.key)
                {
                    case "overview":
                        if (changeItemEnglish != null)
                        {
                            seriesChanges.Overview = new Change<string>( changeItemEnglish.action, changeItemEnglish.ValueAs<string>() );
                        }
                        break;

                    case "images":
                        if (changeItemEnglish != null)
                        {
                            seriesChanges.PosterUriPostfix = new Change<string>( changeItemEnglish.action, changeItemEnglish.ValueAs<PosterChangeValue>().poster.file_path );
                        }
                        break;

                    case "season":
                        foreach (var seasonChange in tmdbChange.items)
                        {

                            var seasonTmdbId = seasonChange.ValueAs<SeasonChangeValue>().season_id;
                            var seasonChanges = await TmdbService.GetSeasonChanges( seasonTmdbId );
                            if (seasonChanges.Any())
                            {
                                var changes = await HandleSeasonChanges( seasonChanges );

                                seriesChanges.SeasonChanges.Add( new Change<SeasonChanges>( seasonChange.action, changes ) );
                            }
                        }

                        break;

                    case "translations":
                    case "certifications":
                        break;

                    default:
                        Log.Warn( $"Series - {tmdbChange.key} - {JsonConvert.SerializeObject( tmdbChange.items.FirstOrDefault() )}" );
                        break;

                }
            }

            return seriesChanges;

        }

        public async Task<SeasonChanges> HandleSeasonChanges( List<TmdbChange> tmdbChanges )
        {
            var seasonChanges = new SeasonChanges();

            foreach (var tmdbChange in tmdbChanges)
            {
                var changeItemEnglish = tmdbChange.items.FirstOrDefault( i => i.iso_639_1 == "en" );
                var changeItemSingle = tmdbChange.items.SingleOrDefault();

                switch (tmdbChange.key)
                {
                    case "overview":
                        if (changeItemEnglish != null)
                        {
                            seasonChanges.Overview = new Change<string>( changeItemEnglish.action, changeItemEnglish.ValueAs<string>() );
                        }
                        break;

                    case "episode":

                        foreach (var episodeChange in tmdbChange.items)
                        {

                            var episodeTmdbId = episodeChange.ValueAs<EpisodeChangeValue>().episode_id;
                            var episodeChanges = await TmdbService.GetEpisodeChanges( episodeTmdbId );
                            if (episodeChanges.Any())
                            {
                                var changes = await HandleEpisodeChanges( episodeChanges );

                                seasonChanges.EpisodeChanges.Add( new Change<EpisodeChanges>( episodeChange.action, changes ) );
                            }
                        }
                        break;

                    default:
                        Log.Warn( $"Season - {tmdbChange.key} - {JsonConvert.SerializeObject( tmdbChange.items.FirstOrDefault() )}" );
                        break;

                }
            }
            return seasonChanges;
        }


        public async Task<EpisodeChanges> HandleEpisodeChanges( List<TmdbChange> tmdbChanges )
        {
            var episodeChanges = new EpisodeChanges();

            foreach (var tmdbChange in tmdbChanges)
            {
                var changeItemEnglish = tmdbChange.items.FirstOrDefault( i => i.iso_639_1 == "en" );
                var changeItemSingle = tmdbChange.items.SingleOrDefault();

                switch (tmdbChange.key)
                {
                    case "overview":
                        if (changeItemEnglish != null)
                        {
                            episodeChanges.Overview = new Change<string>( changeItemEnglish.action, changeItemEnglish.ValueAs<string>() );
                        }
                        break;

                    default:
                        Log.Warn( $"Episode - {tmdbChange.key} - {JsonConvert.SerializeObject( tmdbChange.items.FirstOrDefault() )}" );
                        break;

                }
            }
            return episodeChanges;
        }


    }
}
