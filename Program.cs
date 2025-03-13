using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using tj.DbContexts.SimpleBookStore;
using tj.SimpleBookStore;
using tj.SimpleBookStore.DbContexts;
using tj.SimpleBookStore.Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwagger();
// 注册所有 Service 和 Repository
builder.Services.RegisterServicesAndRepositories();

// 使用内存数据库
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("OnlineBookstoreDb"));
builder.Services.AddTransient<IStartupFilter, DataInitializationStartupFilter>();
builder.Services.AddSingleton<UserContext>();

// 配置 OpenTelemetry
builder.Services.RegisterOpenTelemetry();
// 配置日志记录
builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeFormattedMessage = true;
    options.IncludeScopes = true;
    options.ParseStateValues = true;
    options.AddConsoleExporter();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/V1/swagger.json", "tj.SimpleBookStoreV1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


