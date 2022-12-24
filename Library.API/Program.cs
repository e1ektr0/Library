using Library.API;
using Library.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApi(builder.Configuration);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await using var scope = app.Services.CreateAsyncScope();
var dbInitializer = scope.ServiceProvider.GetService<DatabaseInitializer>()!;
await dbInitializer.Init();

app.Run();