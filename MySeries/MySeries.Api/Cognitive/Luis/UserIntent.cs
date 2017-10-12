using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MySeries.Api.Cognitive.Luis
{
    //[JsonConverter( typeof( StringEnumConverter ) )]
    public enum UserIntent
    {
        SuggestSeries,
        GetNextEpisode,
        None
    }

}