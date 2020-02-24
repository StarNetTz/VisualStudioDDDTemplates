using System;

namespace DomainName.Domain
{
    public interface ITimeProvider
    {
        DateTime GetUtcTime();
    }
}
