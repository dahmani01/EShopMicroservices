var builder = WebApplication.CreateBuilder(args);

//Add Services to the container


var app = builder.Build();

//Configure HTTP Pipeline

app.MapGet("/", () => "Hello World!");

app.Run();
