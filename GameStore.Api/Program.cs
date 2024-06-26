using GameStore.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();

const string GetGameByIdRouteName = "GetGameById";
List<GameDto> games = [
  new (1, "Super Mario Bros.", "Platformer", 29.99m, new DateOnly(1985, 9, 13)),
  new (2, "The Legend of Zelda", "Action-adventure", 49.99m, new DateOnly(1986, 2, 21)),
  new (3, "Minecraft", "Sandbox", 19.99m, new DateOnly(2011, 11, 18)),
  new (4, "Portal 2", "Puzzle-platformer", 9.99m, new DateOnly(2011, 4, 18)),
  new (5, "The Witcher 3: Wild Hunt", "Action RPG", 39.99M, new DateOnly(2015, 5, 19))
];
// GET /games
app.MapGet("/games", () => games);

// GET /games/1
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id))
.WithName(GetGameByIdRouteName);

// POST /games
app.MapPost("/games", (CreateGameDto game) =>
{
  GameDto newGame = new(games.Count + 1, game.Name, game.Genre, game.Price, game.ReleaseDate);
  games.Add(newGame);
  // return game;
  return Results.CreatedAtRoute(GetGameByIdRouteName, new { id = newGame.Id }, newGame);
});

// PUT /games/1
app.MapPut("/games/{id}", (int id, UpdateGameDto game) =>
{
  var index = games.FindIndex(g => g.Id == id);
  if (index == -1)
  {
    return Results.NotFound();
  }
  games[index] = new GameDto(id, game.Name, game.Genre, game.Price, game.ReleaseDate);

  return Results.NoContent();
});

// app.MapGet("/", () => "Hello World!");

app.Run();
