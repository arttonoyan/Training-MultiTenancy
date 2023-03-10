using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddApplicationOptions(builder.Configuration);
builder.Services.AddAuthenticationLayer(builder.Configuration);
builder.Services.AddSwaggerLayer();
builder.Services.AddApplicationMiddlwares();
builder.Services.AddMultiTenancyData(builder.Configuration.GetConnectionString("LocalDbConnection")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMultiTenancy();

app.MapControllers();

app.Run();
