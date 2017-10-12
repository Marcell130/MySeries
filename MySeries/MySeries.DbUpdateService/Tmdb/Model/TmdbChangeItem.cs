namespace MySeries.DbUpdateService.Tmdb.Model
{
    public class TmdbChangeItem<T>
    {
        public string id { get; set; }
        public ChangeAction action { get; set; }
        public string time { get; set; }
        public T value { get; set; }
        public string iso_639_1 { get; set; }

        public TV ValueAs<TV>() => (TV)(object)value;
    }


    public enum ChangeAction
    {
        added,
        created,
        updated,
        deleted
    }
}