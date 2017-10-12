using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MySeries.Api.Dto;
using MySeries.Api.EF;
using MySeries.Api.GlobalHandlers.Exceptions;
using MySeries.Api.Model;

namespace MySeries.Api.Controllers
{
    [Authorize]
    [RoutePrefix( "api/TvShows" )]
    public class TvShowsController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        [HttpPost]
        [Route( "{tvShowId}/Rate" )]
        public async Task<IHttpActionResult> AddTvShowRating( [FromUri] int tvShowId, [FromBody] int? rating )
        {
            var userId = User.Identity.GetUserId();
            await this.unitOfWork.AccountRepository.AddRatingForUserTvShow( userId, tvShowId, rating );
            await this.unitOfWork.SaveChangesAsync();

            return Ok();
        }

        [Authorize( Roles = "Admin" )]
        [HttpPost]
        [Route( "" )]
        public async Task<IHttpActionResult> AddOrUpdateTvShow( [FromBody] TvShow tvShow )
        {
            try
            {
                this.unitOfWork.TvShowRepository.InsertOrUpdate( tvShow );
                foreach (var genre in tvShow.Genres)
                {
                    this.unitOfWork.GenreRepository.InsertOrUpdate( genre );
                }
                foreach (var season in tvShow.Seasons)
                {
                    this.unitOfWork.SeasonRepository.InsertOrUpdateSeason( season );

                    foreach (var episode in season.Episodes)
                    {
                        this.unitOfWork.EpisodeRepository.InsertOrUpdate( episode );
                    }

                }

                await this.unitOfWork.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException( $"Error occured while adding show {tvShow?.Title ?? "<NULL>"} (TmdbId={tvShow?.TmdbId.ToString() ?? "<NULL>"})", ex, HttpStatusCode.BadRequest );
            }
        }

        [HttpGet]
        [Route( "{id}" )]
        public IHttpActionResult GetTvShow( int id )
        {
            var tvShow = this.unitOfWork.TvShowRepository.GetTvShow( id );
            var tvShowDto = tvShow.ToDetailedDto();

            return Ok( tvShowDto );
        }

        [HttpGet]
        [Route( "" )]
        public IHttpActionResult GetAllTvShows( int page, int perPage )
        {
            var tvShows = this.unitOfWork.TvShowRepository.GetTvShows( t => t.Title, page, perPage );
            var tvShowsDto = tvShows.Select( s => s.ToDto() ).ToList();

            return Ok( tvShowsDto );
        }
        
        //[HttpGet]
        //[Route( "{tvShowId}/Seasons" )]
        //public async Task<IHttpActionResult> GetSeasonsInTvShow( int tvShowId )
        //{
        //    var seasons = await this.unitOfWork.TvShowRepository.GetSeasons( tvShowId );
        //    var seasonsDto = seasons.Select( s => s.ToDto() ).ToList();

        //    return Ok( seasonsDto );
        //}

        [Authorize( Roles = "Admin" )]
        [HttpGet]
        [Route( "TmdbIds" )]
        public async Task<IHttpActionResult> GetTmdbIds()
        {
            var tmdbIds = await this.unitOfWork.TvShowRepository.GetTmdbIds();
            return Ok( tmdbIds );
        }
    }
}