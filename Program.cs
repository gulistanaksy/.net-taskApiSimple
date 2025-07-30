using _net_taskApiSimple.Repositories;
using _net_taskApiSimple.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<TaskRepository>();
builder.Services.AddScoped<TaskService>();

// EndpointsApiExplorer: API endpoint’lerini keşfetmek için kullanılır.
// SwaggerGen(): Swagger arayüzü ve JSON dökümantasyonu üretir.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Uygulama oluşturulur ve yapılandırma tamamlanır.
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// HTTP isteklerini otomatik olarak HTTPS’ye yönlendirir.
app.UseHttpsRedirection();

// [Route] ve [HttpGet] gibi anotasyonlara sahip olan controller'ları HTTP endpoint olarak haritalar.
app.MapControllers();
app.Run();
