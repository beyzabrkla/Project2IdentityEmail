using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext Tanýmlamasý (Zaten olabilir, kontrol et)
builder.Services.AddDbContext<EMailContext>();

// 2. Identity Servislerinin Eklenmesi (Hatanýn asýl çözümü burasý)
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<EMailContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
