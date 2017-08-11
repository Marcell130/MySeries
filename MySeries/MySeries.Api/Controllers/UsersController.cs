using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MySeries.Api.Dto;
using MySeries.Api.EF;

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

		[HttpPost]
		[Route( "TvShows/{tvShowId}/Rate" )]
		public async Task<IHttpActionResult> AddTvShowRating( [FromUri] int tvShowId, [FromBody] int? rating )
		{
			var userId = User.Identity.GetUserId();
			await this.unitOfWork.AccountRepository.AddRatingForUserTvShow( userId, tvShowId, rating );
			await this.unitOfWork.SaveChangesAsync();

			return Ok();
		}
	}
}