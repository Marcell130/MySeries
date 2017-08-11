using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySeries.Api.EF;
using MySeries.Api.Identity;
using MySeries.Api.Identity.Model;
using MySeries.Api.Model;

namespace MySeries.Api.Repositories
{
	public class AccountRepository
	{
		private readonly MySeriesDbContext context;

		//private readonly ApplicationUserManager userManager;

		public AccountRepository( MySeriesDbContext context)
		{
			this.context = context;
		}

		public async Task<List<ApplicationUser>> GetUsers( Expression<Func<ApplicationUser, bool>> filter = null )
		{
			if( filter != null )
			{
				return await context.Users.Where( filter ).ToListAsync();
			}
			return await context.Users.ToListAsync();
		}
	}
}