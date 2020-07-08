using System;

namespace DomainName.WebApi.ServiceInterface
{
    public interface ITimeProvider
    {
        DateTime GetUtcTime();
    }
}
