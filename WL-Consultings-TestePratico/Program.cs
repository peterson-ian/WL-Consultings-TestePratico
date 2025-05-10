using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WL_Consultings_TestePratico.Configurations;
using WL_Consultings_TestePratico.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Postgres
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
    ?? throw new ArgumentException("Invalid db conection");

builder.Services.AddDbContext<PostgreDbContext>(options =>
    options.UseNpgsql(connectionString));

// Services
builder.Services.AddApplicationServices();

// DTOs
builder.Services.AddAutoMapper(typeof(MapperProfile));

// Autenticacao
builder.Services.ConfigureAuthentication(builder.Configuration);

var app = builder.Build();

// Midllewares
app.AddCustomMiddlewares();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
