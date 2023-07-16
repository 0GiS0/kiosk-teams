using Microsoft.AspNetCore.Http;

namespace Engine;

public interface IClientNameResolver
{
    string GetClientTerminalName(HttpContext context);
}

public class HttpRequestClientNameResolver : IClientNameResolver
{
    public string GetClientTerminalName(HttpContext context)
    {
        return GetIPAddress(context) == "::1" ? "local" : "remote";
    }


    string? GetIPAddress(HttpContext context)
    {
        string ipAddress = context.Request.Headers["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Connection?.RemoteIpAddress?.ToString();
    }
}
