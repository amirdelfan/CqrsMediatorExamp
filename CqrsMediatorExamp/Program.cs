using CqrsMediatorExamp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using CqrsMediatorExamp.Domain.Commands.Users;
using System.Reflection;
using CqrsMediatorExamp.Helpers;
using MediatR.Extensions.FluentValidation.AspNetCore;
using MediatR.Pipeline;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("CQRS_SAMPLE");
});

// Dependecy injection 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var domainAssembly = typeof(CreateUserCommand).GetTypeInfo().Assembly;
// Initialization of Commands and Queries in assemblies
builder.Services.AddMediatR(domainAssembly);

// Add logging
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddHttpContextAccessor();

//Error handling
builder.Services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(RequestGenericExceptionHandler<,,>));

builder.Services.AddFluentValidation(new[] { domainAssembly });

builder.Logging.AddEventLog(eventLogSettings =>
{
    eventLogSettings.SourceName = "CqrsMediatorExamp";
    eventLogSettings.LogName = "CqrsMediatorExamp";
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
