using Engine;
using Entities;
using Entities.Configuration;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

/// <summary>
/// Handles React app requests for app configuration
/// </summary>
[ApiController]
[Route("[controller]")]
public class AppConfigurationController : ControllerBase
{
    private readonly IClientNameResolver _clientNameResolver;
    private readonly AppDbContext _context;
    private readonly AppConfig _config;

    public AppConfigurationController(IClientNameResolver clientNameResolver, AppDbContext context, AppConfig config)
    {
        _clientNameResolver = clientNameResolver;
        this._context = context;
        this._config = config;
    }

    // Generate app ServiceConfiguration + storage configuration + key to read blobs
    // GET: AppConfiguration/ServiceConfiguration
    [HttpGet("[action]")]
    public ActionResult<ServiceConfiguration> GetServiceConfiguration()
    {
        // Return for react app
        return new ServiceConfiguration
        {
            ClientTerminalName = _clientNameResolver.GetClientTerminalName(HttpContext),
            AcsAccessKeyVal = _config.AcsAccessKeyVal,
            AcsEndpointVal = _config.AcsEndpointVal
        };
    }

}
