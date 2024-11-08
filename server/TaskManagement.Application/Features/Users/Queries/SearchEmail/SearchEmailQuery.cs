using TaskManagement.Application.Abstractions.Messagings;

namespace TaskManagement.Application.Features.Users.Queries.SearchEmail;

public record SearchEmailQuery(string Keyword) : IQuery<SearchEmailResult>;

public record SearchEmailResult(List<string> EmailList);