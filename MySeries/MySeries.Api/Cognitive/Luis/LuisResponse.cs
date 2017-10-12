using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySeries.Api.Cognitive.Luis
{
    public class LuisResponse
    {
        public UserIntent UserIntent { get; set; }
        public List<string> Entities { get; set; }
    }
}