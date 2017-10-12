using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using MySeries.Api.Cognitive.Luis;
using MySeries.Api.EF;
using MySeries.Api.GlobalHandlers.Exceptions;
using MySeries.Api.Services;

namespace MySeries.Api.Controllers
{
    //TODO Authorize
    //[Authorize]
    [RoutePrefix( "api/Cognitive" )]
    public class CognitiveController : ApiController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        [Route( "UserQuery" )]
        public async Task<IHttpActionResult> HandleUserQuery( string query )
        {
            var cognitiveService = new CognitiveService();

            var luisResponse = await cognitiveService.GetUserIntent( query );


            switch (luisResponse.UserIntent)
            {
                case UserIntent.SuggestSeries:
                    var recommendedSeries = await cognitiveService.GetRecommendedSeries();
                    return Ok( new SuggestSeriesResult { Payload = recommendedSeries } );

                case UserIntent.GetNextEpisode:
                    var title = luisResponse.Entities.FirstOrDefault();
                    var tvShows = await this.unitOfWork.TvShowRepository.SearchTitle( title );
                    var tvShow = tvShows.OrderByDescending( t => t.Title.Length ).FirstOrDefault( t => query.ToLower().Contains( t.Title.ToLower() ) );

                    if (tvShow == null)
                    {
                        throw new EntityNotFoundException( $"Could not find series {title}" );
                    }

                    var nextEpisode = await this.unitOfWork.EpisodeRepository.GetNextEpisode( tvShow );

                    return Ok( new NextEpisodeResult { Payload = nextEpisode } );

                case UserIntent.None:
                    throw new BusinessLogicException( "Sorry, I cannot answer your question." );

                default:
                    throw new NotImplementedException();
            }
        }
    }
}