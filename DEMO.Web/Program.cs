using DEMO.Data.Entities;
using DEMO.Repository;
using DEMO.Services;
using DEMO.Web.Areas.Admin.Mappings;  // If you put MappingProfile under Areas/Admin/Mappings
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add GetConnectionString
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Add Scoped Repository-------------
builder.Services.AddScoped<IAdminLoginRepository, AdminLoginRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<ILoanCategoryRepository, LoanCategoryRepository>();
builder.Services.AddScoped<ICategoryPageRepository, CategoryPageRepository>();
builder.Services.AddScoped<ISubCategoryPageRepository, SubCategoryPageRepository>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();

// ✅ Add Scoped Services-------------
builder.Services.AddScoped<IAdminAuthService, AdminAuthService>();

builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddScoped<ILoanCategoryService, LoanCategoryService>();

builder.Services.AddScoped<ICategoryPageService, CategoryPageService>();

builder.Services.AddScoped<ISubCategoryPageService, SubCategoryPageService>();

builder.Services.AddScoped<IRegistrationService, RegistrationService>();

// ✅ Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ✅ Add Session Support (for 15-min auto logout)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15); // Auto logout after 15 minutes of inactivity
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ✅ Optional: Add Authentication if you plan to use cookie-based login in future
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    var admins = db.AdminLogins.ToList();

//    foreach (var admin in admins)
//    {
//        if (!admin.Password.StartsWith("$2")) // skip already hashed
//        {
//            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
//        }
//    }

//    db.SaveChanges();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Enable Authentication (if added above)
app.UseAuthentication();

// ✅ Enable Session Middleware
app.UseSession();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Home}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Auth}/{action=Login}/{id?}");

app.Run();
