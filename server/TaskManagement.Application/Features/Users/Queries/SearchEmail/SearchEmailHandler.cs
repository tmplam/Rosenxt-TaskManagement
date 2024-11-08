using TaskManagement.Application.Abstractions.Messagings;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Features.Users.Queries.SearchEmail;

public sealed class SearchEmailHandler(IUserRepository _userRepository) : IQueryHandler<SearchEmailQuery, SearchEmailResult>
{
    public async Task<SearchEmailResult> Handle(SearchEmailQuery query, CancellationToken cancellationToken)
    {
        var emailList = await _userRepository.GetEmailListAsync(query.Keyword);
        return new SearchEmailResult(emailList);
    }
}
