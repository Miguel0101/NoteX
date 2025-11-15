var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseQuic();
builder.Services.AddCors();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();

app.Run();
