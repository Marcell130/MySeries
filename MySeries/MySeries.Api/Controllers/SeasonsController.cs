using System.Threading.Tasks;
using System.Web.Http;
using MySeries.Api.EF;

namespace MySeries.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Seasons")]
    public class SeasonsController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        [Route( "{seasonId}/Episodes" )]
        public async Task<IHttpActionResult> GetEpisodesInSeason( [FromUri] int seasonId )
        {
            var episodes = await this.unitOfWork.SeasonRepository.GetEpisodes( seasonId );

            return Ok( episodes );
        }
    }
}