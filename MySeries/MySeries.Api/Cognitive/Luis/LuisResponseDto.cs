namespace MySeries.Api.Cognitive.Luis
{
    public class LuisResponseDto
    {
        //TODO [JsonProperty]
        public string query { get; set; }
        public LuisTopScoringIntent topScoringIntent { get; set; }
        public LuisEntity[] entities { get; set; }
    }
    public class LuisTopScoringIntent
    {
        public string intent { get; set; }
        //public float score { get; set; }
    }


    public class LuisEntity
    {
        public string entity { get; set; }
        //public string type { get; set; }
        //public float score { get; set; }
    }

}