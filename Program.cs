using _net_taskApiSimple.Interfaces;
using _net_taskApiSimple.Repositories;
using _net_taskApiSimple.Services;
using _net_taskApiSimple.Mappings; 
using _net_taskApiSimple.Data;
using Microsoft.EntityFrameworkCore;

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
app.Run();
