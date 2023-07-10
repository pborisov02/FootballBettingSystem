namespace SportsBetting.Web
{
    using Microsoft.EntityFrameworkCore;
	using Microsoft.IdentityModel.Tokens;

	using SportsBettingSystem.Data;
	using SportsBettingSystem.Data.Models;
	using SportsBettingSystem.Services;
	using SportsBettingSystem.Services.Interfaces;
	using SportsBettingSystem.Web.Infrastructure.Extensions;



    public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			
			builder.Services.AddDbContext<SportsBettingDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("SportsBettingSystem.Data")));


			builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 1;
			})
				.AddEntityFrameworkStores<SportsBettingDbContext>();
			builder.Services.AddControllersWithViews();

			//builder.Services.AddApplicationServices(typeof(AccountService));
			builder.Services.AddScoped<IAccountService, AccountService>();
            WebApplication app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapDefaultControllerRoute();
			app.MapRazorPages();

			app.Run();
		}
	}
}