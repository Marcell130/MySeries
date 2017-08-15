using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MySeries.Api.EF;

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
    }
}