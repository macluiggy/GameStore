using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();

List<GameDto> games = [
  new (1, "Super Mario Bros.", "Platformer", 29.99m, new DateOnly(1985, 9, 13)),
  new (2, "The Legend of Zelda", "Action-adventure", 49.99m, new DateOnly(1986, 2, 21)),
  new (3, "Minecraft", "Sandbox", 19.99m, new DateOnly(2011, 11, 18)),
  new (4, "Portal 2", "Puzzle-platformer", 9.99m, new DateOnly(2011, 4, 18)),
  new (5, "The Witcher 3: Wild Hunt", "Action RPG", 39.99M, new DateOnly(2015, 5, 19))
];

app.MapGet("/", () => "Hello World!");

app.Run();
