using Microsoft.EntityFrameworkCore;
using Serilog;
using service.userapi.Data;
using service.userapi.Mapper;
using System.Reflection;
using MediatR;
using service.userapi.Service;

var builder = WebApplication.CreateBuilder(args);
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//THIS IS FOR CASHING CONFIGURATION
builder.Services.AddResponseCaching(); // USE FOR response cache technique
builder.Services.AddMemoryCache(); // USE FOR in memory cache technique
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

//builder.Host.UseSerilog();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// THIS IS FOR MEDIATOR FOR CQRS CONFIGURATION
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// THIS IS FOR DB CONFIGURATION
builder.Services.AddDbContext<UserDbContext>(option =>
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
builder.Services.AddScoped<IUserService, UserService>();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//THIS IS FOR CASHING CONFIGURATION
app.UseResponseCaching();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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

//app.Run(args.Length > 0 ? args[0] : "https://localhost:7101");
app.Run();
