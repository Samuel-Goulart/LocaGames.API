using LocaGames_API.Domain.Repository;
using LocaGames_API.Domain.Service;
using LocaGames_API.Infra;
using LocaGames_API.Infra.DatabaseConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbConnectionFactory>(x =>
{
    return new DbConnectionFactory("Server=(localdb)\\MSSQLLocalDB; Database=LocaGames; Trusted_Connection=True;");
});

// Add services to the container.
builder.Services.AddScoped<IJogosService, JogosService>(); // a cada nova requisicao, o proprio .net dá um new no CarroService
builder.Services.AddScoped<IJogosRepository, JogoRepository>(); // a cada nova requisicao, o proprio .net dá um new no CarroRepository

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
