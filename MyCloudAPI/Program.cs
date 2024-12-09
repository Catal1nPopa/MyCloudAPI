using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyCloudApplication;
using MyCloudApplication.Services;
using MyCloudHelper;
using MyCloudInfrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Host.ConfigureSerilog();
builder.Services.ConfigureJWT(builder.Configuration);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddHelperServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MyCloud",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//MappingConfig.RegisterMappings();

var app = builder.Build();
app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        LoggerHelper.LogInformation("Initializare baza de date");
        var context = services.GetRequiredService<MyDbContext>();
        //context.Database.EnsureCreated();
        context.Database.Migrate();
        LoggerHelper.LogInformation("Initializare finisata");
    }
    catch (Exception ex)
    {
        LoggerHelper.LogError(ex, $"Eroare la initializarea bazei de date : {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<FileShareHub>("/fileShareHub");

app.Run();
