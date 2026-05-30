using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw8_s33974.Infrastructure;
using PJATK_APBD_Cw8_s33974.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//ZMIEN TO
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddDbContext<MasterContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "PJATK-APBD-Cw8-s33974"));
//http://localhost:XXXX/swagger/index.html
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli