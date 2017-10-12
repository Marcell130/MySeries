using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySeries.Api.Model;

namespace MySeries.Api.Dto
{
    public class EpisodeCommentDto
    {
        public string Username { get; set; }

        public DateTime Timestamp { get; set; }

        public string Text { get; set; }
    }

    public static class EpisodeCommentExtension
    {
        public static EpisodeCommentDto ToDto( this EpisodeComment comment )
        {
            var commentDto = new EpisodeCommentDto
            {
                Username = comment.User?.UserName,
                Timestamp = comment.Timestamp,
                Text = comment.Text
            };
            return commentDto;
        }

    }
}