using ChallengeN5.Repositories.Contexts;
using ChallengeN5.Repositories.Mapping;
using ChallengeN5.Repositories.UnitOfWork;
using ChallengeN5.Services.Services;
using ChallengeN5.Services.Services.Impl;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using ChallengeN5.Api.Middlewares;
using Nest;

var builder = WebApplication.CreateBuilder(args);

//add cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(o =>
            o.SuppressModelStateInvalidFilter = true)
        .AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
        .DefaultIndex("permissions");

builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IElasticClient>(new ElasticClient(settings));
builder.Services.AddSingleton<IKafkaService, KafkaService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));    

// Add services to the container.
builder.Services.AddDbContext<N5DbContext>(opt => opt
    .UseSqlServer(builder.Configuration.GetConnectionString("N5Challenge"),
    o => o.MigrationsAssembly("ChallengeN5.Api")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseGlobalException();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
