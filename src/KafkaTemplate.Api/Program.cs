using KafkaTemplate.Api.Middlewares;
using KafkaTemplate.Api.Shared;
using KafkaTemplate.Core.Shared;
using KafkaTemplate.Data.Shared;
using KafkaTemplate.Messaging.Kafka.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCore();
builder.Services.AddData(builder.Configuration);
builder.Services.AddMappings();
builder.Services.AddExceptionHandling();
builder.Services.AddMiddleware();
builder.Services.AddMessaging(builder.Configuration);
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

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<ActivityMiddleware>();

app.Run();
