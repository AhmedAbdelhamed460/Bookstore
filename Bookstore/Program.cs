using Bookstore.Models;
using Bookstore.Reposiotries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<BookStoreDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddIdentity<AppUser, IdentityRole>() //Identity
    .AddEntityFrameworkStores<BookStoreDbContext>();

builder.Services.AddScoped<IBookRepo, BookRepo>(); //Ingect IBookRepo

builder.Services.AddCors();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
