
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniCMS.Data;
using MiniCMS.Models.Identity;
using MiniCMS.Repositories.Implementations;
using MiniCMS.Repositories.Interfaces;
using MiniCMS.Services.Implementations;
using MiniCMS.Services.Interfaces;




var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});


//adding services 


builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITagRepository, TagRepository>();

builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddScoped<IPostService, PostService>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapStaticAssets();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   

app.UseAuthorization();    

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await MiniCMS.Seed.IdentitySeeder.SeedRolesAndAdminAsync(services);
}



app.Run();
