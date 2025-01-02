using ConferenceRoomBooking.BLL.Interfaces;
using ConferenceRoomBooking.BLL.Services;
using ConferenceRoomBooking.DTO;
using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.DTO.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Dependency injection for repositiries
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IRoomServiceRepository, RoomServiceRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IConferenceRoomRepository, ConferenceRoomRepository>();
// Dependency injection for services
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IConferenceRoomService, ConferenceRoomService>();
builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// connect to the postgres database via connection string (in appsettings.json)
builder.Services.AddDbContext<BookingDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
