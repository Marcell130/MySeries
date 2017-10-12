using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MySeries.Api.EF;
using MySeries.Api.GlobalHandlers.Exceptions;

namespace MySeries.Api.Controllers
{
    [Authorize]
    [RoutePrefix( "api/Me" )]
    public class UsersController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        [Route( "" )]
        public async Task<IHttpActionResult> GetUserInfo()
        {
            var userId = User.Identity.GetUserId();
            var userInfo = await this.unitOfWork.AccountRepository.GetInfo( userId );

            return Ok( userInfo );
        }

        [HttpGet]
        [Route( "TvShows" )]
        public async Task<IHttpActionResult> GetTvShowsForUser()
        {
            var userId = User.Identity.GetUserId();
            var tvShows = await this.unitOfWork.AccountRepository.GetTvShowsForUser( userId );

            return Ok( tvShows );
        }

        [HttpGet]
        [Route( "TvShows/{tvShowId}" )]
        public async Task<IHttpActionResult> IsTvShowAddedForUser( int tvShowId )
        {
            var userId = User.Identity.GetUserId();
            var isAdded = await this.unitOfWork.AccountRepository.IsTvShowAddedForUser( userId, tvShowId );

            return Ok( isAdded );
        }

        [HttpPost]
        [Route( "TvShows/{tvShowId}" )]
        public async Task<IHttpActionResult> AddTvShowForUser( int tvShowId )
        {
            var userId = User.Identity.GetUserId();
            await this.unitOfWork.AccountRepository.AddTvShowForUser( userId, tvShowId );
            await this.unitOfWork.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route( "TvShows/{tvShowId}" )]
        public async Task<IHttpActionResult> RemoveTvShowForUser( int tvShowId )
        {
            var userId = User.Identity.GetUserId();
            await this.unitOfWork.AccountRepository.RemoveTvShowForUser( userId, tvShowId );
            await this.unitOfWork.SaveChangesAsync();

            return Ok();
        }


        [HttpGet]
        [Route( "Episodes" )]
        public async Task<IHttpActionResult> GetEpisodesInInterval( [FromUri] DateTime startDate, [FromUri] DateTime endDate )
        {
            var userId = User.Identity.GetUserId();
            var episodes = await this.unitOfWork.AccountRepository.GetEpisodesInInterval( userId, startDate, endDate );

            return Ok( episodes );
        }
    }
}