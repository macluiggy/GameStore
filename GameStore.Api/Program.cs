using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
// print the connection string to the console
Console.WriteLine($"Connection String: {connString}");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MapGet("/", () => "Hello World!");

app.MigrateDb();

app.Run();
