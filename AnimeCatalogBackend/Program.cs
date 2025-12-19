var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var animeCatalogItems = app.MapGroup("/animeCatalog");

animeCatalogItems.MapGet("/", GetAllAnimeCatalogItems);

app.Run();

static async Task<IResult> GetAllAnimeCatalogItems()
{
    return TypedResults.Ok("Hello World!");
}