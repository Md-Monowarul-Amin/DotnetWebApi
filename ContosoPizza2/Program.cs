//global using Microsoft.EntityFrameworkCore;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Data.Entity;
//using DotNetCoreMySQL.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection_string = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.WriteLine(connection_string);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connection_string, ServerVersion.AutoDetect(connection_string));
});


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

app.Run();
