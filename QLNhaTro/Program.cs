using Microsoft.EntityFrameworkCore;
using QLNhaTro.Data;
using QLNhaTro.Repositories;
using QLNhaTro.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<NguoiThueService>();
builder.Services.AddScoped<PhongService>();
builder.Services.AddScoped<HoaDonService>();
builder.Services.AddScoped<HopDongService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
//Add Dbcontext
builder.Services.AddDbContext<NhaTroDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("QLNhaTro")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddSession();

var app = builder.Build();
app.UseSession();

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
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
