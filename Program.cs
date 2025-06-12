using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Business.Service;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Services to DI
builder.Services.AddControllersWithViews();

// 2. Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Add Identity
builder.Services.AddIdentity<MemberEntity, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 4. Configure Authentication Cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});

// 5. Add External Authentication (Google)
builder.Services.AddAuthentication()
    .AddGoogle("Google", options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
        options.CallbackPath = "/signin-google";
    });

// 6. Register Custom Services & Repositories
builder.Services.AddScoped<ProjectRepo>();         // ? ????? ???
builder.Services.AddScoped<ProjectService>();      // ? ????? ???? ?????

// 7. Optional: Register other repos/services here if needed
// builder.Services.AddScoped<OtherRepo>();
// builder.Services.AddScoped<OtherService>();
builder.Services.AddScoped<NotificationService>();

var app = builder.Build();

// 8. Middleware Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// 9. Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();

