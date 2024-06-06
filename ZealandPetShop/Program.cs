using ItemRazorV1.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using ZealandPetShop.EFDbContext;
using ZealandPetShop.Models.Login;
using ZealandPetShop.Models.Shop;
using ZealandPetShop.Models.Order;
using ZealandPetShop.Services;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ItemDbContext>(); //en context
builder.Services.AddHttpContextAccessor(); //HttpContext til at access user's info


//Alle vores singleton services
builder.Services.AddSingleton<UserService, UserService>();
builder.Services.AddSingleton<ItemService, ItemService>();
builder.Services.AddSingleton<CartService, CartService>();
builder.Services.AddSingleton<OrderService, OrderService>();


//Alle vores transient(Dbgeneric services)
builder.Services.AddTransient<DbGenericService<Item>, DbGenericService<Item>>();
builder.Services.AddTransient<DbGenericService<User>, DbGenericService<User>>();
builder.Services.AddTransient<DbGenericService<OrderItem>, DbGenericService<OrderItem>>();
builder.Services.AddTransient<DbGenericService<Order>, DbGenericService<Order>>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
    cookieOptions.LoginPath = "/Login/LogIn";
     cookieOptions.LogoutPath = "/Login/Logout";

});
builder.Services.Configure<CookiePolicyOptions>(options => {
    // This lambda determines whether user consent for non-essential cookies is needed for a given request. options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;

});
var app = builder.Build();


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
