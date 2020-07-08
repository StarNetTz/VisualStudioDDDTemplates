using DomainName.WebApi.ServiceInterface;
using System;

namespace DomainName.WebApi.Infrastructure
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.UtcNow;
    }
}
