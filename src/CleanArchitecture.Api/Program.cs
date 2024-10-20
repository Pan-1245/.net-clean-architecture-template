using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Persistence;
using CleanArchitecture.Shared.UseCases;
using CleanArchitecture.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContext")));

// Other service configurations
builder.Services.AddApplicationInjection();
builder.Services.AddPersistenceInjection();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(x => true);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<CustomExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
