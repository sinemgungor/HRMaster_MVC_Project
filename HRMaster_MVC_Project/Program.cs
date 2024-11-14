using HRMaster_MVC_Project.Models.Enums;
using HRMaster_MVC_Project.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using HRMaster_MVC_Project.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new CustomEnumConverter<BloodGroup>());
    options.JsonSerializerOptions.Converters.Add(new CustomEnumConverter<Gender>());
    options.JsonSerializerOptions.Converters.Add(new CustomEnumConverter<MarialStatus>());
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // JSON anahtarlarýný camelCase yapar
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // Null deðerleri yok sayar
    options.JsonSerializerOptions.AllowTrailingCommas = true;
});

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7061/");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddDistributedMemoryCache(); // Session için gerekli
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // Session zaman aþýmý
    options.Cookie.HttpOnly = true; // Cookie'ye sadece HTTP üzerinden eriþim
    options.Cookie.IsEssential = true; // GDPR uyumu için
});
builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "NetCoreMvc.Auth";
    options.LoginPath = "/Login/Login";
    options.AccessDeniedPath = "/Login/Login";
});

builder.Services.AddAutoMapper(x => x.AddProfile(typeof(CompanyMapper)));

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
app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});



app.Run();
