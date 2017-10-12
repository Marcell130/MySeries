using System.Threading.Tasks;
using System.Web.Http;
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
        public IHttpActionResult GetEpisode( int id )
        {
            var episode = this.unitOfWork.EpisodeRepository.GetEpisode( id );
            var episodeDto = episode.ToDetailedDto();

            return Ok( episodeDto );
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