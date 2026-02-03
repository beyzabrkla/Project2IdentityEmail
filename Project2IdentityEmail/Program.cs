using Microsoft.AspNetCore.Identity;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext Tanýmlamasý (Zaten olabilir, kontrol et)
builder.Services.AddDbContext<EMailContext>();

// 2. Identity Servislerinin Eklenmesi (Hatanýn asýl çözümü burasý)
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<EMailContext>()
    .AddErrorDescriber<CustomIdentityValidator>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login/";
    options.AccessDeniedPath = "/Error/AccessDenied/";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

//    // Rolleri oluþtur
//    if (!await roleManager.RoleExistsAsync("Admin")) await roleManager.CreateAsync(new AppRole { Name = "Admin" });
//    if (!await roleManager.RoleExistsAsync("Member")) await roleManager.CreateAsync(new AppRole { Name = "Member" });

//    // Mevcut bir kullanýcýyý (kendini) admin yap
//    var user = await userManager.FindByEmailAsync("beyzarabia121@gmail.com"); // Buraya kendi mailini yaz!
//    if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
//    {
//        await userManager.AddToRoleAsync(user, "Admin");
//    }
//}

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
