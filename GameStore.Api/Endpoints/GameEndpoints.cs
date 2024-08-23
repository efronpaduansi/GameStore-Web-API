using GameStore.Api.Entities;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{

    const string GetGameEndpointName = "GetGame";

    static List<Game> games = new()
    {
        new Game()
        {
            Id = 1,
            Name = "Street Fighther II",
            Genre = "Fighting",
            Price = 19.99M,
            ReleaseDate = new DateTime(1991, 2, 1),
            ImageUri = "https://placehold.co/100"
        },
        new Game()
        {
            Id = 2,
            Name = "Final Fantasy",
            Genre = "Roleplaying",
            Price = 39.99M,
            ReleaseDate = new DateTime(2001, 2, 1),
            ImageUri = "https://placehold.co/100"
        },
        new Game()
        {
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 49.99M,
            ReleaseDate = new DateTime(2008, 2, 1),
            ImageUri = "https://placehold.co/100"
        },
    };
    
    public static RouteGroupBuilder MapGameEndpoints(this IEndpointRouteBuilder routes)
    {
        //Map group implementation
        var group = routes.MapGroup("/games").WithParameterValidation();
        //Get all games
        group.MapGet("/", () => games);

        //Get game by Id
        group.MapGet("/{id}", (int id) =>
        {
            Game? game = games.Find(game => game.Id == id);

            if (game is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(game);
        }).WithName(GetGameEndpointName);

        //Create new game
        group.MapPost("/", (Game game) =>
        {
            game.Id = games.Max(game => game.Id) + 1;
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });


        group.MapPut("/{id}", (int id, Game updatedGame) =>
        {
            Game? existingGame = games.Find(game => game.Id == id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = updatedGame.Name;
            existingGame.Genre = updatedGame.Genre;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;
            existingGame.ImageUri = updatedGame.ImageUri;

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            Game? game = games.Find(game => game.Id == id);

            if (game is not null)
            {
                games.Remove(game);
            }

            return Results.NoContent();
        });

        return group;
    }

}