namespace SportsBetting.Web
{
    using Microsoft.EntityFrameworkCore;

	using SportsBettingSystem.Data;
	using SportsBettingSystem.Data.Models;


	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			
			builder.Services.AddDbContext<SportsBettingDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("SportsBettingSystem.Web")));


			builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
			{
				options.SignIn.RequireConfirmedAccount = true;
			})
				.AddEntityFrameworkStores<SportsBettingDbContext>();
			builder.Services.AddControllersWithViews();


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