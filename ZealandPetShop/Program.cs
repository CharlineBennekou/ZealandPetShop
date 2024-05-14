using ItemRazorV1.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZealandPetShop.EFDbContext;
using ZealandPetShop.Models.Login;
using ZealandPetShop.Models.Shop;
using ZealandPetShop.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ItemDbContext>(); //en context

//Alle vores singleton services
builder.Services.AddSingleton<UserService, UserService>();
builder.Services.AddSingleton<ItemService, ItemService>();

//Alle vores transient(Dbgeneric services)
builder.Services.AddTransient<DbGenericService<Item>, DbGenericService<Item>>();
builder.Services.AddTransient<DbGenericService<User>, DbGenericService<User>>();



// Tilføjer EF Core
//builder.Services.AddDbContext<ItemDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Tilføjer Identity services
//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ItemDbContext>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.LoginPath = "/Login/LogIn";
     cookieOptions.LogoutPath = "/Login/Logout";

});
builder.Services.Configure<CookiePolicyOptions>(options => {
    // This lambda determines whether user consent for non-essential cookies is needed for a given request. options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;

});
var app = builder.Build();

//public void ConfigureServices(IServiceCollection services)
//{
//    services.AddRazorPages();
//    // Registrer PasswordHasher som en service
//    services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
//}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //Noget til login
app.UseAuthorization();

app.MapRazorPages();

app.Run();
