using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySeries.Api.Model;

namespace MySeries.Api.Dto
{
	public class UserDto
	{
		public string FullName { get; set; }
		public string FistName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public UserGender Gender { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime JoinDate { get; set; }
	}

	public static class UserExtension
	{
		public static UserDto ToDto( this ApplicationUser user )
		{
			return new UserDto
			{
				FullName = $"{user.FirstName} {user.LastName}",
				FistName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				BirthDate = user.BirthDate,
				Gender = user.Gender,
				JoinDate = user.JoinDate,
			};
		}
	}
}