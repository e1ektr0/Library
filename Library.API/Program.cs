using Library.API;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApi(builder.Configuration);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

await using var scope = app.Services.CreateAsyncScope();
var dbInitializer = scope.ServiceProvider.GetService<DatabaseInitializer>()!;
await dbInitializer.Init();

app.Run();