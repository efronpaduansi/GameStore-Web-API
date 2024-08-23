using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//map group for games
app.MapGameEndpoints();

app.Run();
