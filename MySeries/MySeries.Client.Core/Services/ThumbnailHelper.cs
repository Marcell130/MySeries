namespace MySeries.Client.Core.Services
{
    public enum ThumbnailSize
    {
        VerySmall = 45,
        Small = 92,
        Medium = 154,
        Large = 342,
        Original = 780
    }

    public static class ThumbnailHelper
    {
        private const string ThumbnailPrefix = "http://image.tmdb.org/t/p/w";

        public static string GetThumbnailUri(string postfix, ThumbnailSize size)
        {
            var thumnailUri = $"{ThumbnailPrefix}{(int)size}{postfix}";
            return thumnailUri;
        }
    }
}
