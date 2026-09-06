using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using WMS.API.DependencyInjection;
using WMS.API.Middleware;
using WMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCoreDependencies(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000" , "http://45.94.214.48:8080")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600;
});

var app = builder.Build();

var storageRoot = builder.Configuration["Storage:RootPath"] ?? "uploads";
var uploadsPath = Path.IsPathRooted(storageRoot)
    ? storageRoot
    : Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, storageRoot));

// اگر پوشه وجود نداشت، بساز
Directory.CreateDirectory(uploadsPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
});

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "WMS API v1");
    options.RoutePrefix = "swagger";
});

app.MapOpenApi();

await app.MigrateAndSeedDatabaseAsync();

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost3000");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
