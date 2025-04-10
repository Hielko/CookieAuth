using CookieAuth.Repo;
using CookieAuth.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUsersRepo, UsersRepo>();
builder.Services.AddSingleton<IAuthorizationHandler, AccountTypeHandler>();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = new PathString("/Account/Login");
            options.AccessDeniedPath = new PathString("/Account/AccessDenied");
            // options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
        });


//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("NameStartsWith1", policy => policy.RequireAssertion(context => context.User.Identity.IsAuthenticated && context.User.Identity.Name.StartsWith("1")));
//});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Premium", policy =>
        policy.Requirements.Add(new AccountTypeRequirement("Premium")));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();