using Microsoft.EntityFrameworkCore;
using SecondAssignment.Application.Contracts;
using SecondAssignment.Database.Context;
using SecondAssignment.Infraestructure.Interfaces;
using SecondAssignment.Infraestructure.Repositories;
using SecondAssignment.Application.Services;
using SecondAssignment.Infraestructure.Utils.ILoggerConcrete;
using SecondAssignment.Database.Entities;
using SecondAssignment.WepApp.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Repositories DI's
builder.Services.AddScoped<ISeriesRepository, SeriesRepository>();
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

// Services DI's
builder.Services.AddTransient<ISeriesService, SeriesService>();
builder.Services.AddTransient<IProducerService, ProducerService>();
builder.Services.AddTransient<IGenreService, GenreService>();

// Loggers DI's
builder.Services.AddSingleton<IConcreteLogger, ConcreteLogger<SeriesService>>();
builder.Services.AddSingleton<IConcreteLogger, ConcreteLogger<Producer>>();
builder.Services.AddSingleton<IConcreteLogger, ConcreteLogger<Genre>>();

builder.Services.AddTransient<GenereteSelectLists>();
//Adding Context
string ConectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<SecondAssignmentContext>(options => { options.UseSqlServer(ConectionString); });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Series}/{action=Index}/{id?}");

app.Run();
