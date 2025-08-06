using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using service.roleapi.Data;
using service.roleapi.Mapper;
using service.roleapi.Service;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// THIS IS FOR MEDIATOR FOR CQRS CONFIGURATION
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// THIS IS FOR DB CONFIGURATION
builder.Services.AddDbContext<RoleDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//THIS IS FOR AUTOMAPPER CONFIGURATION (CONFIGURE AUTOMAPPER SERVICE)
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
builder.Services.AddControllers();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//THIS IS FOR REPOSITORY CONFIGURATION (CONFIGURE MICRO SERVICE)
builder.Services.AddScoped<IRoleService, RoleService>();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//THIS IS FOR YARP COMMUNICATION TO GATEWAY
app.MapGet("/health", () => Results.Ok("Healthy"));
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Run(args.Length > 0 ? args[0] : "https://localhost:7201");
app.Run();
