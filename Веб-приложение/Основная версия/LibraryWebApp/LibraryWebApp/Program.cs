using Core;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Services.Services;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddHttpContextAccessor();
services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();

services.AddAdvancedDependencyInjection();
services.Scan(scan => scan
    .FromAssemblyOf<BaseRepository<Entity>>()
    .AddClasses(classes => classes
        .InNamespaces("Core.Entities")
        .AssignableTo<BaseRepository<Entity>>())
    .AsImplementedInterfaces()
    .WithTransientLifetime());
services.Scan(scan => scan
    .FromAssemblyOf<BaseService>()
    .AddClasses(classes => classes
        .InNamespaces("Data.ViewModels")
        .AssignableTo<BaseService>())
    .AsImplementedInterfaces()
    .WithTransientLifetime());

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Web API",
        Description = "API веб-версии приложения"
    });
});

var app = builder.Build();

app.UseAdvancedDependencyInjection();
app.UseHttpsRedirection();
app.UseRouting();
app.MapStaticAssets();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseSwagger();
app.UseSwaggerUI(c => c.RoutePrefix = "swagger");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    await DbInitializer.Initialize(context);
}

app.Run();