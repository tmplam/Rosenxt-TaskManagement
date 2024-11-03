using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Users.Queries.Login;

public record LoginQuery(string Email, string Password) : IQuery<LoginResult>;

public record LoginResult(string Token);