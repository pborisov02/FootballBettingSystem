namespace SportsBettingSystem.Web
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using SportsBettingSystem.Data;
	using SportsBettingSystem.Data.Models;
	using SportsBettingSystem.Services;
	using SportsBettingSystem.Web.Infrastructure.Extensions;
	using SportsBettingSystem.Web.Infrastructure.ModelBinders;

	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			
			builder.Services.AddDbContext<SportsBettingDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("SportsBettingSystem.Data")));


			builder.Services
				.AddDefaultIdentity<ApplicationUser>(options =>
				{
					options.SignIn.RequireConfirmedAccount = false;
					options.Password.RequireDigit = true;
					options.Password.RequireLowercase = true;
					options.Password.RequireUppercase = true;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequiredLength = 8;
				})
				.AddRoles<IdentityRole<Guid>>()
				.AddEntityFrameworkStores<SportsBettingDbContext>();
			
			builder.Services.AddControllersWithViews();
			
			builder.Services.AddAntiforgery(optiions =>
			{
				optiions.FormFieldName= "_-RequestVerificationToken";
				optiions.HeaderName = "X-CSRF-VERIFICATION-TOKEN";
				optiions.SuppressXFrameOptionsHeader = false;
			});
            
			builder.Services.AddRecaptchaService();

            builder.Services
                .AddControllersWithViews()
                .AddMvcOptions(options =>
                {
                    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
					options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
					options.EnableEndpointRouting = false;
                });

			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Account/Login";
				options.AccessDeniedPath = "/Home/Error/401";
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				options.Cookie.SameSite = SameSiteMode.None; 
			});

			builder.Services.AddApplicationServices(typeof(AccountService));
           
			WebApplication app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error/500");
				app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
				
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.SeedAdministrator("admin@sportsbetting.com");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
	                name: "Areas",
	                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/{controller=Home}/{action=Index}/{id?}");


	            endpoints.MapRazorPages();
            });

            app.Run();
		}
	}
}