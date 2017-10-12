using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MySeries.Api.Dto
{
    public class RoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public static class RoleExtension
    {
        public static RoleDto ToDto( this IdentityRole role )
        {
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}