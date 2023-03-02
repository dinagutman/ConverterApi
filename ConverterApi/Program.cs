using BLConverter.Services;
using DALConverter;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Host.ConfigureServices(services =>
{
    services.AddScoped<IConvertRepository, ConvertRepository>();
    services.AddScoped<IConvertService, ConvertService>();
    services.AddMvcCore();
    services.AddControllers();

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();


app.UseAuthorization();

app.MapControllers();

app.Run();
