
using EducaOnline.Aluno.API.Configuration;
using EducaOnline.Catalogo.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();


builder.Services.AddApiConfig(builder.Configuration);
builder.Services.AddDependencyConfig();
builder.Services.AddMessageBusConfiguration(builder.Configuration);
//builder.Services.AddSeedConfig(builder.Configuration);
//builder.Services.AddAutoMapper(typeof(ConteudoMapperConfig));

var app = builder.Build();

app.UseApiConfig(app.Environment);

app.Run();
