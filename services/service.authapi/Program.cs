using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using service.authapi.Data;
using service.authapi.Mapper;
using service.authapi.Service;

var builder = WebApplication.CreateBuilder(args);
// THIS IS FOR DB CONFIGURATION
builder.Services.AddDbContext<AuthDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// THIS IS FOR JWT TOKEN CONFIGURATION
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
builder.Services.AddControllers();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// THIS IS FOR JWT TOKEN CONFIGURATION
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//THIS IS FOR REPOSITORY CONFIGURATION (CONFIGURE MICRO SERVICE)
builder.Services.AddScoped<IAuthService, AuthService>();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//THIS IS FOR AUTOMAPPER CONFIGURATION (CONFIGURE AUTOMAPPER SERVICE)
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
