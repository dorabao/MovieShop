using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using MovieShopMVC.Helpers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure Services
// Registering your Services/dependcies
builder.Services.AddControllersWithViews();

// Services Injection
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();

// Repositories Injection
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// inject the connection string into the MovieShopSbContext constructor using DbContextOptions
builder.Services.AddDbContext<MovieShopDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"))
    );

//tell the asp.net what kind of authentication we are using. Cookie based authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MovieShopAuthCookie";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.LoginPath = "/account/login";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseMovieShopExceptionMiddleware();
    app.UseHsts();
}
else 
{
    app.UseMovieShopExceptionMiddleware();
}


// Configure method in old ASP.NET
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
