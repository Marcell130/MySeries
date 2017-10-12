using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MySeries.Api.Cognitive.Luis;
using MySeries.Api.Dto;
using MySeries.Api.Model;
using RestSharp;

namespace MySeries.Api.Services
{

    public class CognitiveService
    {
        public async Task<LuisResponse> GetUserIntent( string query )
        {

            using (var client = new HttpClient { BaseAddress = new Uri( Configuration.LuisBaseAddress ) })
            {


                var response = await client.GetAsync( $"{Configuration.LuisAppId}?subscription-key={Configuration.LuisApiKey}&q={query}" );

                if (!response.IsSuccessStatusCode)
                {
                    if (response.Content != null)
                    {
                        throw new WebException( $"Server error {response.StatusCode} - {await response.Content.ReadAsStringAsync()}" );
                    }
                    throw new Exception( $"Server error {response.StatusCode}" );
                }
                else
                {
                    var luisResponseDto = await response.Content.ReadAsAsync<LuisResponseDto>();


                    var luisResponse = new LuisResponse
                    {
                        UserIntent = (UserIntent)Enum.Parse( typeof( UserIntent ), luisResponseDto.topScoringIntent.intent ),
                        Entities = luisResponseDto.entities.Select( e => e.entity ).ToList()
                    };

                    return luisResponse;
                }

            }



            //    var client = new RestClient( Configuration.LuisBaseAddress );

            //    var request = new RestRequest( Configuration.LuisAppId, Method.GET );

            //    request.AddParameter( "subscription-key", Configuration.LuisApiKey, ParameterType.QueryString );
            //    request.AddParameter( "q", query, ParameterType.QueryString );

            //    var response = await client.ExecuteGetTaskAsync<LuisResponseDto>( request );

            //    if (response.ResponseStatus != ResponseStatus.Completed)
            //    {
            //        throw new WebException( response.ErrorMessage, response.ErrorException );
            //    }



            //    var luisResponse = new LuisResponse
            //    {
            //        UserIntent = (UserIntent)Enum.Parse( typeof( UserIntent ), response.Data.topScoringIntent.intent ),
            //        Entities = response.Data.entities.Select( e => e.entity ).ToList()
            //    };

            //    return luisResponse;
        }

        public async Task<List<TvShowDto>> GetRecommendedSeries()
        {
            //TODO
            return new List<TvShowDto>
            {
                new TvShowDto{Title = "Vikings"},
                new TvShowDto{Title = "Narcos"},
                new TvShowDto{Title = "Sherlock"},
            };
        }



    }
}