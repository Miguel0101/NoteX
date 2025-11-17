using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.WebHost.UseQuic();
builder.Services.AddCors();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapOpenApi();
app.MapScalarApiReference();
app.UseCors();
app.MapControllers();

app.Run();
