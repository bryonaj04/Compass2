﻿using System.Configuration;
using Compass.Controllers;
using Compass.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Compass.Data.DbInitializer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SchedulingContext>(x => x.UseSqlite(connectionString));
builder.Services.AddScoped<DbInitializer>();
builder.Services.AddTransient<PersonController>();
builder.Services.AddTransient<RoomController>();
builder.Services.AddTransient<SchedulerController>();
builder.Services.AddTransient<MeetingController>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}



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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

