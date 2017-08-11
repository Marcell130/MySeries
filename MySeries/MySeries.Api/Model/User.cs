using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MySeries.Api.Model
{
	public enum UserGender
	{
		Male,
		Female
	}

	public class ApplicationUser : IdentityUser
	{
		[Required]
		[MaxLength( 100 )]
		public string FirstName { get; set; }

		[Required]
		[MaxLength( 100 )]
		public string LastName { get; set; }


		[Required]
		public DateTime BirthDate { get; set; }

		[Required]
		public UserGender Gender { get; set; }

		[Required]
		public DateTime JoinDate { get; set; }


		public List<TvShow> TvShows { get; set; }
	}
}
