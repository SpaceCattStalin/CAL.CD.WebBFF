using BFF.Client.Dispatches;
using BFF.Services.Dispatches;
using WebBFF;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddTransient<BearerTokenForwardingHandler>();
// Register a typed client 
builder.Services.AddHttpClient<IDispatchServiceClient, DispatchServiceClient>(client =>
{
    var baseUrl = builder.Configuration["DownstreamServices:CentralDispatch:BaseUrl"]
        ?? throw new InvalidOperationException("Missing configuration: DownstreamServices:CentralDispatch:BaseUrl");
    client.BaseAddress = new Uri(baseUrl);
})
// forward the bearer token to outward Http request
.AddHttpMessageHandler<BearerTokenForwardingHandler>();

builder.Services.AddScoped<IDispatchesService, DispatchesService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = context.HttpContext.Request.Path;
        context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
        context.ProblemDetails.Extensions["timestamp"] = DateTime.UtcNow;
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
    };
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

app.MapControllers();

app.Run();