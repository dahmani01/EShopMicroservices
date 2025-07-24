using BuildingBlocks.Behaviors;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR((config) =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddCarter();

builder.Services.AddMarten(options => { options.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception == null)
        {
            return;
        }

        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Detail = exception.StackTrace,
            Status = StatusCodes.Status500InternalServerError,
        };
        
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
        
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";
        
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: context.RequestAborted);
    });
}); 

app.Run();