using DigiQuiz.Application.DependencyInjection;
using DigiQuiz.Infrastructure.Data.DbContexts;
using DigiQuiz.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var DigimonOrigin = "_digimonOrigin";

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: DigimonOrigin,
                      policy =>
                      {
                          policy
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});


var userSecretsConnectionString = builder.Configuration["ConnectionStrings:DatabaseConnection"];
builder.Services.AddDbContext<PlayerDbContext>(opt => opt.UseSqlServer(userSecretsConnectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(DigimonOrigin);

app.UseAuthorization();

app.MapControllers();

app.Run();