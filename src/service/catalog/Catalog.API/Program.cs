

using BuildBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();



builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(new[] { typeof(Program).Assembly });
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
// Yahan "MartenDb" ki jagah "catalog" likhein taaki wo Aspire ka dynamic connection uthaye

builder.Services.AddValidatorsFromAssemblies(new[] { typeof(Program).Assembly });
builder.Services.AddCarter();
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("catalog")!);
    // options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();
builder.AddRedisClient("redis-cache");
builder.AddNpgsqlDbContext<DbContext>("catalog");

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();


// Program.cs (or wherever you configure the pipeline) using FluentValidation -- Once user custome exception handler is implemented, you can remove this middleware and use the custom exception handler instead.
//#region This is Global pipeline excetion
//app.Use(async (context, next) =>
//{
//try
//{
//await next();
//}
//catch (FluentValidation.ValidationException vex)
//{
//context.Response.StatusCode = StatusCodes.Status400BadRequest;
//context.Response.ContentType = "application/problem+json";

//var errors = vex.Errors
//    .GroupBy(e => e.PropertyName)
//    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

//var problem = new ValidationProblemDetails(errors)
//{
//    Title = "Validation error",
//    Status = StatusCodes.Status400BadRequest,
//    Detail = "One or more validation errors occurred."
//};

//await context.Response.WriteAsJsonAsync(problem);
//}
//}); 
//#endregion

