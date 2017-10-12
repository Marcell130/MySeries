using System;
using System.Collections.Generic;
using MySeries.DbUpdateService.Model;

namespace MySeries.DbUpdateService.Tmdb.Model
{
    public class TmdbChanges
    {
        public TmdbChange[] changes { get; set; }
    }

    public class TmdbChange
    {
        public string key { get; set; }
        public TmdbChangeItem<object>[] items { get; set; }
    }

    public class SeriesChanges
    {
        public List<Change<SeasonChanges>> SeasonChanges { get; set; } = new List<Change<SeasonChanges>>();



        public Change<int> TmdbId { get; set; }

        public Change<string> Title { get; set; }
        public Change<string> WallpaperUriPostfix { get; set; }
        public Change<string> PosterUriPostfix { get; set; }
        public Change<int> SeasonCount { get; set; }
        public Change<string> Overview { get; set; }

        public Change<List<Genre>> Genres { get; set; }
        public Change<bool> InProduction { get; set; }
        public Change<int> VoteCount { get; set; }
        public Change<string> Status { get; set; }
        public Change<string> Type { get; set; }
        public Change<int> NumberOfEpisodes { get; set; }

        public Change<DateTime?> FirstAirDate { get; set; }
        public Change<DateTime?> LastAirDate { get; set; }
        public Change<TvShowState> State { get; set; }

    }

    public enum TvShowState
    {
        NoInfo = 0,
        OnGoing,
        AtSeasonEnd,
        TvShowEnded,
    }

    public class SeasonChanges
    {
        public List<Change<EpisodeChanges>> EpisodeChanges { get; set; } = new List<Change<EpisodeChanges>>();

        public Change<int> TmdbId { get; set; }
        public Change<int> SeasonNumber { get; set; }
        public Change<int> EpisodeCount { get; set; }
        public Change<string> PosterUriPostfix { get; set; }
        public Change<string> Title { get; set; }
        public Change<string> Overview { get; set; }
        public Change<DateTime?> AirDate { get; set; }
    }

    public class EpisodeChanges
    {
        public Change<string> Title { get; set; }
        public Change<int> TmdbId { get; set; }
        public Change<DateTime?> AirDate { get; set; }
        public Change<string> Overview { get; set; }
        public Change<string> PosterUriPostfix { get; set; }
        public Change<int> EpisodeNumber { get; set; }
    }

    public class Change<T>
    {
        public ChangeAction Action { get; set; }
        public T NewValue { get; set; }

        public Change( ChangeAction action, T newValue )
        {
            Action = action;
            NewValue = newValue;
        }
    }
}
