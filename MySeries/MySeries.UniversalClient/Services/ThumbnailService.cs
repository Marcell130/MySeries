using System;

namespace MySeries.UniversalClient.Services
{
    public enum ThumbnailSize
    {
        Small = 92,
        Medium = 154,
        Large = 342,
        Original = 780
    }

    public static class ThumbnailService
    {
        private static readonly string ThumbnailPrefix = "http://image.tmdb.org/t/p/w";

        public static Uri GetThumbnailUri( string postfix, ThumbnailSize size )
        {
            var thumnailUri = new Uri( $"{ThumbnailPrefix}{(int)size}{postfix}" );
            return thumnailUri;
        }
    }
}
