using Microsoft.Extensions.FileProviders;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();

//builder.Services.AddControllersWithViews(options =>
//{
//    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
//});

builder.Services.AddSession();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Attachment//")));

builder.Services.AddMvc();


var app = builder.Build();

var env = builder.Environment;

string connString = builder.Configuration.GetConnectionString("TrackBugsContext");

if (env.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}




app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/Error";
        await next();
    }
});

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

var cultureInfo = new CultureInfo("en-GB");
cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.UseRouting();

app.UseAuthorization();

//app.MapAreaControllerRoute(
//            name: "AreaDeveloper",
//            areaName: "Developer",
//            pattern: "Developer/{controller=DeveloperController}/{action=Index}/{id?}");

//app.MapAreaControllerRoute(
//            name: "AreaSupport",
//            areaName: "Support",
//            pattern: "Support/{controller=SupportController}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
            name: "AreaAdmin",
            areaName: "Admin",
            pattern: "Admin/{controller=AdminController}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
