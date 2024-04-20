using HRMS.DAO;
using HRMS.Reports;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var config = builder.Configuration;
var connectionString = config.GetConnectionString("HRMSConnectionString");
builder.Services.AddDbContext<HRMSDbContext>(o => o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//inject the IEmplyeeReport with emplyee detail report
builder.Services.AddScoped<IEmployeeReport, EmployeeDetailReport>();
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

