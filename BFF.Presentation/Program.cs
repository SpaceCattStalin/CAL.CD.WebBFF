using BFF.Client.Companies;
using BFF.Client.Dispatches;
using BFF.Client.SearchService;
using BFF.Services.Companies;
using BFF.Services.Dispatches;
using WebBFF;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

const string DevelopmentCorsPolicy = "DevelopmentCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(DevelopmentCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("Location");
    });
});

builder.Services.AddTransient<BearerTokenForwardingHandler>();
// Register a typed client for CentralDispatch
builder.Services.AddHttpClient<IDispatchServiceClient, DispatchServiceClient>(client =>
{
    var baseUrl = builder.Configuration["DownstreamServices:CentralDispatch:BaseUrl"]
        ?? throw new InvalidOperationException("Missing configuration: DownstreamServices:CentralDispatch:BaseUrl");
    client.BaseAddress = new Uri(baseUrl);
})
// forward the bearer token to outward Http request
.AddHttpMessageHandler<BearerTokenForwardingHandler>();

// Register a typed client for SearchService
builder.Services.AddHttpClient<ISearchServiceClient, SearchServiceClient>(client =>
{
    var baseUrl = builder.Configuration["DownstreamServices:SearchService:BaseUrl"]
        ?? throw new InvalidOperationException("Missing configuration: DownstreamServices:SearchService:BaseUrl");
    client.BaseAddress = new Uri(baseUrl);
})
// forward the bearer token to outward Http request
.AddHttpMessageHandler<BearerTokenForwardingHandler>();

// Register a typed client for CentralDispatch's Company endpoints
builder.Services.AddHttpClient<ICompanyServiceClient, CompanyServiceClient>(client =>
{
    var baseUrl = builder.Configuration["DownstreamServices:CentralDispatch:BaseUrl"]
        ?? throw new InvalidOperationException("Missing configuration: DownstreamServices:CentralDispatch:BaseUrl");
    client.BaseAddress = new Uri(baseUrl);
})
// forward the bearer token to outward Http request
.AddHttpMessageHandler<BearerTokenForwardingHandler>();

builder.Services.AddScoped<IDispatchesService, DispatchesService>();
builder.Services.AddScoped<ICompaniesService, CompaniesService>();
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
    app.UseCors(DevelopmentCorsPolicy);
}

app.UseExceptionHandler();

app.MapControllers();

app.Run();