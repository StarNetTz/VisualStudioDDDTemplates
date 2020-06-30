using System;

namespace DomainName.Domain.Infrastructure
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcTime()
            => DateTime.UtcNow;
    }
}
