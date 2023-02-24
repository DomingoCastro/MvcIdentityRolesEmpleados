using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MvcIdentityRolesEmpleados.Data;
using MvcIdentityRolesEmpleados.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.EnableEndpointRouting = false);
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme= CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme= CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();
string connectionString = builder.Configuration.GetConnectionString("SqlLocal");
builder.Services.AddTransient<RepositoryEmpleado>();
builder.Services.AddDbContext<EmpleadosContext>(options => options.UseSqlServer(connectionString));


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

app.UseMvc(routes =>
{
    routes.MapRoute(name: "default",
    template: "{controller=Empleados}/{action=Index}/{id?}");
});

app.Run();
