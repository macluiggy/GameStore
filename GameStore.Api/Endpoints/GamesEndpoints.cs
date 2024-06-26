using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
  const string GetGameByIdRouteName = "GetGameById";
  private static readonly List<GameDto> games = [
    new (1, "Super Mario Bros.", "Platformer", 29.99m, new DateOnly(1985, 9, 13)),
  new (2, "The Legend of Zelda", "Action-adventure", 49.99m, new DateOnly(1986, 2, 21)),
  new (3, "Minecraft", "Sandbox", 19.99m, new DateOnly(2011, 11, 18)),
  new (4, "Portal 2", "Puzzle-platformer", 9.99m, new DateOnly(2011, 4, 18)),
  new (5, "The Witcher 3: Wild Hunt", "Action RPG", 39.99M, new DateOnly(2015, 5, 19))
  ];

  public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
  {

    var group = app.MapGroup("games")
    .WithParameterValidation();
    // GET /games
    group.MapGet("", () => games);

    // GET /1
    group.MapGet("/{id}", (int id) =>
    {
      var game = games.Find(g => g.Id == id);
      return game is null ? Results.NotFound() : Results.Ok(game);
    })
    .WithName(GetGameByIdRouteName);

    // POST 
    group.MapPost("/", (CreateGameDto game) =>
    {
      GameDto newGame = new(games.Count + 1, game.Name, game.Genre, game.Price, game.ReleaseDate);
      games.Add(newGame);
      // return game;
      return Results.CreatedAtRoute(GetGameByIdRouteName, new { id = newGame.Id }, newGame);
    });

    // PUT /1
    group.MapPut("/{id}", (int id, UpdateGameDto game) =>
    {
      var index = games.FindIndex(g => g.Id == id);
      if (index == -1)
      {
        return Results.NotFound();
      }
      games[index] = new GameDto(id, game.Name, game.Genre, game.Price, game.ReleaseDate);

      return Results.NoContent();
    });

    // DELETE /1
    group.MapDelete("/{id}", (int id) =>
    {
      var index = games.FindIndex(g => g.Id == id);
      if (index == -1)
      {
        return Results.NotFound();
      }
      games.RemoveAt(index);

      return Results.NoContent();
    });

    return group;
  }
}
