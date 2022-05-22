using FastAndFuriousApi.Data.IWantApp.Data;
using FastAndFuriousApi.Services;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("localMySqlConnection"), ServerVersion.Create(new Version(8, 0, 29), ServerType.MySql)));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<AuthorService, AuthorService>();
builder.Services.AddScoped<PhraseService, PhraseService>();
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
