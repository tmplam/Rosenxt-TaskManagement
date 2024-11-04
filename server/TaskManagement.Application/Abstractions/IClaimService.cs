namespace TaskManagement.Application.Abstractions;

public interface IClaimService
{
    string GetUserId();
    string? GetClaim(string key);
}
