using Microsoft.EntityFrameworkCore;
using OrderManager.Db.Interfaces;
using OrderManager.Db.Repositories;
using OrderManagerWebApp;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("order_manager");
builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IOrderRepository, OrderDbRepository>();
builder.Services.AddTransient<IProviderRepository, ProviderDbRepository>();
builder.Services.AddTransient<IOrderItemRepository, OrderItemDbRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

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
