using Application.Adverts.Commands;
using Application.Adverts.Queries;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Mapping;
using AutoMapper;
using FluentValidation;
using Infrastructure.DataAccess;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("AdvertDatabase:ConnectionString")));

builder.Services.AddScoped<IAgencyDbConnection, AgencyDbConnection>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddValidatorsFromAssembly(typeof(ConfigureServices).Assembly);
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

builder.Services.AddMediatR(typeof(CreateAdvertCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetAdvertsPaginationList).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetAdvertsById).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(DeleteAdvertCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(UpdateAdvertCommand).GetTypeInfo().Assembly);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "RedisAdvertsProject";
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowedOrigins = "_myAllowedOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowedOrigins,
                          builder =>
                          {
                              builder.WithOrigins("http://localhost:3000")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

var app = builder.Build();

app.UseCors(MyAllowedOrigins);

app.UseHttpsRedirection();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
