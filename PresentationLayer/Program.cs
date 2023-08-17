using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Mapping;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Context;
using EntityLayer.Concrete;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped<IBillService, BillManager>();
//builder.Services.AddScoped<IBillDal, EFBillDal>();
//builder.Services.AddAutoMapper(typeof(GeneralMapping));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(40);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddMvcCore();
// Add services to the container.

builder.Services.AddDbContext<BankContext>();
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<BankContext>();
builder.Services.AddHttpClient();
builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddHttpContextAccessor();

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
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();