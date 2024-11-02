using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TaskManagement.Persistence.Interceptors;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{

}
