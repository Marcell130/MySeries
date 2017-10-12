using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySeries.Api.Dto;
using MySeries.Api.EF;
using MySeries.Api.GlobalHandlers;
using MySeries.Api.GlobalHandlers.Exceptions;
using MySeries.Api.Identity;
using MySeries.Api.Identity.Model;
using MySeries.Api.Model;

namespace MySeries.Api.Repositories
{
    public class AccountRepository
    {
        private readonly MySeriesDbContext context;

        public AccountRepository(MySeriesDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ApplicationUser>> GetUsers(Expression<Func<ApplicationUser, bool>> filter = null)
        {
            if (filter != null)
            {
                return await this.context.Users.Where(filter).ToListAsync();
            }
            return await this.context.Users.ToListAsync();
        }

        public async Task<UserDto> GetInfo(string userId)
        {
            var userInfo = (await this.context.Users.SingleOrDefaultAsync(u => u.Id == userId)).ToDto();
            return userInfo;
        }

        public async Task<List<TvShowDto>> GetTvShowsForUser(string userId)
        {
            var tvShows = await this.context.UserTvShows.Include(uts => uts.TvShow)
                                                        //.Include( uts => uts.TvShow.Seasons )
                                                        .Where(uts => uts.UserId == userId)
                                                        .ToListAsync();
            //TODO
            //var tvShowsDto = tvShows.Select(uts => uts.TvShow.ToDto(uts.AddedDate, uts.Rating)).ToList();
            var tvShowsDto = tvShows.Select(uts => uts.TvShow.ToDto()).ToList();
            return tvShowsDto;
        }

        public async Task<bool> IsTvShowAddedForUser(string userId, int tvShowId)
        {
            return await this.context.UserTvShows.AnyAsync(uts => uts.UserId == userId && uts.TvShowId == tvShowId);
        }

        public async Task AddTvShowForUser(string userId, int tvShowId)
        {
            if (await IsTvShowAddedForUser(userId, tvShowId))
            {
                throw new BusinessLogicException("Series already added");
            }

            var userTvShow = new UserTvShow
            {
                UserId = userId,
                TvShowId = tvShowId,
                AddedDate = DateTime.Today
            };

            this.context.UserTvShows.Add(userTvShow);
        }

        public async Task RemoveTvShowForUser(string userId, int tvShowId)
        {
            var userTvShow = await this.context.UserTvShows.SingleOrDefaultAsync(uts => uts.UserId == userId && uts.TvShowId == tvShowId);

            if (userTvShow == null)
            {
                throw new BusinessLogicException("Series already removed");
            }

            this.context.UserTvShows.Remove(userTvShow);
        }

        public async Task AddRatingForUserTvShow(string userId, int tvShowId, int? rating)
        {
            var userTvShow = await this.context.UserTvShows.SingleOrDefaultAsync(uts => uts.UserId == userId && uts.TvShowId == tvShowId);
            if (userTvShow == null)
            {
                throw new BusinessLogicException("Need to add series before rating");
            }

            userTvShow.Rating = rating;
        }

        public async Task<List<EpisodeDto>> GetEpisodesInInterval(string userId, DateTime startDate, DateTime endDate)
        {
            var episodes = await this.context.Episodes.Include(e => e.Season)
                .Include(e => e.Season.TvShow)
                .Include(e => e.Season.TvShow.UsersTvShows)
                .Where(e => e.Season.TvShow.UsersTvShows.Select(utv => utv.UserId).Contains(userId))
                .Where(e => e.AirDate.HasValue)
                .Where(e => e.AirDate.Value >= startDate)
                .Where(e => e.AirDate.Value <= endDate)
                .ToListAsync();
            return episodes.Select(e => e.ToDto()).ToList();
        }

        public Task Exception()
        {
            throw new BusinessLogicException("OMG OMG OMG show this please");
        }
    }
}