using conocelos_v3;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

// Supiuesto acceso a la carpeta publica www
builder.WebHost.UseWebRoot("wwwroot");

startup.ConfigureServices(builder.Services);

var app = builder.Build();
app.UseStaticFiles();

startup.Configure(app, app.Environment);
app.Run();
