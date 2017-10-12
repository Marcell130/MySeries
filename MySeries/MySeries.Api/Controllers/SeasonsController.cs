using System.Threading.Tasks;
using System.Web.Http;
using MySeries.Api.Dto;
using MySeries.Api.EF;

namespace MySeries.Api.Controllers
{
    [Authorize]
    [RoutePrefix( "api/Seasons" )]
    public class SeasonsController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        [Route( "{id}" )]
        public IHttpActionResult GetSeason( int id )
        {
            var season = this.unitOfWork.SeasonRepository.GetSeason( id );
            var seasonDto = season.ToDetailedDto();

            return Ok( seasonDto );
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