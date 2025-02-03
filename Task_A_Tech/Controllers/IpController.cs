using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/ip")]
public class IpController : ControllerBase
{
    private readonly IpService _ipService;

    public IpController(IpService ipService)
    {
        _ipService = ipService;
    }

    [HttpGet("lookup")]
    public async Task<IActionResult> LookupIp([FromQuery] string ipAddress)
    {
        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        }

        var result = await _ipService.GetCountryCodeByIP(ipAddress);
        return Ok(result);
    }

    [HttpGet("check-block")]
    public async Task<IActionResult> CheckIfIpIsBlocked()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

        var isBlocked = await _ipService.CheckIfIpIsBlocked(ipAddress, userAgent);
        return Ok(new { IsBlocked = isBlocked });
    }
}