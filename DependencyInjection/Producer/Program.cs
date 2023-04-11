using Producer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProducerSvc, ProducerSvc>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
});

var config = new ProducerConfig { BootstrapServers = "localhost:9092"};
builder.Services.AddSingleton<IProducer<Null, string>>(x => new ProducerBuilder<Null, string>(config).Build());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

app.MapPost("/{collegeModel}", ([FromBody]College collegeModel, IProducerSvc producerSvc) => producerSvc.ProduceAsync(collegeModel));
// app.MapGet("/", () => "Hello World!");

app.Run();
