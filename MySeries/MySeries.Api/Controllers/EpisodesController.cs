using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MySeries.Api.Dto;
using MySeries.Api.EF;

namespace MySeries.Api.Controllers
{
    [Authorize]
    [RoutePrefix( "api/Episodes" )]
    public class EpisodesController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        [Route( "{id}" )]
        public async Task<IHttpActionResult> GetEpisode( int id )
        {
            var episodeDto = await this.unitOfWork.EpisodeRepository.GetEpisode( id );

            return Ok( episodeDto );
        }

        [HttpPost]
        [Route( "{id}/Comments" )]
        public async Task<IHttpActionResult> AddComment( int id, [FromBody] string text )
        {
            var userId = User.Identity.GetUserId();

            this.unitOfWork.EpisodeRepository.AddComment( userId, id, text );

            await this.unitOfWork.SaveChangesAsync();

            return Ok();
        }

        //[HttpGet]
        //[Route( "{seasonId}/Episodes" )]
        //public async Task<IHttpActionResult> GetEpisodesInSeason( [FromUri] int seasonId )
        //{
        //    var episodes = await this.unitOfWork.SeasonRepository.GetEpisodes( seasonId );

        //    return Ok( episodes );
        //}
    }
}