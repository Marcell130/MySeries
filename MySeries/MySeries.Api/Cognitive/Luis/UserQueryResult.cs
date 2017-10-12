using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySeries.Api.Dto;
using MySeries.Api.Model;
using MySeries.Api.Services;

namespace MySeries.Api.Cognitive.Luis
{
    public interface IUserQueryResult<T>
    {
        UserIntent UserIntent { get; }
        T Payload { get; set; }
    }

    public class NextEpisodeResult : IUserQueryResult<EpisodeDto>
    {
        public UserIntent UserIntent { get; } = UserIntent.GetNextEpisode;
        public EpisodeDto Payload { get; set; }
    }

    public class SuggestSeriesResult : IUserQueryResult<List<TvShowDto>>
    {
        public UserIntent UserIntent { get; } = UserIntent.SuggestSeries;
        public List<TvShowDto> Payload { get; set; }
    }
}