using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskManagement.Application.Abstractions;

namespace TaskManagement.Infrastructure.Authentication;

public class ClaimService(IHttpContextAccessor _httpContextAccessor) : IClaimService
{
    public string? GetClaim(string key) => _httpContextAccessor.HttpContext?.User.FindFirst(key)?.Value;

    public string GetUserId() => GetClaim(ClaimTypes.NameIdentifier)!;
}
