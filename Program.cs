using _net_taskApiSimple.Interfaces;
using _net_taskApiSimple.Repositories;
using _net_taskApiSimple.Services;
using _net_taskApiSimple.Mappings; 
using _net_taskApiSimple.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using _net_taskApiSimple.Helpers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<TaskService>();

// EndpointsApiExplorer: API endpoint’lerini keşfetmek için kullanılır.
// SwaggerGen(): Swagger arayüzü ve JSON dökümantasyonu üretir.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(TaskMappingProfile));
//“Biri ITaskRepository isterse, ona TaskRepository ver.”
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);


// default kimlik doğrulama yöntemi JWT Bearer olsun
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // development için, prod’da true olmalı
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSwaggerGen(options =>
{
    // JWT destekleyen security scheme tanımı
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Token'ı giriniz. Örnek: Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    // Endpoint'lere bu güvenlik tanımını uygula
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// AppDbContext içerisine options ile sql url veridli
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Uygulama oluşturulur ve yapılandırma tamamlanır.
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// HTTP isteklerini otomatik olarak HTTPS’ye yönlendirir.
app.UseHttpsRedirection();

// [Route] ve [HttpGet] gibi anotasyonlara sahip olan controller'ları HTTP endpoint olarak haritalar.
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
