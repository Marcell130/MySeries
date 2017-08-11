//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Web.Http.Routing;
//using MySeries.Api.Model;

//namespace MySeries.Api.Identity.Model
//{
//	public class ModelFactory
//	{
//		private UrlHelper _UrlHelper;
//		private ApplicationUserManager _AppUserManager;

//		public ModelFactory( HttpRequestMessage request, ApplicationUserManager appUserManager )
//		{
//			_UrlHelper = new UrlHelper( request );
//			_AppUserManager = appUserManager;
//		}

//		public UserReturnModel Create( ApplicationUser appUser )
//		{
//			return new UserReturnModel
//			{
//				Url = _UrlHelper.Link( "GetUserById", new { id = appUser.Id } ),
//				Id = appUser.Id,
//				UserName = appUser.UserName,
//				FullName = $"{appUser.FirstName} {appUser.LastName}",
//				Email = appUser.Email,
//				EmailConfirmed = appUser.EmailConfirmed,
//				BirthDate = appUser.BirthDate,
//				Gender = appUser.Gender,
//				JoinDate = appUser.JoinDate,
//				Roles = _AppUserManager.GetRolesAsync( appUser.Id ).Result,
//				Claims = _AppUserManager.GetClaimsAsync( appUser.Id ).Result
//			};
//		}
//	}

//	public class UserReturnModel
//	{
//		public string Url { get; set; }
//		public string Id { get; set; }
//		public string UserName { get; set; }
//		public string FullName { get; set; }
//		public string Email { get; set; }
//		public bool EmailConfirmed { get; set; }
//		public UserGender Gender { get; set; }
//		public DateTime BirthDate { get; set; }
//		public DateTime JoinDate { get; set; }
//		public IList<string> Roles { get; set; }
//		public IList<System.Security.Claims.Claim> Claims { get; set; }
//	}
//}