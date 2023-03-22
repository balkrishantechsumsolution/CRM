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

var app = builder.Build();

string connString = builder.Configuration.GetConnectionString("TrackBugsContext");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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
