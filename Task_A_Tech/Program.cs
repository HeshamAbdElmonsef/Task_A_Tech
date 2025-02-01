using System.Collections.Concurrent;
using Task_A_Tech.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// ????? ConcurrentDictionary ????? Singleton
builder.Services.AddSingleton<ConcurrentDictionary<string, Country>>();

// ????? ?????????? ????????
builder.Services.AddSingleton<BlockedCountryRepository>();
builder.Services.AddSingleton<BlockedAttemptLogRepository>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<IpService>();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();

// ????? ????? ???????? ?????? ?? Swagger (Swagger UI)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();