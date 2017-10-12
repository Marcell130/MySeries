using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySeries.Client.Core.Model;
using MySeries.Client.Core.Model.BindingModels;

namespace MySeries.Client.Core.Services
{
    public interface IMySeriesDataService
    {
        Task AddTvShowForUserAsync(Token token, int tvShowId);
        Task<List<TvShow>> GetAllSeriesAsync(Token token, int page, int perPage);
        Task<List<Episode>> GetEpisodesInInterval(Token token, DateTime startDate, DateTime endDate);
        Task<List<Episode>> GetEpisodesInSeasonAsync(Token token, int seasonId);
        Task<UserInfo> GetUserInfoAsync(Token token);
        Task<List<TvShow>> GetUserSeriesAsync(Token token);
        Task<bool> IsTvShowForUserAsync(Token token, int tvShowId);
        Task RemoveTvShowForUserAsync(Token token, int tvShowId);
        Task SignupAsync(SignupBindingModel model);
        Task<Token> TryGetTokenAsync(string username, string password);
        Task<Season> GetSeasonDetailsAsync(Token token, int seasonId);
        Task<Episode> GetEpisodeDetailsAsync(Token token, int episodeId);
        Task<TvShow> GetSeriesDetailsAsync(Token token, int seriesId);
    }
}