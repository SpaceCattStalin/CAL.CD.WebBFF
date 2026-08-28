namespace WebBFF;

/// <summary>
/// Outware middleware configuration for Http request from this server
/// </summary>
/// <param name="httpContextAccessor">To access the HttpContext</param>
/// <param name="logger"></param>
public class BearerTokenForwardingHandler(IHttpContextAccessor httpContextAccessor, ILogger<BearerTokenForwardingHandler> logger) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? authHeader = httpContextAccessor.HttpContext?.Request.Headers.Authorization;

        if (string.IsNullOrEmpty(authHeader))
            logger.LogCritical("Unauthenticated access");
        else
            request.Headers.Add("Authorization", authHeader);

        return base.SendAsync(request, cancellationToken);
    }
}
